using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Сheck_сheck_server.Classes;


namespace Сheck_сheck_server
{
    public partial class Form1 : Form
    {
        private Socket serverSocket;
        private Thread serverThread;
        private bool isRunning;
        private List<Game> games = new List<Game>();
        private List<GameRoom> gameRooms = new List<GameRoom>();
        private readonly object gamesLock = new object();
        public int roomNumber = 8081;


        public Form1()
        {
            InitializeComponent();
            StopServerButton.Enabled = false;
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                serverThread = new Thread(StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();
                isRunning = true;
                StartServerButton.Text = "Сервер запущен";
                StopServerButton.Enabled = true;
            }
        }

        private void StopServerButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                serverSocket?.Close();
                StartServerButton.Text = "Запустить сервер";
                StopServerButton.Enabled = false;
            }
        }

        private void StartServer()
        {
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ipAddress, 8080));
                serverSocket.Listen(10);

                while (isRunning)
                {
                    var clientSocket = serverSocket.Accept();
                    var clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Обработка создания комнаты
                if (request.StartsWith("CREATE_GAME:"))
                {
                    string roomName = request.Substring("CREATE_GAME:".Length);
                    lock (gamesLock)
                    {
                        if (games.Exists(g => g.RoomName == roomName))
                        {
                            clientSocket.Send(Encoding.UTF8.GetBytes("ERROR:Комната с таким именем уже существует"));
                            return;
                        }

                        Game newGame = new Game(roomName);
                        if (newGame.TryAddPlayer(clientSocket))
                        {
                            games.Add(newGame);
                            clientSocket.Send(Encoding.UTF8.GetBytes($"GAME_CREATED:{roomName}:1/2:PLAYER1"));
                            clientSocket.Send(Encoding.UTF8.GetBytes($":{roomName}"));
                        }
                    }
                }
                // Обработка подключения к комнате
                else if (request.StartsWith("JOIN_GAME:"))
                {
                    string roomName = request.Substring("JOIN_GAME:".Length);
                    lock (gamesLock)
                    {
                        Game game = games.Find(g => g.RoomName == roomName);
                        if (game == null)
                        {
                            clientSocket.Send(Encoding.UTF8.GetBytes("JOIN_ERROR:Комната не найдена"));
                            return;
                        }

                        if (game.IsFull)
                        {
                            string playerRole = (game.Player1 == clientSocket) ? "PLAYER1" : "PLAYER2";
                            string symbol = playerRole == "PLAYER1" ? "W" : "B";
                            clientSocket.Send(Encoding.UTF8.GetBytes($"JOIN_SUCCESS:{roomName}:2/2:{playerRole}:{symbol}"));

                            game.Player1.Send(Encoding.UTF8.GetBytes("GAME_STARTED:W"));
                            game.Player2.Send(Encoding.UTF8.GetBytes("GAME_STARTED:B"));
                        }
                        else
                        {
                            if (game.TryAddPlayer(clientSocket))
                            {
                                string playerRole = (game.Player1 == clientSocket) ? "PLAYER1" : "PLAYER2";
                                clientSocket.Send(Encoding.UTF8.GetBytes($"JOIN_SUCCESS:{roomName}:1/2:{playerRole}"));
                            }
                        }
                    }
                }
                else if (request.StartsWith("SEND_MESSAGE:"))
                {
                    string[] parts = request.Split(new[] { ':' }, 3);
                    string roomName = parts[1];
                    string message = parts[2];

                    lock (gamesLock)
                    {
                        Game game = games.Find(g => g.RoomName == roomName);
                        if (game != null)
                        {
                            Socket targetPlayer = game.Player1 == clientSocket ? game.Player2 : game.Player1;
                            if (targetPlayer != null)
                            {
                                targetPlayer.Send(Encoding.UTF8.GetBytes($"MESSAGE:{message}"));
                                clientSocket.Send(Encoding.UTF8.GetBytes("MESSAGE_SENT"));
                            }
                        }
                    }
                }
                // Запрос списка игр
                // Хз зачем, есть автообновление

                else if (request == "GET_GAME_LIST")
                {
                    lock (gamesLock)
                    {
                        var gameList = new StringBuilder();
                        foreach (var game in games)
                        {
                            int playersCount = (game.Player1 != null ? 1 : 0) + (game.Player2 != null ? 1 : 0);
                            gameList.AppendLine($"{game.RoomName}|{playersCount}/2");
                        }
                        clientSocket.Send(Encoding.UTF8.GetBytes($"GAME_LIST:{gameList.ToString()}"));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
    }
}

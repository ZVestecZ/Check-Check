using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chess_Room_Server
{
    public partial class Form1 : Form
    {
        private static Socket serverSocket;
        private static List<Socket> clients = new List<Socket>();
        private static bool isRunning = true;
        private static object lockObj = new object();

        private static int whiteIndex = -1;
        private static int blackIndex = -1;
        public Form1()
        {
            InitializeComponent();

            StartServer();
        }

        private static void StartServer()
        {
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ipAddress, 8080));
                serverSocket.Listen(10);

                Console.WriteLine("Сервер запущен на 127.0.0.1:8080. Ожидание игроков...");

                while (isRunning)
                {
                    Socket clientSocket = serverSocket.Accept();
                    lock (lockObj)
                    {
                        clients.Add(clientSocket);
                    }

                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    Console.WriteLine($"Новое подключение: {clientSocket.RemoteEndPoint}");

                    if (clients.Count == 2)
                    {
                        AssignColors();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сервера: {ex.Message}");
            }
        }

        private static void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead == 0) break; // клиент отключился

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Получено сообщение: {message}");

                    // Переслать сообщение второму игроку
                    Socket opponentSocket = GetOpponentSocket(clientSocket);
                    if (opponentSocket != null)
                    {
                        opponentSocket.Send(Encoding.UTF8.GetBytes(message));
                    }
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Клиент отключился.");
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(clientSocket);
                }
                clientSocket.Close();
            }
        }

        private static void AssignColors()
        {
            Random rnd = new Random();
            int white = rnd.Next(2);
            int black = 1 - white;

            whiteIndex = white;
            blackIndex = black;

            clients[white].Send(Encoding.UTF8.GetBytes("COLOR:WHITE"));
            clients[black].Send(Encoding.UTF8.GetBytes("COLOR:BLACK"));

            Console.WriteLine("Цвета назначены: Белый - клиент " + clients[white].RemoteEndPoint +
                              ", Чёрный - клиент " + clients[black].RemoteEndPoint);
        }

        private static Socket GetOpponentSocket(Socket current)
        {
            lock (lockObj)
            {
                if (clients.Count < 2)
                    return null;

                if (clients[0] == current)
                    return clients[1];
                else if (clients[1] == current)
                    return clients[0];
            }
            return null;
        }
    }
}

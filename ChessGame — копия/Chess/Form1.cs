using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class Form1 : Form
    {
        private Socket clientSocket;
        private string playerColor;
        private Thread listenThread;
        private static readonly string logFilePath = "chess_log.txt";


        public Image chessSprites;
        public int[,] map = new int[8, 8]
        {
            {15,14,13,12,11,13,14,15 },
            {16,16,16,16,16,16,16,16 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {26,26,26,26,26,26,26,26 },
            {25,24,23,22,21,23,24,25 },
        };

        public Button[,] butts = new Button[8, 8];

        public int currPlayer;

        public Button prevButton;

        public bool isMoving = false;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();

            chessSprites = Properties.Resources.chess;

            Init();
        }

        public void Init()
        {
            map = new int[8, 8]
            {
            {15,14,13,12,11,13,14,15 },
            {16,16,16,16,16,16,16,16 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {26,26,26,26,26,26,26,26 },
            {25,24,23,22,21,23,24,25 },
            };

            currPlayer = 1;
            CreateMap();
        }

        public void CreateMap()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butts[i, j] = new Button();

                    var butt = new Button();
                    butt.Size = new Size(50, 50);
                    butt.Location = new Point(j * 50, i * 50);

                    switch (map[i, j] / 10)
                    {
                        case 1:
                            Image part = new Bitmap(50, 50);
                            Graphics g = Graphics.FromImage(part);
                            g.DrawImage(chessSprites, new Rectangle(0, 0, 50, 50), 0 + 150 * (map[i, j] % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                            butt.BackgroundImage = part;
                            break;
                        case 2:
                            Image part1 = new Bitmap(50, 50);
                            Graphics g1 = Graphics.FromImage(part1);
                            g1.DrawImage(chessSprites, new Rectangle(0, 0, 50, 50), 0 + 150 * (map[i, j] % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                            butt.BackgroundImage = part1;
                            break;
                    }
                    butt.BackColor = Color.White;
                    butt.Click += new EventHandler(OnFigurePress);
                    this.Controls.Add(butt);

                    butts[i, j] = butt;
                }
            }
        }
        
        public void OnFigurePress(object sender, EventArgs e)
        {
            if (prevButton != null)
            {
                prevButton.BackColor = Color.White;
            }

            var pressedButton = sender as Button;
            int row = pressedButton.Location.Y / 50;
            int col = pressedButton.Location.X / 50;

            bool isMyTurn = (playerColor == "WHITE" && currPlayer == 1) || (playerColor == "BLACK" && currPlayer == 2);
            if (!isMyTurn)
            {
                MessageBox.Show("Сейчас не ваш ход!");
                return;
            }

            if (map[row, col] != 0 && map[row, col] / 10 == currPlayer)
            {
                CloseSteps();
                pressedButton.BackColor = Color.Red;
                DeactivateAllButtons();
                pressedButton.Enabled = true;

                ShowSteps(row, col, map[row, col]);

                if (isMoving)
                {
                    CloseSteps();
                    pressedButton.BackColor = Color.White;
                    ActivateAllButtons();
                    isMoving = false;
                }
                else
                {
                    isMoving = true;
                }
            }
            else if (isMoving)
            {
                int fromRow = prevButton.Location.Y / 50;
                int fromCol = prevButton.Location.X / 50;

                map[row, col] = map[fromRow, fromCol];
                map[fromRow, fromCol] = 0;
                pressedButton.BackgroundImage = prevButton.BackgroundImage;
                prevButton.BackgroundImage = null;

                isMoving = false;
                CloseSteps();
                ActivateAllButtons();

                // отправка хода на сервер
                string move = $"{fromRow},{fromCol};{row},{col}";
                SendMove(move);
            }

            prevButton = pressedButton;
        }

        public void ShowSteps(int IcurrFigure, int JcurrFigure, int currFigure)
        {
            int dir = currPlayer == 1 ? 1 : -1;
            switch (currFigure%10)
            {
                case 6:
                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure] == 0)
                        {
                            butts[IcurrFigure + 1 * dir, JcurrFigure].BackColor = Color.Yellow;
                            butts[IcurrFigure + 1 * dir, JcurrFigure].Enabled = true;
                        }
                    }
                    if ((currPlayer == 1 && IcurrFigure == 1) || (currPlayer == 2 && IcurrFigure == 6))
                    {
                        if (map[IcurrFigure + 2 * dir, JcurrFigure] == 0)
                        {
                            butts[IcurrFigure + 2 * dir, JcurrFigure].BackColor = Color.Yellow;
                            butts[IcurrFigure + 2 * dir, JcurrFigure].Enabled = true;
                        }
                    }

                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure+1))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure + 1] != 0 && map[IcurrFigure + 1 * dir, JcurrFigure + 1] / 10 != currPlayer)
                        {
                            butts[IcurrFigure + 1 * dir, JcurrFigure + 1].BackColor = Color.Yellow;
                            butts[IcurrFigure + 1 * dir, JcurrFigure + 1].Enabled = true;
                        }
                    }
                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure - 1))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure - 1] != 0 && map[IcurrFigure + 1 * dir, JcurrFigure - 1] / 10 != currPlayer)
                        {
                            butts[IcurrFigure + 1 * dir, JcurrFigure - 1].BackColor = Color.Yellow;
                            butts[IcurrFigure + 1 * dir, JcurrFigure - 1].Enabled = true;
                        }
                    }
                    break;
                case 5:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure);
                    break;
                case 3:
                    ShowDiagonal(IcurrFigure, JcurrFigure);
                    break;
                case 2:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure);
                    ShowDiagonal(IcurrFigure, JcurrFigure);
                    break;
                case 1:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure,true);
                    ShowDiagonal(IcurrFigure, JcurrFigure,true);
                    break;
                case 4:
                    ShowKnightSteps(IcurrFigure, JcurrFigure);
                    break;
            }
        }

        public void ShowKnightSteps(int IcurrFigure, int JcurrFigure)
        {
            if (InsideBorder(IcurrFigure - 2, JcurrFigure + 1))
            {
                DeterminePath(IcurrFigure - 2, JcurrFigure + 1);
            }
            if (InsideBorder(IcurrFigure - 2, JcurrFigure - 1))
            {
                DeterminePath(IcurrFigure - 2, JcurrFigure - 1);
            }
            if (InsideBorder(IcurrFigure + 2, JcurrFigure + 1))
            {
                DeterminePath(IcurrFigure + 2, JcurrFigure + 1);
            }
            if (InsideBorder(IcurrFigure + 2, JcurrFigure - 1))
            {
                DeterminePath(IcurrFigure + 2, JcurrFigure - 1);
            }
            if (InsideBorder(IcurrFigure - 1, JcurrFigure + 2))
            {
                DeterminePath(IcurrFigure - 1, JcurrFigure + 2);
            }
            if (InsideBorder(IcurrFigure + 1, JcurrFigure + 2))
            {
                DeterminePath(IcurrFigure +1, JcurrFigure + 2);
            }
            if (InsideBorder(IcurrFigure - 1, JcurrFigure - 2))
            {
                DeterminePath(IcurrFigure - 1, JcurrFigure -2);
            }
            if (InsideBorder(IcurrFigure + 1, JcurrFigure - 2))
            {
                DeterminePath(IcurrFigure +1, JcurrFigure -2);
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butts[i, j].Enabled = false;
                }
            }
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butts[i, j].Enabled = true;
                }
            }
        }

        public void ShowDiagonal(int IcurrFigure, int JcurrFigure,bool isOneStep=false)
        {
            int j = JcurrFigure + 1;
            for(int i = IcurrFigure-1; i >= 0; i--)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j <7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

        public void ShowVerticalHorizontal(int IcurrFigure, int JcurrFigure,bool isOneStep=false)
        {
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, JcurrFigure))
                {
                    if (!DeterminePath(i, JcurrFigure))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (InsideBorder(i, JcurrFigure))
                {
                    if (!DeterminePath(i, JcurrFigure))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int j = JcurrFigure + 1; j < 8; j++)
            {
                if (InsideBorder(IcurrFigure, j))
                {
                    if (!DeterminePath(IcurrFigure, j))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int j = JcurrFigure - 1; j >= 0; j--)
            {
                if (InsideBorder(IcurrFigure, j))
                {
                    if (!DeterminePath(IcurrFigure, j))
                        break;
                }
                if (isOneStep)
                    break;
            }
        }

        public bool DeterminePath(int IcurrFigure,int j)
        {
            if (map[IcurrFigure, j] == 0)
            {
                butts[IcurrFigure, j].BackColor = Color.Yellow;
                butts[IcurrFigure, j].Enabled = true;
            }
            else
            {
                if (map[IcurrFigure, j] / 10 != currPlayer)
                {
                    butts[IcurrFigure, j].BackColor = Color.Yellow;
                    butts[IcurrFigure, j].Enabled = true;
                }
                return false;
            }
            return true;
        }

        public bool InsideBorder(int ti,int tj)
        {
            if (ti >= 8 || tj >= 8 || ti < 0 || tj < 0)
                return false;
            return true;
        }

        public void CloseSteps()
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    butts[i, j].BackColor = Color.White;
                }
            }
        }

        public void ConnectToServer()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 8080);
                Log("Подключено к серверу");

                byte[] buffer = new byte[1024];
                int received = clientSocket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, received);

                if (response == "COLOR:WHITE")
                    playerColor = "WHITE";
                else if (response == "COLOR:BLACK")
                    playerColor = "BLACK";

                Log($"Игроку присвоен цвет: {playerColor}");
                MessageBox.Show($"Вы играете за: {playerColor}");

                listenThread = new Thread(ListenForMoves);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        private void ListenForMoves()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead == 0) break;

                    string move = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log($"Получено сообщение: {move}");

                    if (move == "DRAW_REQUEST")
                    {
                        var result = MessageBox.Show("Соперник предлагает ничью. Принять?", "Ничья", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            SendMove("DRAW_ACCEPT");
                            MessageBox.Show("Ничья принята.");
                            Log("Ничья принята");
                            clientSocket?.Close();
                            Application.Exit();
                        }
                        else
                        {
                            SendMove("DRAW_DECLINE");
                            Log("Ничья отклонена");
                        }
                    }
                    else if (move == "DRAW_DECLINE")
                    {
                        MessageBox.Show("Соперник отказался от ничьи.");
                    }
                    else if (move == "RESIGN")
                    {
                        MessageBox.Show("Соперник сдался. Вы победили.");
                        Log("Соперник сдался");
                        clientSocket?.Close();
                        Application.Exit();
                    }
                    else if (move == "DRAW_ACCEPT")
                    {
                        MessageBox.Show("Соперник принял ничью. Игра окончена.");
                        Log("Соперник принял ничью");
                        clientSocket?.Close();
                        Application.Exit();
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            ApplyOpponentMove(move);
                        }));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Отключено от сервера.");
            }
        }

        private void SendMove(string move)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(move);
            clientSocket.Send(buffer);
            Log($"Отправлен ход: {move}");
            currPlayer = (currPlayer == 1) ? 2 : 1;
        }

        private void ApplyOpponentMove(string move)
        {
            Log($"Применение хода соперника: {move}");

            string[] parts = move.Split(';');
            var from = parts[0].Split(',');
            var to = parts[1].Split(',');

            int fromRow = int.Parse(from[0]);
            int fromCol = int.Parse(from[1]);
            int toRow = int.Parse(to[0]);
            int toCol = int.Parse(to[1]);

            map[toRow, toCol] = map[fromRow, fromCol];
            map[fromRow, fromCol] = 0;

            butts[toRow, toCol].BackgroundImage = butts[fromRow, fromCol].BackgroundImage;
            butts[fromRow, fromCol].BackgroundImage = null;

            currPlayer = (currPlayer == 1) ? 2 : 1;
        }

        private void ResignButton_Click(object sender, EventArgs e)
        {
            SendMove("RESIGN");
            MessageBox.Show("Вы сдались. Поражение.");
            Log("Игрок сдался");
            clientSocket?.Close();
            Application.Exit();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            SendMove("DRAW_REQUEST");
            Log("Игрок предложил ничью");
        }

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch
            {
                
            }
        }
    }
}

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
        }
        private readonly Socket socket;
        private readonly string roomName;
        private readonly string playerRole;
        private Thread waitingThread;
        private bool isWaiting;

        public WaitingForm(string roomName, string playerRole, Socket socket)
        {
            InitializeComponent();
            this.roomName = roomName;
            this.playerRole = playerRole;
            this.socket = socket;
            isWaiting = true;

            RoomNameLabel.Text = $"Комната: {roomName}";
            StatusLabel.Text = playerRole == "PLAYER1"
                ? "Ожидание второго игрока..."
                : "Подключение к игре...";

            waitingThread = new Thread(WaitForGameStart);
            waitingThread.IsBackground = true;
            waitingThread.Start();
        }

        private void WaitForGameStart()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = socket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (response.StartsWith("GAME_STARTED:"))
                {
                    string symbol = response.Substring("GAME_STARTED:".Length);
                    StartGame(symbol);
                }
            }
            catch (Exception ex)
            {
                if (isWaiting)
                {
                    MessageBox.Show($"Ошибка соединения: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ReturnToMainMenu();
                }
            }
        }

        private void StartGame(string symbol)
        {
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            isWaiting = false;
            try
            {
                socket?.Send(Encoding.UTF8.GetBytes($"SURRENDER:{roomName}"));
            }
            catch { }
            finally
            {
                socket?.Close();
                ReturnToMainMenu();
            }
        }

        private void ReturnToMainMenu()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Close();
                }));
            }
            else
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isWaiting = false;
            socket?.Close();
            base.OnFormClosing(e);
        }

    }
}

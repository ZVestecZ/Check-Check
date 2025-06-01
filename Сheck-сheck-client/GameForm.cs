using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class GameForm : Form
    {
        private readonly Socket socket;
        private readonly string roomName;
        private readonly string playerRole;
        private readonly string playerColor;
        private Thread receiveThread;
        private bool isRunning;
        public GameForm(string roomName, string playerRole, Socket socket, string playerColor)
        {
            InitializeComponent();
            this.roomName = roomName;
            this.playerRole = playerRole;
            this.socket = socket;
            this.playerColor = playerColor;
            this.isRunning = true;

            // Настройка интерфейса
            this.Text = $"Игра - {roomName}";
            RoomNameLabel.Text = $"Комната: {roomName}";
            PlayerColorLabel.Text = $"Вы: {playerRole} ({playerColor})";
            TurnLabel.Text = playerRole == "PLAYER1" ? "Ваш ход" : "Ход противника";

            // Запуск потока для получения сообщений
            receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        private void ReceiveMessages()
        {
            try
            {
                while (isRunning)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = socket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (response.StartsWith("MESSAGE:"))
                    {
                        string message = response.Substring("MESSAGE:".Length);
                        UpdateReceivedMessage(message);
                    }
                    else if (response == "OPPONENT_SURRENDERED")
                    {
                        EndGame("Оппонент сдался! Вы победили!");
                    }
                    else if (response.StartsWith("TURN_UPDATE:"))
                    {
                        UpdateTurn(response.Substring("TURN_UPDATE:".Length));
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning) // Если не было запрошено закрытие формы
                {
                    MessageBox.Show($"Ошибка соединения: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ReturnToMainMenu();
                }
            }
        }

        private void UpdateReceivedMessage(string message)
        {
            if (ReceivedMessageLabel.InvokeRequired)
            {
                ReceivedMessageLabel.Invoke(new Action(() =>
                {
                    ReceivedMessageLabel.Text = $"Сообщение: {message}";
                }));
            }
            else
            {
                ReceivedMessageLabel.Text = $"Сообщение: {message}";
            }
        }

        private void UpdateTurn(string turnInfo)
        {
            if (TurnLabel.InvokeRequired)
            {
                TurnLabel.Invoke(new Action(() =>
                {
                    TurnLabel.Text = turnInfo == playerRole ? "Ваш ход" : "Ход противника";
                }));
            }
            else
            {
                TurnLabel.Text = turnInfo == playerRole ? "Ваш ход" : "Ход противника";
            }
        }

        private void EndGame(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(message, "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReturnToMainMenu();
                }));
            }
            else
            {
                MessageBox.Show(message, "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnToMainMenu();
            }
        }

        private void ReturnToMainMenu()
        {
            isRunning = false;
            socket?.Close();

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

        private void SendMessageButton_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                try
                {
                    string message = MessageTextBox.Text;
                    string request = $"SEND_MESSAGE:{roomName}:{message}";
                    socket.Send(Encoding.UTF8.GetBytes(request));

                    MessageStatusLabel.Text = "Сообщение отправлено";
                    MessageTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SurrenderButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите сдаться?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    socket.Send(Encoding.UTF8.GetBytes($"SURRENDER:{roomName}"));
                    ReturnToMainMenu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сдаче: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            socket?.Close();
            base.OnFormClosing(e);
        }
    }
}

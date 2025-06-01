using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class GameCreationForm : Form
    {
        public GameCreationForm()
        {
            InitializeComponent();
        }

        private void CreateGameButton_Click(object sender, EventArgs e)
        {
            string roomName = GameNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(roomName))
            {
                MessageBox.Show("Введите название комнаты", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                socket.Send(Encoding.UTF8.GetBytes($"CREATE_GAME:{roomName}"));

                byte[] buffer = new byte[1024];
                int bytesRead = socket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (response.StartsWith("GAME_CREATED:"))
                {
                    string[] parts = response.Split(':');
                    string createdRoomName = parts[1];
                    string players = parts[2];
                    string playerRole = parts[3];

                    WaitingForm waitingForm = new WaitingForm(createdRoomName, playerRole, socket);
                    waitingForm.Show();
                    this.Hide();
                }
                else if (response.StartsWith("ERROR:"))
                {
                    MessageBox.Show(response.Substring("ERROR:".Length), "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания комнаты: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}


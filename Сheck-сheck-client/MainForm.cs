﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Сheck_сheck_client.Classes;

namespace Сheck_сheck_client
{
    public partial class MainForm : Form
    {
        string name = "Guest";
        string login = "-";
        string rating = "-";
        private System.Windows.Forms.Timer refreshTimer;
        public MainForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            SetupRefreshTimer();
            LoadGameList();
        }

        private void UpdateServersListButton_Click(object sender, EventArgs e)
        {
            LoadGameList();
        }

        private void CreateGameButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите название комнаты", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                socket.Send(Encoding.UTF8.GetBytes($"CREATE_GAME:{name}"));

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

        
        private void GamesDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ConfigureDataGridView()
        {
            GamesDataGridView.AutoGenerateColumns = false;
            GamesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Колонка с названием комнаты
            var nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Name";
            nameColumn.HeaderText = "Название комнаты";
            nameColumn.DataPropertyName = "Name";
            nameColumn.Width = 200;

            // Колонка с количеством игроков
            var playersColumn = new DataGridViewTextBoxColumn();
            playersColumn.Name = "Players";
            playersColumn.HeaderText = "Игроки";
            playersColumn.DataPropertyName = "Players";
            playersColumn.Width = 80;

            // Колонка со статусом
            var statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.Name = "Status";
            statusColumn.HeaderText = "Статус";
            statusColumn.DataPropertyName = "Status";
            statusColumn.Width = 120;

            GamesDataGridView.Columns.AddRange(nameColumn, playersColumn, statusColumn);
        }

        private void SetupRefreshTimer()
        {
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 5000; // 5 сек
            refreshTimer.Tick += (s, e) => LoadGameList();
            refreshTimer.Start();
        }

        private void LoadGameList()
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                    socket.Send(Encoding.UTF8.GetBytes("GET_GAME_LIST"));

                    byte[] buffer = new byte[4096];
                    int bytesRead = socket.Receive(buffer);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (response.StartsWith("GAME_LIST:"))
                    {
                        string gameListData = response.Substring("GAME_LIST:".Length);
                        UpdateGameListUI(gameListData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке списка игр: {ex.Message}");
            }
        }

        private void UpdateGameListUI(string gameListData)
        {
            var games = new List<GameInfo>();
            string[] lines = gameListData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    games.Add(new GameInfo
                    {
                        Name = parts[0],
                        Players = parts[1],
                        IsFull = parts[1] == "2/2",
                        Status = parts[1] == "2/2" ? "Заполнена" : "Ожидание игроков"
                    });
                }
            }

            if (GamesDataGridView.InvokeRequired)
            {
                GamesDataGridView.Invoke(new Action(() =>
                {
                    GamesDataGridView.DataSource = null;
                    GamesDataGridView.DataSource = games;
                    ApplyRowStyling();
                }));
            }
            else
            {
                GamesDataGridView.DataSource = null;
                GamesDataGridView.DataSource = games;
                ApplyRowStyling();
            }
        }

        private void ApplyRowStyling()
        {
            foreach (DataGridViewRow row in GamesDataGridView.Rows)
            {
                var gameInfo = row.DataBoundItem as GameInfo;
                if (gameInfo != null)
                {
                    if (gameInfo.IsFull)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var accountForm = new AccountForm(name, login, rating);
            accountForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "https://chessday.ru/blog/39-pravila-shahmat";
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть ссылку.");
            }
        }
    }
}

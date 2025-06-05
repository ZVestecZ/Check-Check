using System;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using Npgsql;
using System.Text.RegularExpressions;

namespace Сheck_сheck_client
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (login.Length < 6 || login.Length > 20)
            {
                MessageBox.Show("Логин должен содержать от 6 до 20 символов.");
                return;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Логин может содержать только латинские буквы и цифры.");
                return;
            }

            if (password.Length < 6 || password.Length > 20)
            {
                MessageBox.Show("Пароль должен содержать от 6 до 20 символов.");
                return;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^&*()_+]+$"))
            {
                MessageBox.Show("Пароль может содержать латинские буквы, цифры и спецсимволы: !@#$%^&*()_+");
                return;
            }

            try
            {
                string connStr = "Host=localhost;Port=5432;Username=chessuser;Password=chesspass;Database=chessdb";

                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();

                    using (var checkCmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE login = @login", conn))
                    {
                        checkCmd.Parameters.AddWithValue("login", login);
                        var exists = (long)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Логин уже существует");
                            return;
                        }
                    }

                    // Вставка пользователя
                    using (var cmd = new NpgsqlCommand("INSERT INTO users (name, login, password, rating) VALUES (@name, @login, @password, 1000)", conn))
                    {
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("login", login);
                        cmd.Parameters.AddWithValue("password", password);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Регистрация успешна!");

                    // Переход в главное окно
                    if (CheckServerAvailability())
                    {
                        var mainForm = new MainForm();
                        this.Hide();
                        mainForm.ShowDialog();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе: {ex.Message}");
            }







            if (CheckServerAvailability())
            {
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Сервер недоступен");
            }
        }

        private bool CheckServerAvailability()
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IAsyncResult result = socket.BeginConnect("127.0.0.1", 8080, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(1000, true);
                    if (success)
                    {
                        socket.EndConnect(result);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        bool IsValidInput(string input)
        {
            if (input.Length < 6 || input.Length > 20)
                return false;

            string allowedSpecialChars = "!@#$%^&*()_+-=";

            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c) && !allowedSpecialChars.Contains(c))
                    return false;
            }

            return true;
        }
    }
}

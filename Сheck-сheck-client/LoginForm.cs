using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (CheckServerAvailability())
            {
                var mainForm = new MainForm();
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
    }
}

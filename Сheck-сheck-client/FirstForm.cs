using System;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class FirstForm : Form
    {
        public FirstForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegistrationForm();
            this.Hide();
            registrationForm.ShowDialog();
            this.Close();
        }

        private void GuestEntranceButton_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }
    }
}

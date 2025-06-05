using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Сheck_сheck_client
{
    public partial class AccountForm : Form
    {
        private string _name;
        private string _login;
        private string _rating;


        public AccountForm(string name, string login, string rating)
        {
            InitializeComponent();

            var rm = new ResourceManager("Check_check_client.AccountForm", typeof(AccountForm).Assembly);

            _name = name;
            _login = login;
            _rating = rating;

            namelabel.Text = name;
            loginlabel.Text = login;
            ratinglabel.Text = rating;

            ApplyLocalization();
        }

        private void radioButtonRussian_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRussian.Checked)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
                ApplyLocalization();
            }
        }

        private void radioButtonEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnglish.Checked)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                ApplyLocalization();
            }
        }

        private void ApplyLocalization()
        {
            var rm = new ResourceManager("Check_check_client.AccountForm", typeof(AccountForm).Assembly);

            this.labelLogin.Text = string.Format(rm.GetString("LabelLoginText"));
            this.labelName.Text = string.Format(rm.GetString("LabelNameText"));
            this.labelRating.Text = string.Format(rm.GetString("LabelRatingText"));
        }
    }
}

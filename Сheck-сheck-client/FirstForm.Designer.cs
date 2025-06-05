namespace Сheck_сheck_client
{
    partial class FirstForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegistrationButton = new System.Windows.Forms.Button();
            this.GuestEntranceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(270, 80);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(180, 60);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Вход";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegistrationButton
            // 
            this.RegistrationButton.Location = new System.Drawing.Point(270, 231);
            this.RegistrationButton.Name = "RegistrationButton";
            this.RegistrationButton.Size = new System.Drawing.Size(180, 60);
            this.RegistrationButton.TabIndex = 1;
            this.RegistrationButton.Text = "Регистрация";
            this.RegistrationButton.UseVisualStyleBackColor = true;
            this.RegistrationButton.Click += new System.EventHandler(this.RegistrationButton_Click);
            // 
            // GuestEntranceButton
            // 
            this.GuestEntranceButton.Location = new System.Drawing.Point(270, 156);
            this.GuestEntranceButton.Name = "GuestEntranceButton";
            this.GuestEntranceButton.Size = new System.Drawing.Size(180, 60);
            this.GuestEntranceButton.TabIndex = 2;
            this.GuestEntranceButton.Text = "Вход как гость";
            this.GuestEntranceButton.UseVisualStyleBackColor = true;
            this.GuestEntranceButton.Click += new System.EventHandler(this.GuestEntranceButton_Click);
            // 
            // FirstForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(704, 381);
            this.Controls.Add(this.GuestEntranceButton);
            this.Controls.Add(this.RegistrationButton);
            this.Controls.Add(this.LoginButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(720, 420);
            this.MinimumSize = new System.Drawing.Size(720, 420);
            this.Name = "FirstForm";
            this.Text = "FirstForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegistrationButton;
        private System.Windows.Forms.Button GuestEntranceButton;
    }
}


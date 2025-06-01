namespace Сheck_сheck_client
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TurnLabel = new System.Windows.Forms.Label();
            this.MessageStatusLabel = new System.Windows.Forms.Label();
            this.ReceivedMessageLabel = new System.Windows.Forms.Label();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SurrenderButton = new System.Windows.Forms.Button();
            this.RoomNameLabel = new System.Windows.Forms.Label();
            this.PlayerColorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TurnLabel
            // 
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Location = new System.Drawing.Point(324, 21);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(35, 13);
            this.TurnLabel.TabIndex = 0;
            this.TurnLabel.Text = "label1";
            // 
            // MessageStatusLabel
            // 
            this.MessageStatusLabel.AutoSize = true;
            this.MessageStatusLabel.Location = new System.Drawing.Point(156, 109);
            this.MessageStatusLabel.Name = "MessageStatusLabel";
            this.MessageStatusLabel.Size = new System.Drawing.Size(35, 13);
            this.MessageStatusLabel.TabIndex = 1;
            this.MessageStatusLabel.Text = "label2";
            // 
            // ReceivedMessageLabel
            // 
            this.ReceivedMessageLabel.AutoSize = true;
            this.ReceivedMessageLabel.Location = new System.Drawing.Point(598, 109);
            this.ReceivedMessageLabel.Name = "ReceivedMessageLabel";
            this.ReceivedMessageLabel.Size = new System.Drawing.Size(35, 13);
            this.ReceivedMessageLabel.TabIndex = 2;
            this.ReceivedMessageLabel.Text = "label3";
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(272, 291);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(161, 23);
            this.SendMessageButton.TabIndex = 3;
            this.SendMessageButton.Text = "Отправить сообщение";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click_1);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(315, 139);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(100, 20);
            this.MessageTextBox.TabIndex = 4;
            // 
            // SurrenderButton
            // 
            this.SurrenderButton.Location = new System.Drawing.Point(557, 388);
            this.SurrenderButton.Name = "SurrenderButton";
            this.SurrenderButton.Size = new System.Drawing.Size(75, 23);
            this.SurrenderButton.TabIndex = 5;
            this.SurrenderButton.Text = "Сдаться";
            this.SurrenderButton.UseVisualStyleBackColor = true;
            this.SurrenderButton.Click += new System.EventHandler(this.SurrenderButton_Click);
            // 
            // RoomNameLabel
            // 
            this.RoomNameLabel.AutoSize = true;
            this.RoomNameLabel.Location = new System.Drawing.Point(13, 20);
            this.RoomNameLabel.Name = "RoomNameLabel";
            this.RoomNameLabel.Size = new System.Drawing.Size(35, 13);
            this.RoomNameLabel.TabIndex = 6;
            this.RoomNameLabel.Text = "label1";
            // 
            // PlayerColorLabel
            // 
            this.PlayerColorLabel.AutoSize = true;
            this.PlayerColorLabel.Location = new System.Drawing.Point(13, 52);
            this.PlayerColorLabel.Name = "PlayerColorLabel";
            this.PlayerColorLabel.Size = new System.Drawing.Size(35, 13);
            this.PlayerColorLabel.TabIndex = 7;
            this.PlayerColorLabel.Text = "label2";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PlayerColorLabel);
            this.Controls.Add(this.RoomNameLabel);
            this.Controls.Add(this.SurrenderButton);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.ReceivedMessageLabel);
            this.Controls.Add(this.MessageStatusLabel);
            this.Controls.Add(this.TurnLabel);
            this.Name = "GameForm";
            this.Text = "Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TurnLabel;
        private System.Windows.Forms.Label MessageStatusLabel;
        private System.Windows.Forms.Label ReceivedMessageLabel;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Button SurrenderButton;
        private System.Windows.Forms.Label RoomNameLabel;
        private System.Windows.Forms.Label PlayerColorLabel;
    }
}
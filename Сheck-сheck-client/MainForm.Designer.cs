namespace Сheck_сheck_client
{
    partial class MainForm
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
            this.UpdateServersListButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CreateGameButton = new System.Windows.Forms.Button();
            this.GamesDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GamesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateServersListButton
            // 
            this.UpdateServersListButton.Location = new System.Drawing.Point(624, 13);
            this.UpdateServersListButton.Name = "UpdateServersListButton";
            this.UpdateServersListButton.Size = new System.Drawing.Size(163, 23);
            this.UpdateServersListButton.TabIndex = 1;
            this.UpdateServersListButton.Text = "Обновить список серверов";
            this.UpdateServersListButton.UseVisualStyleBackColor = true;
            this.UpdateServersListButton.Click += new System.EventHandler(this.UpdateServersListButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Аккаунт";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CreateGameButton
            // 
            this.CreateGameButton.Location = new System.Drawing.Point(13, 415);
            this.CreateGameButton.Name = "CreateGameButton";
            this.CreateGameButton.Size = new System.Drawing.Size(101, 23);
            this.CreateGameButton.TabIndex = 3;
            this.CreateGameButton.Text = "Создать игру";
            this.CreateGameButton.UseVisualStyleBackColor = true;
            this.CreateGameButton.Click += new System.EventHandler(this.CreateGameButton_Click);
            // 
            // GamesDataGridView
            // 
            this.GamesDataGridView.AllowUserToDeleteRows = false;
            this.GamesDataGridView.AllowUserToResizeColumns = false;
            this.GamesDataGridView.AllowUserToResizeRows = false;
            this.GamesDataGridView.Location = new System.Drawing.Point(277, 42);
            this.GamesDataGridView.MultiSelect = false;
            this.GamesDataGridView.Name = "GamesDataGridView";
            this.GamesDataGridView.ReadOnly = true;
            this.GamesDataGridView.Size = new System.Drawing.Size(510, 396);
            this.GamesDataGridView.TabIndex = 0;
            this.GamesDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GamesDataGridView_CellContentDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GamesDataGridView);
            this.Controls.Add(this.CreateGameButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.UpdateServersListButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.GamesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button UpdateServersListButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button CreateGameButton;
        private System.Windows.Forms.DataGridView GamesDataGridView;
    }
}
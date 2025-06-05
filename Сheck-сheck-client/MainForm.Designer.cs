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
            this.button1 = new System.Windows.Forms.Button();
            this.CreateGameButton = new System.Windows.Forms.Button();
            this.GamesDataGridView = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GamesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(607, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Аккаунт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CreateGameButton
            // 
            this.CreateGameButton.Location = new System.Drawing.Point(70, 203);
            this.CreateGameButton.Name = "CreateGameButton";
            this.CreateGameButton.Size = new System.Drawing.Size(101, 23);
            this.CreateGameButton.TabIndex = 3;
            this.CreateGameButton.Text = "Создать";
            this.CreateGameButton.UseVisualStyleBackColor = true;
            this.CreateGameButton.Click += new System.EventHandler(this.CreateGameButton_Click);
            // 
            // GamesDataGridView
            // 
            this.GamesDataGridView.AllowUserToDeleteRows = false;
            this.GamesDataGridView.AllowUserToResizeColumns = false;
            this.GamesDataGridView.AllowUserToResizeRows = false;
            this.GamesDataGridView.Location = new System.Drawing.Point(277, 60);
            this.GamesDataGridView.MultiSelect = false;
            this.GamesDataGridView.Name = "GamesDataGridView";
            this.GamesDataGridView.ReadOnly = true;
            this.GamesDataGridView.Size = new System.Drawing.Size(510, 378);
            this.GamesDataGridView.TabIndex = 0;
            this.GamesDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GamesDataGridView_CellContentDoubleClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(712, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Правила";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.GamesDataGridView);
            this.Controls.Add(this.CreateGameButton);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.GamesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button CreateGameButton;
        private System.Windows.Forms.DataGridView GamesDataGridView;
        private System.Windows.Forms.Button button2;
    }
}
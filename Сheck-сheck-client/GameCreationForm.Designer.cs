namespace Сheck_сheck_client
{
    partial class GameCreationForm
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
            this.GameNameTextBox = new System.Windows.Forms.TextBox();
            this.CreateGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameNameTextBox
            // 
            this.GameNameTextBox.Location = new System.Drawing.Point(13, 13);
            this.GameNameTextBox.Name = "GameNameTextBox";
            this.GameNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.GameNameTextBox.TabIndex = 0;
            // 
            // CreateGameButton
            // 
            this.CreateGameButton.Location = new System.Drawing.Point(13, 39);
            this.CreateGameButton.Name = "CreateGameButton";
            this.CreateGameButton.Size = new System.Drawing.Size(100, 23);
            this.CreateGameButton.TabIndex = 1;
            this.CreateGameButton.Text = "Создать";
            this.CreateGameButton.UseVisualStyleBackColor = true;
            this.CreateGameButton.Click += new System.EventHandler(this.CreateGameButton_Click);
            // 
            // GameCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateGameButton);
            this.Controls.Add(this.GameNameTextBox);
            this.Name = "GameCreationForm";
            this.Text = "GameCreationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GameNameTextBox;
        private System.Windows.Forms.Button CreateGameButton;
    }
}
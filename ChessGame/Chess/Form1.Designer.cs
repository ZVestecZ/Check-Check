namespace Chess
{
    partial class Form1
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
            this.resignButton = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resignButton
            // 
            this.resignButton.Location = new System.Drawing.Point(422, 260);
            this.resignButton.Name = "resignButton";
            this.resignButton.Size = new System.Drawing.Size(75, 23);
            this.resignButton.TabIndex = 0;
            this.resignButton.Text = "Сдаться";
            this.resignButton.UseVisualStyleBackColor = true;
            this.resignButton.Click += new System.EventHandler(this.ResignButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(528, 260);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 1;
            this.drawButton.Text = "Ничья";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(624, 401);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.resignButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 440);
            this.MinimumSize = new System.Drawing.Size(640, 440);
            this.Name = "Form1";
            this.Text = "Check-check";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button resignButton;
        private System.Windows.Forms.Button drawButton;
    }
}


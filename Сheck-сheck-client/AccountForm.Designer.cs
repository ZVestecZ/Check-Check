namespace Сheck_сheck_client
{
    partial class AccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            this.labelName = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelRating = new System.Windows.Forms.Label();
            this.radioButtonRussian = new System.Windows.Forms.RadioButton();
            this.radioButtonEnglish = new System.Windows.Forms.RadioButton();
            this.namelabel = new System.Windows.Forms.Label();
            this.loginlabel = new System.Windows.Forms.Label();
            this.ratinglabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelLogin
            // 
            resources.ApplyResources(this.labelLogin, "labelLogin");
            this.labelLogin.Name = "labelLogin";
            // 
            // labelRating
            // 
            resources.ApplyResources(this.labelRating, "labelRating");
            this.labelRating.Name = "labelRating";
            // 
            // radioButtonRussian
            // 
            resources.ApplyResources(this.radioButtonRussian, "radioButtonRussian");
            this.radioButtonRussian.Name = "radioButtonRussian";
            this.radioButtonRussian.TabStop = true;
            this.radioButtonRussian.UseVisualStyleBackColor = true;
            this.radioButtonRussian.CheckedChanged += new System.EventHandler(this.radioButtonRussian_CheckedChanged);
            // 
            // radioButtonEnglish
            // 
            resources.ApplyResources(this.radioButtonEnglish, "radioButtonEnglish");
            this.radioButtonEnglish.Name = "radioButtonEnglish";
            this.radioButtonEnglish.TabStop = true;
            this.radioButtonEnglish.UseVisualStyleBackColor = true;
            this.radioButtonEnglish.CheckedChanged += new System.EventHandler(this.radioButtonEnglish_CheckedChanged);
            // 
            // namelabel
            // 
            resources.ApplyResources(this.namelabel, "namelabel");
            this.namelabel.Name = "namelabel";
            // 
            // loginlabel
            // 
            resources.ApplyResources(this.loginlabel, "loginlabel");
            this.loginlabel.Name = "loginlabel";
            // 
            // ratinglabel
            // 
            resources.ApplyResources(this.ratinglabel, "ratinglabel");
            this.ratinglabel.Name = "ratinglabel";
            // 
            // AccountForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.ratinglabel);
            this.Controls.Add(this.loginlabel);
            this.Controls.Add(this.namelabel);
            this.Controls.Add(this.radioButtonEnglish);
            this.Controls.Add(this.radioButtonRussian);
            this.Controls.Add(this.labelRating);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelName);
            this.MaximizeBox = false;
            this.Name = "AccountForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelRating;
        private System.Windows.Forms.RadioButton radioButtonRussian;
        private System.Windows.Forms.RadioButton radioButtonEnglish;
        private System.Windows.Forms.Label namelabel;
        private System.Windows.Forms.Label loginlabel;
        private System.Windows.Forms.Label ratinglabel;
    }
}
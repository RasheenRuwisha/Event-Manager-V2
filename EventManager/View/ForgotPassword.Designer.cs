namespace EventManager.View
{
    partial class ForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPassword));
            this.reset_panel = new System.Windows.Forms.Panel();
            this.lbl_confirmpasword = new System.Windows.Forms.Label();
            this.txt_confirmpasword = new System.Windows.Forms.TextBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.email_panel = new System.Windows.Forms.Panel();
            this.btn_send_mail = new System.Windows.Forms.Button();
            this.lbl_email = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_otp = new System.Windows.Forms.TextBox();
            this.verification_panel = new System.Windows.Forms.Panel();
            this.txt_proceed = new System.Windows.Forms.Button();
            this.lbl_login = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.reset_panel.SuspendLayout();
            this.email_panel.SuspendLayout();
            this.verification_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // reset_panel
            // 
            this.reset_panel.Controls.Add(this.lbl_confirmpasword);
            this.reset_panel.Controls.Add(this.txt_confirmpasword);
            this.reset_panel.Controls.Add(this.btn_reset);
            this.reset_panel.Controls.Add(this.lbl_password);
            this.reset_panel.Controls.Add(this.txt_password);
            this.reset_panel.Location = new System.Drawing.Point(0, 202);
            this.reset_panel.Name = "reset_panel";
            this.reset_panel.Size = new System.Drawing.Size(396, 314);
            this.reset_panel.TabIndex = 20;
            // 
            // lbl_confirmpasword
            // 
            this.lbl_confirmpasword.AutoSize = true;
            this.lbl_confirmpasword.BackColor = System.Drawing.Color.Transparent;
            this.lbl_confirmpasword.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_confirmpasword.Location = new System.Drawing.Point(18, 122);
            this.lbl_confirmpasword.Name = "lbl_confirmpasword";
            this.lbl_confirmpasword.Size = new System.Drawing.Size(91, 13);
            this.lbl_confirmpasword.TabIndex = 11;
            this.lbl_confirmpasword.Text = "Confirm Password";
            // 
            // txt_confirmpasword
            // 
            this.txt_confirmpasword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_confirmpasword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_confirmpasword.ForeColor = System.Drawing.Color.White;
            this.txt_confirmpasword.Location = new System.Drawing.Point(21, 139);
            this.txt_confirmpasword.MaxLength = 30;
            this.txt_confirmpasword.Name = "txt_confirmpasword";
            this.txt_confirmpasword.PasswordChar = '*';
            this.txt_confirmpasword.Size = new System.Drawing.Size(150, 24);
            this.txt_confirmpasword.TabIndex = 8;
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.Transparent;
            this.btn_reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.btn_reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_reset.Location = new System.Drawing.Point(21, 191);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(200, 31);
            this.btn_reset.TabIndex = 9;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_password.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_password.Location = new System.Drawing.Point(17, 65);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 8;
            this.lbl_password.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.ForeColor = System.Drawing.Color.White;
            this.txt_password.Location = new System.Drawing.Point(20, 82);
            this.txt_password.MaxLength = 30;
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(150, 24);
            this.txt_password.TabIndex = 7;
            // 
            // email_panel
            // 
            this.email_panel.Controls.Add(this.btn_send_mail);
            this.email_panel.Controls.Add(this.lbl_email);
            this.email_panel.Controls.Add(this.txt_email);
            this.email_panel.Location = new System.Drawing.Point(0, 202);
            this.email_panel.Name = "email_panel";
            this.email_panel.Size = new System.Drawing.Size(396, 314);
            this.email_panel.TabIndex = 18;
            // 
            // btn_send_mail
            // 
            this.btn_send_mail.BackColor = System.Drawing.Color.Transparent;
            this.btn_send_mail.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.btn_send_mail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_send_mail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send_mail.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_send_mail.Location = new System.Drawing.Point(20, 124);
            this.btn_send_mail.Name = "btn_send_mail";
            this.btn_send_mail.Size = new System.Drawing.Size(200, 31);
            this.btn_send_mail.TabIndex = 9;
            this.btn_send_mail.Text = "Send Email";
            this.btn_send_mail.UseVisualStyleBackColor = false;
            this.btn_send_mail.Click += new System.EventHandler(this.btn_send_mail_Click);
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.BackColor = System.Drawing.Color.Transparent;
            this.lbl_email.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_email.Location = new System.Drawing.Point(17, 65);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(32, 13);
            this.lbl_email.TabIndex = 8;
            this.lbl_email.Text = "Email";
            // 
            // txt_email
            // 
            this.txt_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_email.ForeColor = System.Drawing.Color.White;
            this.txt_email.Location = new System.Drawing.Point(20, 82);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(150, 24);
            this.txt_email.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(17, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Verification Code";
            // 
            // txt_otp
            // 
            this.txt_otp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_otp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_otp.ForeColor = System.Drawing.Color.White;
            this.txt_otp.Location = new System.Drawing.Point(20, 91);
            this.txt_otp.Name = "txt_otp";
            this.txt_otp.Size = new System.Drawing.Size(150, 24);
            this.txt_otp.TabIndex = 7;
            // 
            // verification_panel
            // 
            this.verification_panel.Controls.Add(this.txt_proceed);
            this.verification_panel.Controls.Add(this.label1);
            this.verification_panel.Controls.Add(this.txt_otp);
            this.verification_panel.Location = new System.Drawing.Point(0, 202);
            this.verification_panel.Name = "verification_panel";
            this.verification_panel.Size = new System.Drawing.Size(404, 322);
            this.verification_panel.TabIndex = 19;
            // 
            // txt_proceed
            // 
            this.txt_proceed.BackColor = System.Drawing.Color.Transparent;
            this.txt_proceed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.txt_proceed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.txt_proceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txt_proceed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_proceed.Location = new System.Drawing.Point(20, 134);
            this.txt_proceed.Name = "txt_proceed";
            this.txt_proceed.Size = new System.Drawing.Size(200, 31);
            this.txt_proceed.TabIndex = 11;
            this.txt_proceed.Text = "Proceed";
            this.txt_proceed.UseVisualStyleBackColor = false;
            this.txt_proceed.Click += new System.EventHandler(this.txt_proceed_Click);
            // 
            // lbl_login
            // 
            this.lbl_login.AutoSize = true;
            this.lbl_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.lbl_login.Location = new System.Drawing.Point(311, 523);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(73, 13);
            this.lbl_login.TabIndex = 21;
            this.lbl_login.Text = "Back to Login";
            this.lbl_login.Click += new System.EventHandler(this.lbl_login_Click);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.Location = new System.Drawing.Point(117, 26);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(150, 150);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 17;
            this.logo.TabStop = false;
            // 
            // ForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(397, 563);
            this.Controls.Add(this.reset_panel);
            this.Controls.Add(this.email_panel);
            this.Controls.Add(this.verification_panel);
            this.Controls.Add(this.lbl_login);
            this.Controls.Add(this.logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.reset_panel.ResumeLayout(false);
            this.reset_panel.PerformLayout();
            this.email_panel.ResumeLayout(false);
            this.email_panel.PerformLayout();
            this.verification_panel.ResumeLayout(false);
            this.verification_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel reset_panel;
        private System.Windows.Forms.Label lbl_confirmpasword;
        private System.Windows.Forms.TextBox txt_confirmpasword;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Panel email_panel;
        private System.Windows.Forms.Button btn_send_mail;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_otp;
        private System.Windows.Forms.Panel verification_panel;
        private System.Windows.Forms.Button txt_proceed;
        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.PictureBox logo;
    }
}
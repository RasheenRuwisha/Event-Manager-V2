namespace EventManager
{
    partial class Login
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
            this.pb_logo = new System.Windows.Forms.PictureBox();
            this.pnl_error = new System.Windows.Forms.Panel();
            this.lbl_forgotpassword = new System.Windows.Forms.Label();
            this.lbl_register = new System.Windows.Forms.Label();
            this.lbl_noaccount = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.cb_rememberme = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_logo
            // 
            this.pb_logo.BackColor = System.Drawing.Color.Transparent;
            this.pb_logo.Location = new System.Drawing.Point(116, 12);
            this.pb_logo.Name = "pb_logo";
            this.pb_logo.Size = new System.Drawing.Size(150, 150);
            this.pb_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_logo.TabIndex = 2;
            this.pb_logo.TabStop = false;
            // 
            // pnl_error
            // 
            this.pnl_error.Location = new System.Drawing.Point(0, 181);
            this.pnl_error.Name = "pnl_error";
            this.pnl_error.Size = new System.Drawing.Size(396, 50);
            this.pnl_error.TabIndex = 11;
            // 
            // lbl_forgotpassword
            // 
            this.lbl_forgotpassword.AutoSize = true;
            this.lbl_forgotpassword.BackColor = System.Drawing.Color.Transparent;
            this.lbl_forgotpassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_forgotpassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.lbl_forgotpassword.Location = new System.Drawing.Point(153, 465);
            this.lbl_forgotpassword.Name = "lbl_forgotpassword";
            this.lbl_forgotpassword.Size = new System.Drawing.Size(92, 13);
            this.lbl_forgotpassword.TabIndex = 19;
            this.lbl_forgotpassword.Text = "Forgot Password !";
            this.lbl_forgotpassword.Click += new System.EventHandler(this.lbl_forgotpassword_Click);
            // 
            // lbl_register
            // 
            this.lbl_register.AutoSize = true;
            this.lbl_register.BackColor = System.Drawing.Color.Transparent;
            this.lbl_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_register.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.lbl_register.Location = new System.Drawing.Point(221, 487);
            this.lbl_register.Name = "lbl_register";
            this.lbl_register.Size = new System.Drawing.Size(77, 13);
            this.lbl_register.TabIndex = 18;
            this.lbl_register.Text = "Register Now !";
            this.lbl_register.Click += new System.EventHandler(this.lbl_register_Click);
            // 
            // lbl_noaccount
            // 
            this.lbl_noaccount.AutoSize = true;
            this.lbl_noaccount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_noaccount.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_noaccount.Location = new System.Drawing.Point(101, 487);
            this.lbl_noaccount.Name = "lbl_noaccount";
            this.lbl_noaccount.Size = new System.Drawing.Size(122, 13);
            this.lbl_noaccount.TabIndex = 17;
            this.lbl_noaccount.Text = "Don\'t have an account?";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_password.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_password.Location = new System.Drawing.Point(114, 319);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 16;
            this.lbl_password.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_password.ForeColor = System.Drawing.Color.White;
            this.txt_password.Location = new System.Drawing.Point(117, 337);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(150, 24);
            this.txt_password.TabIndex = 15;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.BackColor = System.Drawing.Color.Transparent;
            this.lbl_email.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_email.Location = new System.Drawing.Point(114, 259);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(32, 13);
            this.lbl_email.TabIndex = 14;
            this.lbl_email.Text = "Email";
            // 
            // txt_email
            // 
            this.txt_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_email.ForeColor = System.Drawing.Color.White;
            this.txt_email.Location = new System.Drawing.Point(117, 276);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(150, 24);
            this.txt_email.TabIndex = 13;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Transparent;
            this.btn_login.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.btn_login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_login.Location = new System.Drawing.Point(98, 404);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(200, 33);
            this.btn_login.TabIndex = 12;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // cb_rememberme
            // 
            this.cb_rememberme.AutoSize = true;
            this.cb_rememberme.ForeColor = System.Drawing.Color.White;
            this.cb_rememberme.Location = new System.Drawing.Point(156, 381);
            this.cb_rememberme.Name = "cb_rememberme";
            this.cb_rememberme.Size = new System.Drawing.Size(95, 17);
            this.cb_rememberme.TabIndex = 20;
            this.cb_rememberme.Text = "Remember Me";
            this.cb_rememberme.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(397, 563);
            this.Controls.Add(this.cb_rememberme);
            this.Controls.Add(this.lbl_forgotpassword);
            this.Controls.Add(this.lbl_register);
            this.Controls.Add(this.lbl_noaccount);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.pnl_error);
            this.Controls.Add(this.pb_logo);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_logo;
        private System.Windows.Forms.Panel pnl_error;
        private System.Windows.Forms.Label lbl_forgotpassword;
        private System.Windows.Forms.Label lbl_register;
        private System.Windows.Forms.Label lbl_noaccount;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.CheckBox cb_rememberme;
    }
}
namespace EventManager.View
{
    partial class Register
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
            this.lbl_login = new System.Windows.Forms.Label();
            this.btn_register = new System.Windows.Forms.Button();
            this.lbl_confirmpassword = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_lastname = new System.Windows.Forms.Label();
            this.lbl_firstname = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.txt_confirmpassword = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_phone = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.pb_logo = new System.Windows.Forms.PictureBox();
            this.cpb_userimage = new EventManager.UIComponents.CircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpb_userimage)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_login
            // 
            this.lbl_login.AutoSize = true;
            this.lbl_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.lbl_login.Location = new System.Drawing.Point(651, 495);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(73, 13);
            this.lbl_login.TabIndex = 8;
            this.lbl_login.Text = "Back to Login";
            this.lbl_login.Click += new System.EventHandler(this.lbl_login_Click);
            // 
            // btn_register
            // 
            this.btn_register.BackColor = System.Drawing.Color.Transparent;
            this.btn_register.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.btn_register.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_register.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_register.Location = new System.Drawing.Point(255, 474);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(200, 31);
            this.btn_register.TabIndex = 7;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = false;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // lbl_confirmpassword
            // 
            this.lbl_confirmpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_confirmpassword.AutoSize = true;
            this.lbl_confirmpassword.ForeColor = System.Drawing.Color.White;
            this.lbl_confirmpassword.Location = new System.Drawing.Point(389, 402);
            this.lbl_confirmpassword.Name = "lbl_confirmpassword";
            this.lbl_confirmpassword.Size = new System.Drawing.Size(91, 13);
            this.lbl_confirmpassword.TabIndex = 28;
            this.lbl_confirmpassword.Text = "Confirm Password";
            // 
            // lbl_password
            // 
            this.lbl_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_password.AutoSize = true;
            this.lbl_password.ForeColor = System.Drawing.Color.White;
            this.lbl_password.Location = new System.Drawing.Point(100, 402);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(53, 13);
            this.lbl_password.TabIndex = 27;
            this.lbl_password.Text = "Password";
            // 
            // lbl_email
            // 
            this.lbl_email.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_email.AutoSize = true;
            this.lbl_email.ForeColor = System.Drawing.Color.White;
            this.lbl_email.Location = new System.Drawing.Point(100, 345);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(32, 13);
            this.lbl_email.TabIndex = 26;
            this.lbl_email.Text = "Email";
            // 
            // lbl_lastname
            // 
            this.lbl_lastname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_lastname.AutoSize = true;
            this.lbl_lastname.ForeColor = System.Drawing.Color.White;
            this.lbl_lastname.Location = new System.Drawing.Point(389, 345);
            this.lbl_lastname.Name = "lbl_lastname";
            this.lbl_lastname.Size = new System.Drawing.Size(38, 13);
            this.lbl_lastname.TabIndex = 25;
            this.lbl_lastname.Text = "Phone";
            // 
            // lbl_firstname
            // 
            this.lbl_firstname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_firstname.AutoSize = true;
            this.lbl_firstname.ForeColor = System.Drawing.Color.White;
            this.lbl_firstname.Location = new System.Drawing.Point(389, 281);
            this.lbl_firstname.Name = "lbl_firstname";
            this.lbl_firstname.Size = new System.Drawing.Size(35, 13);
            this.lbl_firstname.TabIndex = 24;
            this.lbl_firstname.Text = "Name";
            // 
            // lbl_username
            // 
            this.lbl_username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_username.AutoSize = true;
            this.lbl_username.ForeColor = System.Drawing.Color.White;
            this.lbl_username.Location = new System.Drawing.Point(100, 281);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(55, 13);
            this.lbl_username.TabIndex = 23;
            this.lbl_username.Text = "Username";
            // 
            // txt_confirmpassword
            // 
            this.txt_confirmpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_confirmpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_confirmpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_confirmpassword.ForeColor = System.Drawing.Color.White;
            this.txt_confirmpassword.Location = new System.Drawing.Point(390, 418);
            this.txt_confirmpassword.MaxLength = 20;
            this.txt_confirmpassword.Name = "txt_confirmpassword";
            this.txt_confirmpassword.PasswordChar = '*';
            this.txt_confirmpassword.Size = new System.Drawing.Size(250, 24);
            this.txt_confirmpassword.TabIndex = 6;
            // 
            // txt_password
            // 
            this.txt_password.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_password.ForeColor = System.Drawing.Color.White;
            this.txt_password.Location = new System.Drawing.Point(102, 418);
            this.txt_password.MaxLength = 20;
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(250, 24);
            this.txt_password.TabIndex = 5;
            // 
            // txt_phone
            // 
            this.txt_phone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_phone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_phone.ForeColor = System.Drawing.Color.White;
            this.txt_phone.Location = new System.Drawing.Point(390, 361);
            this.txt_phone.MaxLength = 15;
            this.txt_phone.Name = "txt_phone";
            this.txt_phone.Size = new System.Drawing.Size(250, 24);
            this.txt_phone.TabIndex = 4;
            // 
            // txt_name
            // 
            this.txt_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.ForeColor = System.Drawing.Color.White;
            this.txt_name.Location = new System.Drawing.Point(390, 298);
            this.txt_name.MaxLength = 50;
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(250, 24);
            this.txt_name.TabIndex = 2;
            // 
            // txt_email
            // 
            this.txt_email.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_email.ForeColor = System.Drawing.Color.White;
            this.txt_email.Location = new System.Drawing.Point(102, 361);
            this.txt_email.MaxLength = 75;
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(250, 24);
            this.txt_email.TabIndex = 3;
            // 
            // txt_username
            // 
            this.txt_username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_username.ForeColor = System.Drawing.Color.White;
            this.txt_username.Location = new System.Drawing.Point(102, 298);
            this.txt_username.MaxLength = 30;
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(250, 24);
            this.txt_username.TabIndex = 1;
            // 
            // pb_logo
            // 
            this.pb_logo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pb_logo.BackColor = System.Drawing.Color.Transparent;
            this.pb_logo.Image = global::EventManager.Properties.Resources.logo2;
            this.pb_logo.Location = new System.Drawing.Point(197, 12);
            this.pb_logo.Name = "pb_logo";
            this.pb_logo.Size = new System.Drawing.Size(332, 100);
            this.pb_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_logo.TabIndex = 32;
            this.pb_logo.TabStop = false;
            // 
            // cpb_userimage
            // 
            this.cpb_userimage.BackColor = System.Drawing.Color.Transparent;
            this.cpb_userimage.Location = new System.Drawing.Point(310, 148);
            this.cpb_userimage.Name = "cpb_userimage";
            this.cpb_userimage.Size = new System.Drawing.Size(100, 100);
            this.cpb_userimage.TabIndex = 33;
            this.cpb_userimage.TabStop = false;
            this.cpb_userimage.Click += new System.EventHandler(this.cpb_userimage_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(734, 530);
            this.Controls.Add(this.cpb_userimage);
            this.Controls.Add(this.pb_logo);
            this.Controls.Add(this.lbl_login);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.lbl_confirmpassword);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.lbl_lastname);
            this.Controls.Add(this.lbl_firstname);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.txt_confirmpassword);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_phone);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_username);
            this.MinimumSize = new System.Drawing.Size(750, 569);
            this.Name = "Register";
            this.Text = "Register";
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpb_userimage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Label lbl_confirmpassword;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_lastname;
        private System.Windows.Forms.Label lbl_firstname;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.TextBox txt_confirmpassword;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_phone;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.PictureBox pb_logo;
        private UIComponents.CircularPictureBox cpb_userimage;
    }
}
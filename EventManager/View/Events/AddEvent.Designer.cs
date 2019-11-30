namespace EventManager.View.Events
{
    partial class AddEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEvent));
            this.cmb_repeattype = new System.Windows.Forms.ComboBox();
            this.lbl_repeat = new System.Windows.Forms.Label();
            this.rb_appointment = new System.Windows.Forms.RadioButton();
            this.rb_task = new System.Windows.Forms.RadioButton();
            this.dtp_endtime = new System.Windows.Forms.DateTimePicker();
            this.dtp_starttime = new System.Windows.Forms.DateTimePicker();
            this.cmb_contacts = new System.Windows.Forms.ComboBox();
            this.dtp_enddate = new System.Windows.Forms.DateTimePicker();
            this.dtp_startdate = new System.Windows.Forms.DateTimePicker();
            this.btn_save = new System.Windows.Forms.Button();
            this.lbl_collaborators = new System.Windows.Forms.Label();
            this.lbl_header = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_endtime = new System.Windows.Forms.Label();
            this.lbl_startadate = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.pb_close = new System.Windows.Forms.PictureBox();
            this.lbl_addcollab = new System.Windows.Forms.Button();
            this.btn_removecollab = new System.Windows.Forms.Button();
            this.cmb_evetncollab = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_close)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_repeattype
            // 
            this.cmb_repeattype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmb_repeattype.ForeColor = System.Drawing.Color.White;
            this.cmb_repeattype.FormattingEnabled = true;
            this.cmb_repeattype.Items.AddRange(new object[] {
            "None",
            "Daily",
            "Weekly",
            "Monthly"});
            this.cmb_repeattype.Location = new System.Drawing.Point(43, 474);
            this.cmb_repeattype.Name = "cmb_repeattype";
            this.cmb_repeattype.Size = new System.Drawing.Size(250, 21);
            this.cmb_repeattype.TabIndex = 75;
            this.cmb_repeattype.SelectedIndexChanged += new System.EventHandler(this.cmb_repeattype_SelectedIndexChanged);
            // 
            // lbl_repeat
            // 
            this.lbl_repeat.AutoSize = true;
            this.lbl_repeat.ForeColor = System.Drawing.Color.White;
            this.lbl_repeat.Location = new System.Drawing.Point(41, 449);
            this.lbl_repeat.Name = "lbl_repeat";
            this.lbl_repeat.Size = new System.Drawing.Size(42, 13);
            this.lbl_repeat.TabIndex = 74;
            this.lbl_repeat.Text = "Repeat";
            // 
            // rb_appointment
            // 
            this.rb_appointment.AutoSize = true;
            this.rb_appointment.ForeColor = System.Drawing.Color.White;
            this.rb_appointment.Location = new System.Drawing.Point(128, 354);
            this.rb_appointment.Name = "rb_appointment";
            this.rb_appointment.Size = new System.Drawing.Size(84, 17);
            this.rb_appointment.TabIndex = 73;
            this.rb_appointment.Text = "Appointment";
            this.rb_appointment.UseVisualStyleBackColor = true;
            this.rb_appointment.CheckedChanged += new System.EventHandler(this.rb_appointment_CheckedChanged);
            // 
            // rb_task
            // 
            this.rb_task.AutoSize = true;
            this.rb_task.Checked = true;
            this.rb_task.ForeColor = System.Drawing.Color.White;
            this.rb_task.Location = new System.Drawing.Point(43, 354);
            this.rb_task.Name = "rb_task";
            this.rb_task.Size = new System.Drawing.Size(49, 17);
            this.rb_task.TabIndex = 72;
            this.rb_task.TabStop = true;
            this.rb_task.Text = "Task";
            this.rb_task.UseVisualStyleBackColor = true;
            // 
            // dtp_endtime
            // 
            this.dtp_endtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_endtime.Location = new System.Drawing.Point(299, 317);
            this.dtp_endtime.Name = "dtp_endtime";
            this.dtp_endtime.ShowUpDown = true;
            this.dtp_endtime.Size = new System.Drawing.Size(92, 20);
            this.dtp_endtime.TabIndex = 71;
            // 
            // dtp_starttime
            // 
            this.dtp_starttime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_starttime.Location = new System.Drawing.Point(299, 262);
            this.dtp_starttime.Name = "dtp_starttime";
            this.dtp_starttime.ShowUpDown = true;
            this.dtp_starttime.Size = new System.Drawing.Size(92, 20);
            this.dtp_starttime.TabIndex = 70;
            // 
            // cmb_contacts
            // 
            this.cmb_contacts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmb_contacts.ForeColor = System.Drawing.Color.White;
            this.cmb_contacts.FormattingEnabled = true;
            this.cmb_contacts.Items.AddRange(new object[] {
            "Loading...."});
            this.cmb_contacts.Location = new System.Drawing.Point(43, 413);
            this.cmb_contacts.Name = "cmb_contacts";
            this.cmb_contacts.Size = new System.Drawing.Size(250, 21);
            this.cmb_contacts.TabIndex = 69;
            // 
            // dtp_enddate
            // 
            this.dtp_enddate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.dtp_enddate.Location = new System.Drawing.Point(44, 317);
            this.dtp_enddate.Name = "dtp_enddate";
            this.dtp_enddate.Size = new System.Drawing.Size(249, 20);
            this.dtp_enddate.TabIndex = 68;
            // 
            // dtp_startdate
            // 
            this.dtp_startdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.dtp_startdate.Location = new System.Drawing.Point(44, 262);
            this.dtp_startdate.Name = "dtp_startdate";
            this.dtp_startdate.Size = new System.Drawing.Size(249, 20);
            this.dtp_startdate.TabIndex = 67;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Transparent;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_save.Location = new System.Drawing.Point(44, 527);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(200, 29);
            this.btn_save.TabIndex = 65;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_collaborators
            // 
            this.lbl_collaborators.AutoSize = true;
            this.lbl_collaborators.ForeColor = System.Drawing.Color.White;
            this.lbl_collaborators.Location = new System.Drawing.Point(41, 388);
            this.lbl_collaborators.Name = "lbl_collaborators";
            this.lbl_collaborators.Size = new System.Drawing.Size(68, 13);
            this.lbl_collaborators.TabIndex = 64;
            this.lbl_collaborators.Text = "Collabortaors";
            // 
            // lbl_header
            // 
            this.lbl_header.AutoSize = true;
            this.lbl_header.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_header.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_header.Location = new System.Drawing.Point(199, 3);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(123, 29);
            this.lbl_header.TabIndex = 63;
            this.lbl_header.Text = "Add Event";
            this.lbl_header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.ForeColor = System.Drawing.Color.White;
            this.lbl_email.Location = new System.Drawing.Point(41, 129);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(35, 13);
            this.lbl_email.TabIndex = 62;
            this.lbl_email.Text = "Notes";
            // 
            // lbl_endtime
            // 
            this.lbl_endtime.AutoSize = true;
            this.lbl_endtime.ForeColor = System.Drawing.Color.White;
            this.lbl_endtime.Location = new System.Drawing.Point(41, 301);
            this.lbl_endtime.Name = "lbl_endtime";
            this.lbl_endtime.Size = new System.Drawing.Size(52, 13);
            this.lbl_endtime.TabIndex = 61;
            this.lbl_endtime.Text = "End Time";
            // 
            // lbl_startadate
            // 
            this.lbl_startadate.AutoSize = true;
            this.lbl_startadate.ForeColor = System.Drawing.Color.White;
            this.lbl_startadate.Location = new System.Drawing.Point(41, 245);
            this.lbl_startadate.Name = "lbl_startadate";
            this.lbl_startadate.Size = new System.Drawing.Size(55, 13);
            this.lbl_startadate.TabIndex = 60;
            this.lbl_startadate.Text = "Start Time";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.ForeColor = System.Drawing.Color.White;
            this.lbl_name.Location = new System.Drawing.Point(41, 75);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(27, 13);
            this.lbl_name.TabIndex = 59;
            this.lbl_name.Text = "Title";
            // 
            // txt_email
            // 
            this.txt_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_email.ForeColor = System.Drawing.Color.White;
            this.txt_email.Location = new System.Drawing.Point(44, 156);
            this.txt_email.MaxLength = 500;
            this.txt_email.Multiline = true;
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(250, 69);
            this.txt_email.TabIndex = 58;
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txt_name.ForeColor = System.Drawing.Color.White;
            this.txt_name.Location = new System.Drawing.Point(43, 92);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(250, 24);
            this.txt_name.TabIndex = 57;
            // 
            // pb_close
            // 
            this.pb_close.Image = ((System.Drawing.Image)(resources.GetObject("pb_close.Image")));
            this.pb_close.Location = new System.Drawing.Point(17, 12);
            this.pb_close.Name = "pb_close";
            this.pb_close.Size = new System.Drawing.Size(20, 20);
            this.pb_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_close.TabIndex = 66;
            this.pb_close.TabStop = false;
            this.pb_close.Click += new System.EventHandler(this.pb_close_Click);
            // 
            // lbl_addcollab
            // 
            this.lbl_addcollab.Location = new System.Drawing.Point(299, 411);
            this.lbl_addcollab.Name = "lbl_addcollab";
            this.lbl_addcollab.Size = new System.Drawing.Size(25, 23);
            this.lbl_addcollab.TabIndex = 76;
            this.lbl_addcollab.Text = "+";
            this.lbl_addcollab.UseVisualStyleBackColor = true;
            this.lbl_addcollab.Click += new System.EventHandler(this.lbl_addcollab_Click);
            // 
            // btn_removecollab
            // 
            this.btn_removecollab.Location = new System.Drawing.Point(589, 410);
            this.btn_removecollab.Name = "btn_removecollab";
            this.btn_removecollab.Size = new System.Drawing.Size(25, 23);
            this.btn_removecollab.TabIndex = 78;
            this.btn_removecollab.Text = "-";
            this.btn_removecollab.UseVisualStyleBackColor = true;
            this.btn_removecollab.Click += new System.EventHandler(this.btn_removecollab_Click);
            // 
            // cmb_evetncollab
            // 
            this.cmb_evetncollab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmb_evetncollab.ForeColor = System.Drawing.Color.White;
            this.cmb_evetncollab.FormattingEnabled = true;
            this.cmb_evetncollab.Location = new System.Drawing.Point(333, 412);
            this.cmb_evetncollab.Name = "cmb_evetncollab";
            this.cmb_evetncollab.Size = new System.Drawing.Size(250, 21);
            this.cmb_evetncollab.TabIndex = 77;
            // 
            // AddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(627, 583);
            this.Controls.Add(this.btn_removecollab);
            this.Controls.Add(this.cmb_evetncollab);
            this.Controls.Add(this.lbl_addcollab);
            this.Controls.Add(this.cmb_repeattype);
            this.Controls.Add(this.lbl_repeat);
            this.Controls.Add(this.rb_appointment);
            this.Controls.Add(this.rb_task);
            this.Controls.Add(this.dtp_endtime);
            this.Controls.Add(this.dtp_starttime);
            this.Controls.Add(this.cmb_contacts);
            this.Controls.Add(this.dtp_enddate);
            this.Controls.Add(this.dtp_startdate);
            this.Controls.Add(this.pb_close);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lbl_collaborators);
            this.Controls.Add(this.lbl_header);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.lbl_endtime);
            this.Controls.Add(this.lbl_startadate);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddEvent";
            this.Load += new System.EventHandler(this.AddEvent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_repeattype;
        private System.Windows.Forms.Label lbl_repeat;
        private System.Windows.Forms.RadioButton rb_appointment;
        private System.Windows.Forms.RadioButton rb_task;
        private System.Windows.Forms.DateTimePicker dtp_endtime;
        private System.Windows.Forms.DateTimePicker dtp_starttime;
        private System.Windows.Forms.ComboBox cmb_contacts;
        private System.Windows.Forms.DateTimePicker dtp_enddate;
        private System.Windows.Forms.DateTimePicker dtp_startdate;
        private System.Windows.Forms.PictureBox pb_close;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbl_collaborators;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_endtime;
        private System.Windows.Forms.Label lbl_startadate;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Button lbl_addcollab;
        private System.Windows.Forms.Button btn_removecollab;
        private System.Windows.Forms.ComboBox cmb_evetncollab;
    }
}
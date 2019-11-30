namespace EventManager.UIComponents
{
    partial class EventListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_enddate = new System.Windows.Forms.Label();
            this.lbl_startdate = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.pb_edit = new System.Windows.Forms.PictureBox();
            this.pb_delete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_delete)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_enddate
            // 
            this.lbl_enddate.AutoSize = true;
            this.lbl_enddate.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_enddate.ForeColor = System.Drawing.Color.White;
            this.lbl_enddate.Location = new System.Drawing.Point(111, 26);
            this.lbl_enddate.Name = "lbl_enddate";
            this.lbl_enddate.Size = new System.Drawing.Size(39, 16);
            this.lbl_enddate.TabIndex = 13;
            this.lbl_enddate.Text = "email";
            // 
            // lbl_startdate
            // 
            this.lbl_startdate.AutoSize = true;
            this.lbl_startdate.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_startdate.ForeColor = System.Drawing.Color.White;
            this.lbl_startdate.Location = new System.Drawing.Point(22, 26);
            this.lbl_startdate.Name = "lbl_startdate";
            this.lbl_startdate.Size = new System.Drawing.Size(39, 16);
            this.lbl_startdate.TabIndex = 12;
            this.lbl_startdate.Text = "email";
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(22, 6);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(36, 16);
            this.lbl_title.TabIndex = 11;
            this.lbl_title.Text = "Title";
            // 
            // pb_edit
            // 
            this.pb_edit.Image = global::EventManager.Properties.Resources.edit;
            this.pb_edit.Location = new System.Drawing.Point(557, 16);
            this.pb_edit.Name = "pb_edit";
            this.pb_edit.Size = new System.Drawing.Size(20, 20);
            this.pb_edit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_edit.TabIndex = 15;
            this.pb_edit.TabStop = false;
            this.pb_edit.Click += new System.EventHandler(this.pb_edit_Click);
            // 
            // pb_delete
            // 
            this.pb_delete.Image = global::EventManager.Properties.Resources.remove;
            this.pb_delete.Location = new System.Drawing.Point(583, 16);
            this.pb_delete.Name = "pb_delete";
            this.pb_delete.Size = new System.Drawing.Size(20, 20);
            this.pb_delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_delete.TabIndex = 14;
            this.pb_delete.TabStop = false;
            this.pb_delete.Click += new System.EventHandler(this.pb_delete_Click);
            // 
            // EventListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.pb_edit);
            this.Controls.Add(this.pb_delete);
            this.Controls.Add(this.lbl_enddate);
            this.Controls.Add(this.lbl_startdate);
            this.Controls.Add(this.lbl_title);
            this.Name = "EventListView";
            this.Size = new System.Drawing.Size(624, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pb_edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_delete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_edit;
        private System.Windows.Forms.PictureBox pb_delete;
        private System.Windows.Forms.Label lbl_enddate;
        private System.Windows.Forms.Label lbl_startdate;
        private System.Windows.Forms.Label lbl_title;
    }
}

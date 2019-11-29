namespace EventManager.UIComponents
{
    partial class ContactListView
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
            this.pb_edit = new System.Windows.Forms.PictureBox();
            this.pb_delete = new System.Windows.Forms.PictureBox();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.cpb_image = new EventManager.UIComponents.CircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpb_image)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_edit
            // 
            this.pb_edit.Image = global::EventManager.Properties.Resources.edit;
            this.pb_edit.Location = new System.Drawing.Point(547, 15);
            this.pb_edit.Name = "pb_edit";
            this.pb_edit.Size = new System.Drawing.Size(20, 20);
            this.pb_edit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_edit.TabIndex = 8;
            this.pb_edit.TabStop = false;
            this.pb_edit.Click += new System.EventHandler(this.pb_edit_Click);
            // 
            // pb_delete
            // 
            this.pb_delete.Image = global::EventManager.Properties.Resources.remove;
            this.pb_delete.Location = new System.Drawing.Point(573, 15);
            this.pb_delete.Name = "pb_delete";
            this.pb_delete.Size = new System.Drawing.Size(20, 20);
            this.pb_delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_delete.TabIndex = 7;
            this.pb_delete.TabStop = false;
            this.pb_delete.Click += new System.EventHandler(this.pb_delete_Click);
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email.ForeColor = System.Drawing.Color.White;
            this.lbl_email.Location = new System.Drawing.Point(58, 24);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(39, 16);
            this.lbl_email.TabIndex = 6;
            this.lbl_email.Text = "email";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.ForeColor = System.Drawing.Color.White;
            this.lbl_name.Location = new System.Drawing.Point(58, 4);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(74, 16);
            this.lbl_name.TabIndex = 5;
            this.lbl_name.Text = "Username";
            // 
            // cpb_image
            // 
            this.cpb_image.BackColor = System.Drawing.Color.Transparent;
            this.cpb_image.Image = global::EventManager.Properties.Resources.user;
            this.cpb_image.Location = new System.Drawing.Point(3, 4);
            this.cpb_image.Name = "cpb_image";
            this.cpb_image.Size = new System.Drawing.Size(40, 40);
            this.cpb_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cpb_image.TabIndex = 9;
            this.cpb_image.TabStop = false;
            // 
            // ContactListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.cpb_image);
            this.Controls.Add(this.pb_edit);
            this.Controls.Add(this.pb_delete);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.lbl_name);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ContactListView";
            this.Size = new System.Drawing.Size(624, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pb_edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpb_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_edit;
        private System.Windows.Forms.PictureBox pb_delete;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_name;
        private CircularPictureBox cpb_image;
    }
}

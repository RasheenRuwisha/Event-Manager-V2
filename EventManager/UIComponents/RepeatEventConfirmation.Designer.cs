namespace EventManager.UIComponents
{
    partial class RepeatEventConfirmation
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
            this.btn_alleventsinseries = new System.Windows.Forms.Button();
            this.btn_futureevents = new System.Windows.Forms.Button();
            this.btn_thisonly = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_alleventsinseries
            // 
            this.btn_alleventsinseries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_alleventsinseries.FlatAppearance.BorderSize = 0;
            this.btn_alleventsinseries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_alleventsinseries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_alleventsinseries.ForeColor = System.Drawing.Color.White;
            this.btn_alleventsinseries.Location = new System.Drawing.Point(0, 0);
            this.btn_alleventsinseries.Name = "btn_alleventsinseries";
            this.btn_alleventsinseries.Size = new System.Drawing.Size(368, 47);
            this.btn_alleventsinseries.TabIndex = 0;
            this.btn_alleventsinseries.Text = "All Events in the series";
            this.btn_alleventsinseries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_alleventsinseries.UseVisualStyleBackColor = false;
            this.btn_alleventsinseries.Click += new System.EventHandler(this.btn_alleventsinseries_Click);
            // 
            // btn_futureevents
            // 
            this.btn_futureevents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_futureevents.FlatAppearance.BorderSize = 0;
            this.btn_futureevents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_futureevents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_futureevents.ForeColor = System.Drawing.Color.White;
            this.btn_futureevents.Location = new System.Drawing.Point(0, 51);
            this.btn_futureevents.Name = "btn_futureevents";
            this.btn_futureevents.Size = new System.Drawing.Size(368, 47);
            this.btn_futureevents.TabIndex = 1;
            this.btn_futureevents.Text = "This and Future Events";
            this.btn_futureevents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_futureevents.UseVisualStyleBackColor = false;
            this.btn_futureevents.Click += new System.EventHandler(this.btn_futureevents_Click);
            // 
            // btn_thisonly
            // 
            this.btn_thisonly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_thisonly.FlatAppearance.BorderSize = 0;
            this.btn_thisonly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_thisonly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thisonly.ForeColor = System.Drawing.Color.White;
            this.btn_thisonly.Location = new System.Drawing.Point(0, 102);
            this.btn_thisonly.Name = "btn_thisonly";
            this.btn_thisonly.Size = new System.Drawing.Size(368, 47);
            this.btn_thisonly.TabIndex = 2;
            this.btn_thisonly.Text = "This Event Only";
            this.btn_thisonly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_thisonly.UseVisualStyleBackColor = false;
            this.btn_thisonly.Click += new System.EventHandler(this.btn_thisonly_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(174)))), ((int)(((byte)(191)))));
            this.btn_cancel.Location = new System.Drawing.Point(0, 159);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(368, 47);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // RepeatEventConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(368, 203);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_thisonly);
            this.Controls.Add(this.btn_futureevents);
            this.Controls.Add(this.btn_alleventsinseries);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RepeatEventConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RepeatEventConfirmation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_alleventsinseries;
        private System.Windows.Forms.Button btn_futureevents;
        private System.Windows.Forms.Button btn_thisonly;
        private System.Windows.Forms.Button btn_cancel;
    }
}
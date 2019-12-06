using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManager.DatabaseHelper;
using EventManager.View.Contacts;
using EventManager.Utility;

namespace EventManager.UIComponents
{
    public partial class ContactListView : UserControl
    {
        readonly CommonUtil commonUtil = new CommonUtil();
        public ContactListView()
        {
            InitializeComponent();
        }


        public String ContactName
        {
            set
            {
                this.lbl_name.Text = value;
            }
        }

        public String ContactEmail
        {
            set
            {
                this.lbl_email.Text = value;
            }
        }

        public Image ContactImage
        {
            set
            {
                this.cpb_image.Image = value;
            }
        }

        public String ContactId
        {
            set
            {
                this.Tag = value;
            }
        }


        private async void pb_delete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                    "Confirm Delete!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Controls.Clear();
                Label label = new Label
                {
                    Text = "Removing",
                    Font = new Font("Microsoft Sans Serif", 11.25F),
                    ForeColor = Color.White
                };
                label.Location = new Point(this.Width / 2 - label.Width / 2, this.Height / 2 - label.Height / 2);
                Controls.Add(label);
                Controls.Add(commonUtil.addLoaderImage(label.Location.X - 30, label.Location.Y - 3));

                bool removeContact = await Task.Run(() => ContactHelper.RemoveContact(this.Tag.ToString()));
                if (removeContact)
                {

                    Panel panel = Parent.Parent.Controls.Find("pnl_contactlist", true).FirstOrDefault() as Panel;
                    panel.Controls.Clear();
                    panel.Refresh();
                    panel.BringToFront();

                    Panel parentPanel = Parent as Panel;
                    if(parentPanel != null)
                    {
                        parentPanel.Controls.Clear();

                    }

                }
            }
            else
            {

            }
        }

        private void pb_edit_Click(object sender, EventArgs e)
        {
            EditContact editContact = new EditContact(this.Tag.ToString());
            editContact.FormClosing += new FormClosingEventHandler(this.EditContact_FormClosing);
            //editContact.FormClosing += new FormClosingEventHandler(this.form);
            editContact.ShowDialog();       
        }
        private void EditContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            Panel panel = this.Parent.Parent.Controls.Find("pnl_contactlist", true).FirstOrDefault() as Panel;
            panel.Controls.Clear();
            panel.Refresh();
            panel.BringToFront();

            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();

            }

        }
    }
}

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
using EventManager.Model;

namespace EventManager.UIComponents
{
    public partial class ContactListView : UserControl
    {
        readonly CommonUtil commonUtil = new CommonUtil();
        public ContactListView()
        {
            InitializeComponent();
        }

        public Contact ContactDetails
        {
            set {

                this.lbl_name.Text = value.Name;
                this.lbl_email.Text = value.Email;
                this.cpb_image.Image = commonUtil.Base64ToBitmap(value.Image);
                Tag = value;
            }
        }


        private async void pb_delete_Click(object sender, EventArgs e)
        {
            Contact contact = Tag as Contact;
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                    "Confirm Delete!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                Panel panel = Parent.Parent.Controls.Find("pnl_contactlist", true).FirstOrDefault() as Panel;
                Panel panelPreview = Parent.Parent.Controls.Find("pnl_contactpreview", true).FirstOrDefault() as Panel;


                DeleteEvent deleteEvent = new DeleteEvent("contact", contact.ContactId, panel, panelPreview);
                deleteEvent.ShowDialog();
            }
        }

        private void pb_edit_Click(object sender, EventArgs e)
        {
            Contact contact = Tag as Contact;
            EditContact editContact = new EditContact(contact);
            editContact.FormClosing += new FormClosingEventHandler(this.EditContact_FormClosing);
            //editContact.FormClosing += new FormClosingEventHandler(this.form);
            editContact.ShowDialog();       
        }
        private void EditContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            Panel panel = this.Parent.Parent.Controls.Find("pnl_contactlist", true).FirstOrDefault() as Panel;

            Panel panelPreview = Parent.Parent.Controls.Find("pnl_contactpreview", true).FirstOrDefault() as Panel;
            panelPreview.Controls.Clear();

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

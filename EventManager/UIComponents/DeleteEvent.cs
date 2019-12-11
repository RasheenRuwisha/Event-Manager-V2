using EventManager.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.UIComponents
{
    public partial class DeleteEvent : Form
    {
        string type;
        string id;
        Panel panel;
        Panel previewPanel;
        public DeleteEvent()
        {
            InitializeComponent();
        }

        public  DeleteEvent(string type, string id, Panel panel, Panel previewPanel)
        {
            InitializeComponent();
            this.type = type;
            this.id = id;
            this.panel = panel;
            this.previewPanel = previewPanel;

        }

        private async void DeleteEvent_Load(object sender, EventArgs e)
        {
            bool remove = false;
            if (type.Equals("event"))
            {
                remove = await Task.Run(() => EventHelper.RemoveEvent(id));
            }
            else
            {
                remove = await Task.Run(() => ContactHelper.RemoveContact(id));
            }

            if (remove)
            {
                previewPanel.Controls.Clear();
                panel.Controls.Clear();
                panel.Refresh();
                this.Close();
            }
        }
    }
}

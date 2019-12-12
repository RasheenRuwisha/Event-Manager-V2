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
        Panel eventPreviewPanel;
        Panel eventPanel;

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


        public DeleteEvent(string type, string id, Panel panel, Panel previewPanel, Panel eventPanel, Panel eventPreviewPanel)
        {
            InitializeComponent();
            this.type = type;
            this.id = id;
            this.panel = panel;
            this.previewPanel = previewPanel;
            this.eventPreviewPanel = eventPreviewPanel;
            this.eventPanel = eventPanel;

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
                if (type.Equals("contact"))
                {
                    eventPanel.Controls.Clear();
                    eventPreviewPanel.Controls.Clear();
                    eventPanel.Refresh();
                }
                previewPanel.Controls.Clear();
                panel.Controls.Clear();
                panel.Refresh();
                this.Close();
            }
         
        }
    }
}

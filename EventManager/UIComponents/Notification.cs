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
    public partial class Notification : Form
    {

        public Notification()
        {
            InitializeComponent();
        }
        public Notification( string text)
        {
            InitializeComponent();
            lbl_text.Text = text;
            lbl_text.Left = Width / 2 - lbl_text.Width / 2;
            lbl_text.Top = Height / 2 - lbl_text.Height / 2;
        }
    }
}

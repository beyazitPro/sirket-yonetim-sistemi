using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ticariotomasyon.forms
{
    public partial class frmnotdetay : Form
    {
        public frmnotdetay()
        {
            InitializeComponent();
        }

        public string not;

        private void frmnotdetay_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = not;
        }
    }
}

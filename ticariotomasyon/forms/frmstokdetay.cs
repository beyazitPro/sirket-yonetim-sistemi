using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ticariotomasyon.forms
{
    public partial class frmstokdetay : Form
    {
        public frmstokdetay()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();
        public string ad;

        private void frmstokdetay_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_URUN where URUNAD='"+ad+"'",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}

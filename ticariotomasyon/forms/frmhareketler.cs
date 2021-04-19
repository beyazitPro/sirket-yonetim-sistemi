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
    public partial class frmhareketler : Form
    {
        public frmhareketler()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void liste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec firmahareketleri",sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("exec musreıhareketlerı", sql.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void frmhareketler_Load(object sender, EventArgs e)
        {
            liste();
        }
    }
}

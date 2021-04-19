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
    public partial class frmstoklar : Form
    {
        public frmstoklar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD,sum(ADET) as 'miktar' from TBL_URUN group by URUNAD",sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void grafikliste()
        {
            //ürün - adet
            SqlCommand komut = new SqlCommand("select URUNAD,sum(ADET) as 'miktar' from TBL_URUN group by URUNAD", sql.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["ürünler"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));
            }
            sql.baglanti().Close();
            dr.Close();

            //firmalar - iller
            SqlCommand komut2 = new SqlCommand("select IL,COUNT(*) from TBL_FİRMALAR group by IL", sql.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["firmalar - iller"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            sql.baglanti().Close();
            dr2.Close();
        }

        private void frmstoklar_Load(object sender, EventArgs e)
        {
            listele();
            grafikliste();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            frmstokdetay fr = new frmstokdetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }
            fr.Show();
        }
    }
}

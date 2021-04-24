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
    public partial class frmrehber : Form
    {
        public frmrehber()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        private void frmrehber_Load(object sender, EventArgs e)
        {
            //müşteri bilgileri getirme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_MUSTERI", sql.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;

            //Firmaların listelenmesi
            DataTable dt2= new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select AD,YETKILIADSOYAD as 'YETKİLİ KİŞİ',TELEFON1,TELEFON2,TELEFON3,FAX,MAIL from TBL_FİRMALAR", sql.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt2;
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            frmmail fr = new frmmail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr!=null)
            {
                fr.mail = dr["MAIL"].ToString();
            }
            fr.Show();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            frmmail fr = new frmmail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.mail = dr["MAIL"].ToString();
            }
            fr.Show();
        }
    }
}

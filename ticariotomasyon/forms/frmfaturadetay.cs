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
    public partial class frmfaturadetay : Form
    {
        public frmfaturadetay()
        {
            InitializeComponent();
        }

        public int id;
        sqlbaglanti sql = new sqlbaglanti();

        public void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select FATURAURUNID as 'ID',URUNAD as 'ÜRÜN AD',MİKTAR,FİYAT,TUTAR,TBL_FATURABILGI.SIRANO as 'FATURA' from TBL_FATURADETAY inner join TBL_FATURABILGI on TBL_FATURADETAY.FATURAID = TBL_FATURABILGI.FATURABILGIID where FATURAID = '" + id+"'", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void frmfaturadetay_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            frmfaturadetayduzeltme fr = new frmfaturadetayduzeltme();
            DataRow dt = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dt!=null)
            {
                fr.Id = int.Parse(dt["ID"].ToString());
            }
            fr.Show();
        }
    }
}

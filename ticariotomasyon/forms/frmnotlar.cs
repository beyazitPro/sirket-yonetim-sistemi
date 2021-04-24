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
using DevExpress.XtraEditors;

namespace ticariotomasyon.forms
{
    public partial class frmnotlar : Form
    {
        public frmnotlar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_NOTLAR", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtbaslik.Text = "";
            txthitap.Text = "";
            txtolusturan.Text = "";
            tarihsaat.Text = "";
            rchdetay.Text = "";
        }

        private void frmnotlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand ekleme = new SqlCommand("insert into TBL_NOTLAR (TARİHVESAAT,OLUSTURAN,HİTAP,BASLIK,DETAY) values (@p1,@p2,@p3,@p4,@p5)", sql.baglanti());
                ekleme.Parameters.AddWithValue("@p1",tarihsaat.Text);
                ekleme.Parameters.AddWithValue("@p2", txtolusturan.Text);
                ekleme.Parameters.AddWithValue("@p3", txthitap.Text);
                ekleme.Parameters.AddWithValue("@p4", txtbaslik.Text);
                ekleme.Parameters.AddWithValue("@p5", rchdetay.Text);
                ekleme.ExecuteNonQuery();
                sql.baglanti().Close();
                listele();
                temizle();
                XtraMessageBox.Show("Not gönderildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow dt = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dt != null)
            {
                txtid.Text = dt["ID"].ToString();
                txtbaslik.Text = dt["BASLIK"].ToString();
                txthitap.Text = dt["HİTAP"].ToString();
                txtolusturan.Text = dt["OLUSTURAN"].ToString();
                tarihsaat.Text = dt["TARİHVESAAT"].ToString();
                rchdetay.Text = dt["DETAY"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = XtraMessageBox.Show("notu silek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog==DialogResult.Yes)
                {
                    SqlCommand silme = new SqlCommand("Delete from TBL_NOTLAR where ID=@p1", sql.baglanti());
                    silme.Parameters.AddWithValue("@p1", txtid.Text);
                    silme.ExecuteNonQuery();
                    listele();
                    temizle();
                    XtraMessageBox.Show("Not silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guncelle = new SqlCommand("Update TBL_NOTLAR set TARİHVESAAT=@p1,OLUSTURAN=@p2,HİTAP=@p3,BASLIK=@p4,DETAY=@p5 where ID=@p6", sql.baglanti());
                guncelle.Parameters.AddWithValue("@p1", tarihsaat.Text);
                guncelle.Parameters.AddWithValue("@p2", txtolusturan.Text);
                guncelle.Parameters.AddWithValue("@p3", txthitap.Text);
                guncelle.Parameters.AddWithValue("@p4", txtbaslik.Text);
                guncelle.Parameters.AddWithValue("@p5", rchdetay.Text);
                guncelle.Parameters.AddWithValue("@p6", txtid.Text);
                guncelle.ExecuteNonQuery();
                sql.baglanti().Close();
                listele();
                temizle();
                XtraMessageBox.Show("Not güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dt = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frmnotdetay fr =new frmnotdetay();
            if (dt!=null)
            {
                fr.not = dt["DETAY"].ToString();
            }
            fr.Show();
        }
    }
}

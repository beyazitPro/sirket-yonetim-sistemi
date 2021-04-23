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
    public partial class frmfaturadetayduzeltme : Form
    {
        public frmfaturadetayduzeltme()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();
        int miktar;
        double fiyat, tutar;
        public int Id;

        void faturaliste()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select FATURABILGIID as 'ID' ,SIRANO as 'Sıra no' from TBL_FATURABILGI", sql.baglanti());
            da2.Fill(dt2);
            cmbfatura.Properties.ValueMember = "ID";
            cmbfatura.Properties.DisplayMember = "Sıra no";
            cmbfatura.Properties.DataSource = dt2;
        }

        private void txtmiktar_TextChanged(object sender, EventArgs e)
        {

            if (txtmiktar.Text != "")
            {
                miktar = int.Parse(txtmiktar.Text);
                if (txtfiyat.Text != "")
                {
                    fiyat = double.Parse(txtfiyat.Text);
                    tutar = miktar * fiyat;
                    txttutar.Text = tutar.ToString();
                }
            }
        }

        private void txtfiyat_TextChanged(object sender, EventArgs e)
        {


            if (txtfiyat.Text != "")
            {
                fiyat = double.Parse(txtfiyat.Text);
                if (txtmiktar.Text != "")
                {
                    miktar = int.Parse(txtmiktar.Text);
                    tutar = miktar * fiyat;
                    txttutar.Text = tutar.ToString();
                }
            }

        }

        private void txtfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtmiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand gunceleme = new SqlCommand("Update TBL_FATURADETAY  set URUNAD=@p1,MİKTAR=@p2,FİYAT=@p3,TUTAR=@p4,FATURAID=@p5 where FATURAURUNID=@p6", sql.baglanti());
                gunceleme.Parameters.AddWithValue("@p1", txturunad.Text);
                gunceleme.Parameters.AddWithValue("@p2", txtmiktar.Text);
                gunceleme.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
                gunceleme.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                gunceleme.Parameters.AddWithValue("@p5", cmbfatura.EditValue);
                gunceleme.Parameters.AddWithValue("@p6", Id);
                gunceleme.ExecuteNonQuery();
                sql.baglanti().Close();
                XtraMessageBox.Show("Fatura Detayları güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = XtraMessageBox.Show("ürünün faturasını silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog==DialogResult.Yes)
                {
                    SqlCommand silme = new SqlCommand("delete from TBL_FATURADETAY where FATURAURUNID=@p1", sql.baglanti());
                    silme.Parameters.AddWithValue("@p1", Id);
                    silme.ExecuteNonQuery();
                    sql.baglanti().Close();
                    XtraMessageBox.Show("Fatura silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmfaturadetayduzeltme_Load(object sender, EventArgs e)
        {
            txtid.Text = Id.ToString();
            SqlCommand komut = new SqlCommand("Select * from TBL_FATURADETAY where FATURAURUNID=@p1", sql.baglanti());
            komut.Parameters.AddWithValue("@p1", Id);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txturunad.Text = dr[1].ToString();
                txtmiktar.Text = dr[2].ToString();
                txtfiyat.Text = dr[3].ToString();
                txttutar.Text = dr[4].ToString();
                cmbfatura.EditValue = dr[5].ToString();
            }
            dr.Close();
            sql.baglanti().Close();
            faturaliste();
        }
    }
}

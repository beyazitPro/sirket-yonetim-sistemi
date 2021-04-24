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
    public partial class faturalar : Form
    {
        public faturalar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();
        int miktar;
        double fiyat, tutar;

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select FATURABILGIID as 'ID',SERI,SIRANO as 'Sıra no',TARIHSAAT as 'Tarih ve Saat',VERGIDAIRE as 'Vergi Dairesi',ALICI,TESLIMEDEN as 'TESLIM EDEN',TESLIMALAN as 'TESLIM ALAN' from TBL_FATURABILGI", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select FATURABILGIID as 'ID' ,SIRANO as 'Sıra no' from TBL_FATURABILGI", sql.baglanti());
            da2.Fill(dt2);
            cmbfatura.Properties.ValueMember = "ID";
            cmbfatura.Properties.DisplayMember = "Sıra no";
            cmbfatura.Properties.DataSource = dt2;

            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Select ID,(AD+' '+SOYAD) as 'PERSONEL' from TBL_PERSONEL", sql.baglanti());
            da3.Fill(dt3);
            cmbpersonel.Properties.ValueMember = "ID";
            cmbpersonel.Properties.DisplayMember = "PERSONEL";
            cmbpersonel.Properties.DataSource = dt3;

            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("Select ID,AD from TBL_FİRMALAR", sql.baglanti());
            da4.Fill(dt4);
            cmbfirma.Properties.ValueMember = "ID";
            cmbfirma.Properties.DisplayMember = "AD";
            cmbfirma.Properties.DataSource = dt4;
        }

        void temizle()
        {
            txtid.Text = "";
            txtseri.Text = "";
            txtsırano.Text = "";
            tarihvesaat.EditValue = "";
            txtvergidaire.Text = "";
            txtteslimalan.Text = "";
            txtteslimeden.Text = "";
            txtalıcı.Text = "";
            txtmiktar.Text = "";
            txtfiyat.Text = "";
            txttutar.Text = "";
            txturunad.Text = "";
            txturunıd.Text = "";
            cmbfatura.Text = "";
        }

        private void frmfaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }


        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand eklemekoutu = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MİKTAR,FİYAT,TUTAR,FATURAID) VALUES (@p1,@p2,@p3,@p4,@p5)", sql.baglanti());
                eklemekoutu.Parameters.AddWithValue("@P1", txturunad.Text);
                eklemekoutu.Parameters.AddWithValue("@P2", txtmiktar.Text);
                eklemekoutu.Parameters.AddWithValue("@P3", decimal.Parse(txtfiyat.Text));
                eklemekoutu.Parameters.AddWithValue("@P4", decimal.Parse(txttutar.Text));
                eklemekoutu.Parameters.AddWithValue("@P5", cmbfatura.EditValue);
                eklemekoutu.ExecuteNonQuery();
                sql.baglanti().Close();
                if (comboBoxEdit1.Text == "Firma")
                {
                    SqlCommand hareketkomutu = new SqlCommand("insert into TBL_FİRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", sql.baglanti());
                    hareketkomutu.Parameters.AddWithValue("@h1", txturunıd.Text);
                    hareketkomutu.Parameters.AddWithValue("@h2", txtmiktar.Text);
                    hareketkomutu.Parameters.AddWithValue("@h3", cmbpersonel.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h4", cmbfirma.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                    hareketkomutu.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                    hareketkomutu.Parameters.AddWithValue("@h7", cmbfatura.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h8", tarihvesaat.Text);
                    hareketkomutu.ExecuteNonQuery();
                    sql.baglanti().Close();
                }
                else if (comboBoxEdit1.Text == "Müşteri")
                {
                    SqlCommand hareketkomutu = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", sql.baglanti());
                    hareketkomutu.Parameters.AddWithValue("@h1", txturunıd.Text);
                    hareketkomutu.Parameters.AddWithValue("@h2", txtmiktar.Text);
                    hareketkomutu.Parameters.AddWithValue("@h3", cmbpersonel.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h4", cmbfirma.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                    hareketkomutu.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                    hareketkomutu.Parameters.AddWithValue("@h7", cmbfatura.EditValue);
                    hareketkomutu.Parameters.AddWithValue("@h8", tarihvesaat.Text);
                    hareketkomutu.ExecuteNonQuery();
                    sql.baglanti().Close();
                }

                SqlCommand stokdusme = new SqlCommand("Update TBL_URUN set ADET=ADET-@s1 where ID=@s2", sql.baglanti());
                stokdusme.Parameters.AddWithValue("@s1", txtmiktar.Text);
                stokdusme.Parameters.AddWithValue("@s2", txturunıd.Text);
                stokdusme.ExecuteNonQuery();
                sql.baglanti().Close();

                XtraMessageBox.Show("Fatura sisteme kaydedilmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz." + ex, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                //seçilen verileri textboxlara yazdırma
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txtseri.Text = dr["SERI"].ToString();
                    txtsırano.Text = dr["Sıra no"].ToString();
                    tarihvesaat.Text = dr["Tarih ve Saat"].ToString();
                    txtvergidaire.Text = dr["Vergi Dairesi"].ToString();
                    txtalıcı.Text = dr["ALICI"].ToString();
                    txtteslimeden.Text = dr["TESLIM EDEN"].ToString();
                    txtteslimalan.Text = dr["TESLIM ALAN"].ToString();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btntemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = XtraMessageBox.Show("Fatura silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand silmekomutu = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", sql.baglanti());
                    silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                    silmekomutu.ExecuteNonQuery();
                    sql.baglanti().Close();
                    XtraMessageBox.Show("Fatura Silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guncelemekomutu = new SqlCommand("Update TBL_FATURABILGI set SERI=@p1,SIRANO=@p2,TARIHSAAT=@p3,VERGIDAIRE=@p4,ALICI=@p5,TESLIMEDEN=@p6,TESLIMALAN=@p7 where FATURABILGIID=@p8", sql.baglanti());
                guncelemekomutu.Parameters.AddWithValue("@p1", txtseri.Text);
                guncelemekomutu.Parameters.AddWithValue("@p2", txtsırano.Text);
                guncelemekomutu.Parameters.AddWithValue("@p3", tarihvesaat.Text);
                guncelemekomutu.Parameters.AddWithValue("@p4", txtvergidaire.Text);
                guncelemekomutu.Parameters.AddWithValue("@p5", txtalıcı.Text);
                guncelemekomutu.Parameters.AddWithValue("@p6", txtteslimeden.Text);
                guncelemekomutu.Parameters.AddWithValue("@p7", txtteslimalan.Text);
                guncelemekomutu.Parameters.AddWithValue("@p8", txtid.Text);
                guncelemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                XtraMessageBox.Show("Fatura sisteme kaydedilmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz." + ex, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnkaydetfaturabilgi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand eklemekoutu = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIHSAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7)", sql.baglanti());
                eklemekoutu.Parameters.AddWithValue("@P1", txtseri.Text);
                eklemekoutu.Parameters.AddWithValue("@P2", txtsırano.Text);
                eklemekoutu.Parameters.AddWithValue("@P3", tarihvesaat.Text);
                eklemekoutu.Parameters.AddWithValue("@P4", txtvergidaire.Text);
                eklemekoutu.Parameters.AddWithValue("@P5", txtalıcı.Text);
                eklemekoutu.Parameters.AddWithValue("@P6", txtteslimeden.Text);
                eklemekoutu.Parameters.AddWithValue("@P7", txtteslimalan.Text);
                eklemekoutu.ExecuteNonQuery();
                sql.baglanti().Close();
                XtraMessageBox.Show("Fatura sisteme kaydedilmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            frmfaturadetay fr = new frmfaturadetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.id = int.Parse(dr["ID"].ToString());
            }
            fr.Show();
        }

        private void txturunıd_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT from TBL_URUN where ID=@p1", sql.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunıd.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txturunad.Text = dr[0].ToString();
                txtfiyat.Text = dr[1].ToString();
            }
            dr.Close();
            sql.baglanti().Close();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "Firma")
            {
                layoutControlItem29.Text = "Firma :";
                DataTable dt4 = new DataTable();
                SqlDataAdapter da4 = new SqlDataAdapter("Select ID,AD from TBL_FİRMALAR", sql.baglanti());
                da4.Fill(dt4);
                cmbfirma.Properties.ValueMember = "ID";
                cmbfirma.Properties.DisplayMember = "AD";
                cmbfirma.Properties.DataSource = dt4;
            }
            else if (comboBoxEdit1.Text == "Müşteri")
            {
                layoutControlItem29.Text = "Müşteri :";
                DataTable dt5 = new DataTable();
                SqlDataAdapter da5 = new SqlDataAdapter("Select ID,AD from TBL_MUSTERI", sql.baglanti());
                da5.Fill(dt5);
                cmbfirma.Properties.ValueMember = "ID";
                cmbfirma.Properties.DisplayMember = "AD";
                cmbfirma.Properties.DataSource = dt5;
            }
        }

        private void btnlistele_Click_1(object sender, EventArgs e)
        {
            listele();
        }
    }
}

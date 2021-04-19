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

namespace ticariotomasyon
{
    public partial class frmmusteriler : Form
    {
        public frmmusteriler()
        {
            InitializeComponent();
        }

        //değişkenler,sınıflar,methodlar,listler
        sqlbaglanti sql = new sqlbaglanti();

        void temizle()
        {
            //temizleme methodu
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            txttelno.Text = "";
            txttelno2.Text = "";
            txttc.Text = "";
            txtmail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtadres.Text = "";
            txtvergidare.Text = "";
        }

        void listele()
        {
            //listeleme methodu
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE as 'Vergi Dairesi' from TBL_MUSTERI", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void frmmusteriler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            cmbil.Properties.DataSource = sql.sehirlistesi();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // veri kaydetme
                SqlCommand eklemekomutu = new SqlCommand("insert into TBL_MUSTERI (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", sql.baglanti());
                eklemekomutu.Parameters.AddWithValue("@p1", txtad.Text);
                eklemekomutu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                eklemekomutu.Parameters.AddWithValue("@p3", txttelno.Text);
                eklemekomutu.Parameters.AddWithValue("@p4", txttelno2.Text);
                eklemekomutu.Parameters.AddWithValue("@p5", txttc.Text);
                eklemekomutu.Parameters.AddWithValue("@p6", txtmail.Text);
                eklemekomutu.Parameters.AddWithValue("@p7", cmbil.Text);
                eklemekomutu.Parameters.AddWithValue("@p8", cmbilce.Text);
                eklemekomutu.Parameters.AddWithValue("@p9", txtadres.Text);
                eklemekomutu.Parameters.AddWithValue("@p10", txtvergidare.Text);
                eklemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Müşteri sisteme eklenmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //silme komutları
            try
            {
                if (txtid != null)
                {
                    DialogResult dialog = DevExpress.XtraEditors.XtraMessageBox.Show("Veriyi silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        SqlCommand silmekomutu = new SqlCommand("Delete from TBL_MUSTERI where ID=@p1", sql.baglanti());
                        silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                        silmekomutu.ExecuteNonQuery();
                        sql.baglanti().Close();
                        DevExpress.XtraEditors.XtraMessageBox.Show("Veri silinmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Lütfen silmeden önce bir satır seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //güncelleme komutları
            try
            {
                SqlCommand guncellemekomutu = new SqlCommand("update  TBL_MUSTERI set AD=@p1,SOYAD=@p2,TELEFON=@p3,TELEFON2=@p4,TC=@p5,MAIL=@p6,IL=@p7,ILCE=@p8,ADRES=@p9,VERGIDAIRE=@p10 where ID=@p11", sql.baglanti());
                guncellemekomutu.Parameters.AddWithValue("@p1", txtad.Text);
                guncellemekomutu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                guncellemekomutu.Parameters.AddWithValue("@p3", txttelno.Text);
                guncellemekomutu.Parameters.AddWithValue("@p4", txttelno2.Text);
                guncellemekomutu.Parameters.AddWithValue("@p5", txttc.Text);
                guncellemekomutu.Parameters.AddWithValue("@p6", txtmail.Text);
                guncellemekomutu.Parameters.AddWithValue("@p7", cmbil.Text);
                guncellemekomutu.Parameters.AddWithValue("@p8", cmbilce.Text);
                guncellemekomutu.Parameters.AddWithValue("@p9", txtadres.Text);
                guncellemekomutu.Parameters.AddWithValue("@p10", txtvergidare.Text);
                guncellemekomutu.Parameters.AddWithValue("@p11", txtid.Text);
                guncellemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Müşteri bilgileri güncelenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //veri seçtiğinde textboxlara yansıması
            try
            {
                temizle();
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr!=null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txtad.Text = dr["AD"].ToString();
                    txtsoyad.Text = dr["SOYAD"].ToString();
                    txttelno.Text = dr["TELEFON"].ToString();
                    txttelno2.Text = dr["TELEFON2"].ToString();
                    txttc.Text = dr["TC"].ToString();
                    txtmail.Text = dr["MAIL"].ToString();
                    cmbil.Text = dr["IL"].ToString();
                    cmbilce.Text = dr["ILCE"].ToString();
                    txtadres.Text = dr["ADRES"].ToString();
                    txtvergidare.Text = dr["Vergi Dairesi"].ToString();
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbil_EditValueChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.DataSource = sql.ilcelistesi(cmbil.ItemIndex+1);
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

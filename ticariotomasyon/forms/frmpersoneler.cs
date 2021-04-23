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
    public partial class frmpersoneler : Form
    {
        public frmpersoneler()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_PERSONEL",sql.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void temizle()
        {
            //temizleme methodu
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            txttelno.Text = "";
            txttc.Text = "";
            txtmail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtadres.Text = "";
            txtgorev.Text = "";
        }

        private void frmpersoneler_Load(object sender, EventArgs e)
        {
            listele();
            cmbil.Properties.DataSource = sql.sehirlistesi();
            temizle();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void cmbil_EditValueChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.DataSource = sql.ilcelistesi(cmbil.ItemIndex + 1);
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // veri kaydetme
                SqlCommand eklemekomutu = new SqlCommand("insert into TBL_PERSONEL (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sql.baglanti());
                eklemekomutu.Parameters.AddWithValue("@p1", txtad.Text);
                eklemekomutu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                eklemekomutu.Parameters.AddWithValue("@p3", txttelno.Text);
                eklemekomutu.Parameters.AddWithValue("@p4", txttc.Text);
                eklemekomutu.Parameters.AddWithValue("@p5", txtmail.Text);
                eklemekomutu.Parameters.AddWithValue("@p6", cmbil.Text);
                eklemekomutu.Parameters.AddWithValue("@p7", cmbilce.Text);
                eklemekomutu.Parameters.AddWithValue("@p8", txtadres.Text);
                eklemekomutu.Parameters.AddWithValue("@p9", txtgorev.Text);
                eklemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Personel sisteme eklenmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            {
                temizle();
                DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (dr != null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txtad.Text = dr["AD"].ToString();
                    txtsoyad.Text = dr["SOYAD"].ToString();
                    txttelno.Text = dr["TELEFON"].ToString();
                    txttc.Text = dr["TC"].ToString();
                    txtmail.Text = dr["MAIL"].ToString();
                    cmbil.Text = dr["IL"].ToString();
                    cmbilce.Text = dr["ILCE"].ToString();
                    txtadres.Text = dr["ADRES"].ToString();
                    txtgorev.Text = dr["GOREV"].ToString();
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //silme komutları
            try
            {
                if (txtid != null)
                {
                    DialogResult dialog = DevExpress.XtraEditors.XtraMessageBox.Show("Prsoneli silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        SqlCommand silmekomutu = new SqlCommand("Delete from TBL_PERSONEL where ID=@p1", sql.baglanti());
                        silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                        silmekomutu.ExecuteNonQuery();
                        sql.baglanti().Close();
                        DevExpress.XtraEditors.XtraMessageBox.Show("Personel sistemden kaldırılmıştır.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                SqlCommand guncellemekomutu = new SqlCommand("update TBL_PERSONEL set AD=@p1,SOYAD=@p2,TELEFON=@p3,TC=@p4,MAIL=@p5,IL=@p6,ILCE=@p7,ADRES=@p8,GOREV=@p9 where ID=@p10", sql.baglanti());
                guncellemekomutu.Parameters.AddWithValue("@p1", txtad.Text);
                guncellemekomutu.Parameters.AddWithValue("@p2", txtsoyad.Text);
                guncellemekomutu.Parameters.AddWithValue("@p3", txttelno.Text);
                guncellemekomutu.Parameters.AddWithValue("@p4", txttc.Text);
                guncellemekomutu.Parameters.AddWithValue("@p5", txtmail.Text);
                guncellemekomutu.Parameters.AddWithValue("@p6", cmbil.Text);
                guncellemekomutu.Parameters.AddWithValue("@p7", cmbilce.Text);
                guncellemekomutu.Parameters.AddWithValue("@p8", txtadres.Text);
                guncellemekomutu.Parameters.AddWithValue("@p9", txtgorev.Text);
                guncellemekomutu.Parameters.AddWithValue("@p10", txtid.Text);
                guncellemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Veri güncelenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception EX)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz."+EX, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

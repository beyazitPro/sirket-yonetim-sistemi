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
    public partial class frmurunler : Form
    {
        public frmurunler()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            //ürünleri listeleme kodu
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,URUNAD as 'Ürün adı',MARKA,MODEL,YIL,ADET,ALISFIYAT as 'Alış Fiyat',SATISFIYAT as 'Satış Fiyat',DETAY from TBL_URUN ", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txturunad.Text = "";
            txtmarka.Text = "";
            txtmodel.Text = "";
            txtyıl.Text = "";
            txtadet.Text = "";
            txtalisfiyat.Text = "";
            txtsatısfiyat.Text = "";
            txtalisfiyat.Text = "";
            txtdetay.Text = "";
        }

        private void frmurunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            //ekleme işlemi
            try
            {
                SqlCommand eklemekomutu = new SqlCommand("insert into TBL_URUN (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", sql.baglanti());
                eklemekomutu.Parameters.AddWithValue("@p1", txturunad.Text);
                eklemekomutu.Parameters.AddWithValue("@p2", txtmarka.Text);
                eklemekomutu.Parameters.AddWithValue("@p3", txtmodel.Text);
                eklemekomutu.Parameters.AddWithValue("@p4", txtyıl.Text);
                eklemekomutu.Parameters.AddWithValue("@p5", int.Parse(txtadet.Text));
                eklemekomutu.Parameters.AddWithValue("@p6", decimal.Parse(txtalisfiyat.Text));
                eklemekomutu.Parameters.AddWithValue("@p7", decimal.Parse(txtsatısfiyat.Text));
                eklemekomutu.Parameters.AddWithValue("@p8", txtdetay.Text);
                eklemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Ürün sisteme eklenmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //Veri tabanından seçilen veriyi textboxlara aktarma işlemi.
            try
            {
                temizle();
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txturunad.Text = dr["Ürün adı"].ToString();
                    txtmarka.Text = dr["MARKA"].ToString();
                    txtmodel.Text = dr["MODEL"].ToString();
                    txtyıl.Text = dr["YIL"].ToString();
                    txtadet.Text = dr["ADET"].ToString();
                    txtsatısfiyat.Text = dr["Satış Fiyat"].ToString();
                    txtalisfiyat.Text = dr["Alış Fiyat"].ToString();
                    txtdetay.Text = dr["DETAY"].ToString();
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //silme işlemi
            if (txtid.Text != "")
            {
                DialogResult dialog = new DialogResult();
                dialog = DevExpress.XtraEditors.XtraMessageBox.Show("Veriyi silmek istediğinden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {

                        SqlCommand silmekomutu = new SqlCommand("delete from TBL_URUN where ID=@p1", sql.baglanti());
                        silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                        silmekomutu.ExecuteNonQuery();
                        DevExpress.XtraEditors.XtraMessageBox.Show("Ürün silinmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }
                    catch (Exception)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Lütfen önce bir satır seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guncellemekomutu = new SqlCommand("update TBL_URUN set URUNAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFıYAT=@P7,DETAY = @P8 where ID=@P9", sql.baglanti());
                guncellemekomutu.Parameters.AddWithValue("@P1", txturunad.Text);
                guncellemekomutu.Parameters.AddWithValue("@P2", txtmarka.Text);
                guncellemekomutu.Parameters.AddWithValue("@P3", txtmodel.Text);
                guncellemekomutu.Parameters.AddWithValue("@P4", txtyıl.Text);
                guncellemekomutu.Parameters.AddWithValue("@P5", int.Parse(txtadet.Text));
                guncellemekomutu.Parameters.AddWithValue("@P6", decimal.Parse(txtalisfiyat.Text));
                guncellemekomutu.Parameters.AddWithValue("@P7", decimal.Parse(txtsatısfiyat.Text));
                guncellemekomutu.Parameters.AddWithValue("@P8", txtdetay.Text);
                guncellemekomutu.Parameters.AddWithValue("@P9", txtid.Text);
                guncellemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("bilgi güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtadet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtalisfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtsatısfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }
    }
}

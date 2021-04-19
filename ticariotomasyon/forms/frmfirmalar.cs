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
    public partial class frmfirmalar : Form
    {
        public frmfirmalar()
        {
            InitializeComponent();
        }
        //değişkenler methodlar ve sınıflar
        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            //listeleme komutu
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD,YETKILISTATU as 'Yetkili Statü',YETKILIADSOYAD as 'Yetkili Ad Soyad',YETKILITC as 'Yetkili TC',SEKTOR as 'Sektör',TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE as 'Vergi Dairesi',ADRES,OZELKOD1 as'Özel kod 1',OZELKOD2 as'Özel kod 2',OZELKOD3 as'Özel kod 3' from TBL_FİRMALAR", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            //temizleme komutu
            txtid.Text = "";
            txtfirmaad.Text = "";
            txtyetkili.Text = "";
            txtytc.Text = "";
            txtygorev.Text = "";
            txtvergidairesi.Text = "";
            txtfirmasektör.Text = "";
            txtfax.Text = "";
            txtmail.Text = "";
            txttelefon.Text = "";
            txttelefon2.Text = "";
            txttelefon3.Text = "";
            txtyetkili.Text = "";
            txtfax.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            rchadres.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void frmfirmalar_Load(object sender, EventArgs e)
        {
            temizle();
            cmbil.Properties.DataSource = sql.sehirlistesi();
            listele();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                //seçilen verileri textboxlara yazdırma
                temizle();
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txtfirmaad.Text = dr["AD"].ToString();
                    txtygorev.Text = dr["Yetkili Statü"].ToString();
                    txtyetkili.Text = dr["Yetkili Ad Soyad"].ToString();
                    txtytc.Text = dr["Yetkili TC"].ToString();
                    txtfirmasektör.Text = dr["Sektör"].ToString();
                    txttelefon.Text = dr["TELEFON1"].ToString();
                    txttelefon2.Text = dr["TELEFON2"].ToString();
                    txttelefon3.Text = dr["TELEFON3"].ToString();
                    txtfax.Text = dr["FAX"].ToString();
                    txtmail.Text = dr["MAIL"].ToString();
                    cmbil.Text = dr["IL"].ToString();
                    cmbilce.Text = dr["ILCE"].ToString();
                    txtvergidairesi.Text = dr["Vergi Dairesi"].ToString();
                    rchadres.Text = dr["ADRES"].ToString();
                    txtkod1.Text = dr["Özel kod 1"].ToString();
                    txtkod2.Text = dr["Özel kod 2"].ToString();
                    txtkod3.Text = dr["Özel kod 3"].ToString();
                }
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // veri ekleme
                SqlCommand eklemekomutu = new SqlCommand("insert into TBL_FİRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", sql.baglanti());
                eklemekomutu.Parameters.AddWithValue("@P1", txtfirmaad.Text);
                eklemekomutu.Parameters.AddWithValue("@P2", txtygorev.Text);
                eklemekomutu.Parameters.AddWithValue("@P3", txtyetkili.Text);
                eklemekomutu.Parameters.AddWithValue("@P4", txtytc.Text);
                eklemekomutu.Parameters.AddWithValue("@P5", txtfirmasektör.Text);
                eklemekomutu.Parameters.AddWithValue("@P6", txttelefon.Text);
                eklemekomutu.Parameters.AddWithValue("@P7", txttelefon2.Text);
                eklemekomutu.Parameters.AddWithValue("@P8", txttelefon3.Text);
                eklemekomutu.Parameters.AddWithValue("@P9", txtmail.Text);
                eklemekomutu.Parameters.AddWithValue("@P10", txtfax.Text);
                eklemekomutu.Parameters.AddWithValue("@P11", cmbil.Text);
                eklemekomutu.Parameters.AddWithValue("@P12", cmbilce.Text);
                eklemekomutu.Parameters.AddWithValue("@P13", txtvergidairesi.Text);
                eklemekomutu.Parameters.AddWithValue("@P14", rchadres.Text);
                eklemekomutu.Parameters.AddWithValue("@P15", txtkod1.Text);
                eklemekomutu.Parameters.AddWithValue("@P16", txtkod2.Text);
                eklemekomutu.Parameters.AddWithValue("@P17", txtkod3.Text);
                eklemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                DevExpress.XtraEditors.XtraMessageBox.Show("Firma sisteme eklenmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanların dolu olup olmadığını kontrol edip tekrar deneyiniz."+ex, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void cmbil_EditValueChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.DataSource = sql.ilcelistesi(cmbil.ItemIndex + 1);
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtid != null)
            {
                DialogResult dialog = DevExpress.XtraEditors.XtraMessageBox.Show("Veriyi silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SqlCommand silmekomutu = new SqlCommand("delete from TBL_FİRMALAR where ID=@p1", sql.baglanti());
                    silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                    silmekomutu.ExecuteNonQuery();
                    sql.baglanti().Close();
                    DevExpress.XtraEditors.XtraMessageBox.Show("kayıt silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Lütfen silmeden önce bir satır seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //Güncelleme komutu
            SqlCommand guncellemekomutu = new SqlCommand("update TBL_FİRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 where ID=@p18",sql.baglanti());
            guncellemekomutu.Parameters.AddWithValue("@P1", txtfirmaad.Text);
            guncellemekomutu.Parameters.AddWithValue("@P2", txtygorev.Text);
            guncellemekomutu.Parameters.AddWithValue("@P3", txtyetkili.Text);
            guncellemekomutu.Parameters.AddWithValue("@P4", txtytc.Text);
            guncellemekomutu.Parameters.AddWithValue("@P5", txtfirmasektör.Text);
            guncellemekomutu.Parameters.AddWithValue("@P6", txttelefon.Text);
            guncellemekomutu.Parameters.AddWithValue("@P7", txttelefon2.Text);
            guncellemekomutu.Parameters.AddWithValue("@P8", txttelefon3.Text);
            guncellemekomutu.Parameters.AddWithValue("@P9", txtmail.Text);
            guncellemekomutu.Parameters.AddWithValue("@P10", txtfax.Text);
            guncellemekomutu.Parameters.AddWithValue("@P11", cmbil.Text);
            guncellemekomutu.Parameters.AddWithValue("@P12", cmbilce.Text);
            guncellemekomutu.Parameters.AddWithValue("@P13", txtvergidairesi.Text);
            guncellemekomutu.Parameters.AddWithValue("@P14", rchadres.Text);
            guncellemekomutu.Parameters.AddWithValue("@P15", txtkod1.Text);
            guncellemekomutu.Parameters.AddWithValue("@P16", txtkod2.Text);
            guncellemekomutu.Parameters.AddWithValue("@P17", txtkod3.Text);
            guncellemekomutu.Parameters.AddWithValue("@P18", txtid.Text);
            guncellemekomutu.ExecuteNonQuery();
            sql.baglanti().Close();
            DevExpress.XtraEditors.XtraMessageBox.Show("Firma bilgisi güncellenmişdir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}

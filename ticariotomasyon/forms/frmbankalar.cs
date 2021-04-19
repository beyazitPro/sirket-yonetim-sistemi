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
    public partial class frmbankalar : Form
    {
        public frmbankalar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute BankaBilgi", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FİRMALAR", sql.baglanti());
            da.Fill(dt);
            cmbfirma.Properties.ValueMember = "ID";
            cmbfirma.Properties.DisplayMember = "AD";
            cmbfirma.Properties.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtbankaad.Text = "";
            txthesapno.Text = "";
            txthesapturu.Text = "";
            txtiban.Text = "";
            txtsube.Text = "";
            txttarih.Text = "";
            txttelno.Text = "";
            txtyetkili.Text = "";
            cmbfirma.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void frmbankalar_Load(object sender, EventArgs e)
        {
            listele();
            cmbil.Properties.DataSource = sql.sehirlistesi();
            firmalistesi();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand eklemekomutu = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,SUBE,İL,İLCE,IBAN,HESAPNO,YETKILI,TELEFON,TARİH,HESAPTURU,FİRMAID) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", sql.baglanti());
            eklemekomutu.Parameters.AddWithValue("@P1", txtbankaad.Text);
            eklemekomutu.Parameters.AddWithValue("@P2", txtsube.Text);
            eklemekomutu.Parameters.AddWithValue("@P3", cmbil.Text);
            eklemekomutu.Parameters.AddWithValue("@P4", cmbilce.Text);
            eklemekomutu.Parameters.AddWithValue("@P5", txtiban.Text);
            eklemekomutu.Parameters.AddWithValue("@P6", txthesapno.Text);
            eklemekomutu.Parameters.AddWithValue("@P7", txtyetkili.Text);
            eklemekomutu.Parameters.AddWithValue("@P8", txttelno.Text);
            eklemekomutu.Parameters.AddWithValue("@P9", txttarih.Text);
            eklemekomutu.Parameters.AddWithValue("@P10", txthesapturu.Text);
            eklemekomutu.Parameters.AddWithValue("@P11", cmbfirma.EditValue);
            eklemekomutu.ExecuteNonQuery();
            sql.baglanti().Close();
            XtraMessageBox.Show("Banka Sisteme tanımlanmıştır.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void cmbil_EditValueChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.DataSource = sql.ilcelistesi(cmbil.ItemIndex + 1);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    txtid.Text = dr["ID"].ToString();
                    txtbankaad.Text = dr["BANKA ADI"].ToString();
                    txtsube.Text = dr["ŞUBE"].ToString();
                    txttelno.Text = dr["TELEFON"].ToString();
                    cmbil.Text = dr["İL"].ToString();
                    cmbilce.Text = dr["İLCE"].ToString();
                    txtiban.Text = dr["IBAN"].ToString();
                    txthesapno.Text = dr["HESAPNO"].ToString();
                    txtyetkili.Text = dr["YETKILI"].ToString();
                    txttarih.Text = dr["TARİH"].ToString();
                    txthesapturu.Text = dr["HESAP TÜRÜ"].ToString();
                    cmbfirma.Text = dr["FİRMA"].ToString();
                }
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
                if (txtid != null)
                {
                    DialogResult dialog = XtraMessageBox.Show("Veriyi silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        SqlCommand silmekomutu = new SqlCommand("Delete from TBL_BANKALAR where ID=@p1", sql.baglanti());
                        silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                        silmekomutu.ExecuteNonQuery();
                        sql.baglanti().Close();
                        XtraMessageBox.Show("Veri silinmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Lütfen silmeden önce bir satır seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlCommand guncellemekomutu = new SqlCommand("update  TBL_BANKALAR set BANKAADI=@p1,SUBE=@p2,İL=@p3,İLCE=@p4,IBAN=@p5,HESAPNO=@p6,YETKILI=@p7,TELEFON=@p8,TARİH=@p9,HESAPTURU=@p10,FİRMAID=@P11 where ID=@p12", sql.baglanti());
                guncellemekomutu.Parameters.AddWithValue("@p1", txtbankaad.Text);
                guncellemekomutu.Parameters.AddWithValue("@p2", txtsube.Text);
                guncellemekomutu.Parameters.AddWithValue("@p3", cmbil.Text);
                guncellemekomutu.Parameters.AddWithValue("@p4", cmbilce.Text);
                guncellemekomutu.Parameters.AddWithValue("@p5", txtiban.Text);
                guncellemekomutu.Parameters.AddWithValue("@p6", txthesapno.Text);
                guncellemekomutu.Parameters.AddWithValue("@p7", txtyetkili.Text);
                guncellemekomutu.Parameters.AddWithValue("@p8", txttelno.Text);
                guncellemekomutu.Parameters.AddWithValue("@p9", txttarih.Text);
                guncellemekomutu.Parameters.AddWithValue("@p10", txthesapturu.Text);
                guncellemekomutu.Parameters.AddWithValue("@p11", cmbfirma.EditValue);
                guncellemekomutu.Parameters.AddWithValue("@p12", txtid.Text);
                guncellemekomutu.ExecuteNonQuery();
                sql.baglanti().Close();
                XtraMessageBox.Show("Müşteri bilgileri güncelenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen tüm alanları kontrol ettikten sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

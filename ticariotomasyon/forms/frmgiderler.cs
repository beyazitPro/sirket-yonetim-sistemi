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
    public partial class frmgiderler : Form
    {
        public frmgiderler()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AY,YIL,ELEKTİRİK,SU,DOGALGAZ AS 'DOĞAL GAZ',INTERNET,MAASLAR AS 'MAAŞLAR',EKSTRA,NOTLAR from TBL_GİDERLER", sql.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            cmbay.Text = "";
            cmbyil.Text = "";
            txtelektirik.Text = "";
            txtsu.Text = "";
            txtdogalgaz.Text = "";
            txtinternet.Text = "";
            txtmaas.Text = "";
            txtextra.Text = "";
            txtnotlar.Text = "";
        }

        private void frmgiderler_Load(object sender, EventArgs e)
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
                SqlCommand eklemekomutu = new SqlCommand("insert into TBL_GİDERLER (AY,YIL,ELEKTİRİK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sql.baglanti());
                eklemekomutu.Parameters.AddWithValue("@p1", cmbay.Text);
                eklemekomutu.Parameters.AddWithValue("@p2", cmbyil.Text);
                eklemekomutu.Parameters.AddWithValue("@p3", decimal.Parse(txtelektirik.Text));
                eklemekomutu.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
                eklemekomutu.Parameters.AddWithValue("@p5", decimal.Parse(txtdogalgaz.Text));
                eklemekomutu.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
                eklemekomutu.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
                eklemekomutu.Parameters.AddWithValue("@p8", decimal.Parse(txtextra.Text));
                eklemekomutu.Parameters.AddWithValue("@p9", txtnotlar.Text);
                eklemekomutu.ExecuteNonQuery();
                XtraMessageBox.Show("Gider sisteme eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("bir hata ile karşılaşıldı lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            {
                temizle();
                DataRow dt = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (dt != null)
                {
                    txtid.Text = dt["ID"].ToString();
                    cmbay.Text = dt["AY"].ToString();
                    cmbyil.Text = dt["YIL"].ToString();
                    txtelektirik.Text = dt["ELEKTİRİK"].ToString();
                    txtsu.Text = dt["SU"].ToString();
                    txtdogalgaz.Text = dt["DOĞAL GAZ"].ToString();
                    txtinternet.Text = dt["INTERNET"].ToString();
                    txtmaas.Text = dt["MAAŞLAR"].ToString();
                    txtextra.Text = dt["EKSTRA"].ToString();
                    txtnotlar.Text = dt["NOTLAR"].ToString();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lüften daha sonra tekrar deneyin", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult dialog= XtraMessageBox.Show("Gideri listeden silmek istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog==DialogResult.Yes)
                {
                    SqlCommand silmekomutu = new SqlCommand("delete from TBL_GİDERLER Where ID=@p1", sql.baglanti());
                    silmekomutu.Parameters.AddWithValue("@p1", txtid.Text);
                    silmekomutu.ExecuteNonQuery();
                    sql.baglanti().Close();
                    XtraMessageBox.Show("gider listeden silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guncellemekomutu = new SqlCommand("Update TBL_GİDERLER set AY=@P1,YIL=@P2,ELEKTİRİK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 WHERE ID=@P10", sql.baglanti());
                guncellemekomutu.Parameters.AddWithValue("@p1", cmbay.Text);
                guncellemekomutu.Parameters.AddWithValue("@p2", cmbyil.Text);
                guncellemekomutu.Parameters.AddWithValue("@p3", decimal.Parse(txtelektirik.Text));
                guncellemekomutu.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
                guncellemekomutu.Parameters.AddWithValue("@p5", decimal.Parse(txtdogalgaz.Text));
                guncellemekomutu.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
                guncellemekomutu.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
                guncellemekomutu.Parameters.AddWithValue("@p8", decimal.Parse(txtextra.Text));
                guncellemekomutu.Parameters.AddWithValue("@p9", txtnotlar.Text);
                guncellemekomutu.Parameters.AddWithValue("@p10", txtid.Text);
                guncellemekomutu.ExecuteNonQuery();
                XtraMessageBox.Show("Gider güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void txtelektirik_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtsu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtdogalgaz_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtinternet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtmaas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void txtextra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }
    }
}

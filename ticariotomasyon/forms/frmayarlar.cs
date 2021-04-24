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
    public partial class frmayarlar : Form
    {
        public frmayarlar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();
        int id;

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_ADMİN", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtkulaniciadi.Text = "";
            txtsifre.Text = "";
            id = 0;
        }

        private void frmayarlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnkaydetguncelle.Text=="kaydet")
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_ADMİN (KULANİCİAD,SİFRE) Values (@p1,@p2)", sql.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtkulaniciadi.Text);
                    komut.Parameters.AddWithValue("@p2", txtsifre.Text);
                    komut.ExecuteNonQuery();
                    listele();
                    temizle();
                    XtraMessageBox.Show("kullanıcı sisteme tanımlanmıştır.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else if (btnkaydetguncelle.Text == "güncelle")
                {
                    SqlCommand komut = new SqlCommand("Update TBL_ADMİN set KULANİCİAD=@p1,SİFRE=@p2 where ID=@p3", sql.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtkulaniciadi.Text);
                    komut.Parameters.AddWithValue("@p2", txtsifre.Text);
                    komut.Parameters.AddWithValue("@p3",id);
                    komut.ExecuteNonQuery();
                    listele();
                    temizle();
                    XtraMessageBox.Show("kullanıcı bilgisi güncellenmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    btnkaydetguncelle.Text = "Kaydet";
                    btnkaydetguncelle.Appearance.BackColor = Color.LightCoral;
                }
                else if(btnkaydetguncelle.Text=="sil")
                {
                    SqlCommand komut = new SqlCommand("delete from TBL_ADMİN where ID=@p1", sql.baglanti());
                    komut.Parameters.AddWithValue("@p1", id);
                    komut.ExecuteNonQuery();
                    listele();
                    temizle();
                    XtraMessageBox.Show("kullanıcı silinmiştir.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    btnkaydetguncelle.Text = "Kaydet";
                    btnkaydetguncelle.Appearance.BackColor = Color.LightCoral;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("kullanıcı sisteme tanımlanırken beklenmeyen bir hata ile karşılaşıldı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr!=null)
                {
                    txtkulaniciadi.Text = dr["KULANİCİAD"].ToString();
                    txtsifre.Text = dr["SİFRE"].ToString();
                    id = int.Parse(dr["ID"].ToString());
                    btnkaydetguncelle.Text = "sil";
                    btnkaydetguncelle.Appearance.BackColor = Color.IndianRed;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("kullanıcı sisteme tanımlanırken beklenmeyen bir hata ile karşılaşıldı.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void txtkulaniciadi_TextChanged(object sender, EventArgs e)
        {
            if (txtkulaniciadi.Text!="")
            {
                if (btnkaydetguncelle.Text == "sil")
                {
                    btnkaydetguncelle.Text = "güncelle";
                    btnkaydetguncelle.Appearance.BackColor = Color.BlueViolet;
                }
            }
            else
            {
                btnkaydetguncelle.Text = "Kaydet";
                btnkaydetguncelle.Appearance.BackColor = Color.LightCoral;
            }
        }

        private void txtsifre_TextChanged(object sender, EventArgs e)
        {
            if (txtkulaniciadi.Text != "")
            {
                if (btnkaydetguncelle.Text == "sil")
                {
                    btnkaydetguncelle.Text = "güncelle";
                    btnkaydetguncelle.Appearance.BackColor = Color.BlueViolet;
                }
            }
            else
            {
                btnkaydetguncelle.Text = "Kaydet";
                btnkaydetguncelle.Appearance.BackColor = Color.LightCoral;
            }
        }
    }
}

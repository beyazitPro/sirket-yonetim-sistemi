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
using System.IO;
using DevExpress.XtraEditors;

namespace ticariotomasyon.forms
{
    public partial class frmadmin : Form
    {
        public frmadmin()
        {
            InitializeComponent();
        }

        string uygklasoru = Application.StartupPath;
        sqlbaglanti sql = new sqlbaglanti();

        private void btngirisyap_MouseLeave(object sender, EventArgs e)
        {
            btngirisyap.Appearance.BackColor = Color.LightCoral;
        }

        private void btngirisyap_MouseHover(object sender, EventArgs e)
        {
            btngirisyap.Appearance.BackColor = Color.Blue;
        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            if (cbbenihatirla.Checked==true)
            {
                File.WriteAllText(uygklasoru + @"\gerekli dosyalar\kulaniciadi.txt", txtkulaniciadi.Text);
                File.WriteAllText(uygklasoru + @"\gerekli dosyalar\sifre.txt", txtsifre.Text);
            }
            else
            {
                File.WriteAllText(uygklasoru + @"\gerekli dosyalar\kulaniciadi.txt","");
                File.WriteAllText(uygklasoru + @"\gerekli dosyalar\sifre.txt","");
            }
            SqlCommand komut = new SqlCommand("Select * from TBL_ADMİN where KULANİCİAD=@p1 and SİFRE=@p2", sql.baglanti());
            komut.Parameters.AddWithValue("@p1", txtkulaniciadi.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmanaform frm = new frmanaform();
                frm.Show();
                frm.kullancici = txtkulaniciadi.Text;
                this.Hide();
            }
            else
            {
                XtraMessageBox.Show("Kullanıcı adınız veya şifreniz yanlış.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmadmin_Load(object sender, EventArgs e)
        {
            StreamReader dr = new StreamReader(uygklasoru + @"\gerekli dosyalar\kulaniciadi.txt");
            string kulaniciadi = dr.ReadToEnd();
            if (kulaniciadi!=null)
            {
                StreamReader dr2 = new StreamReader(uygklasoru + @"\gerekli dosyalar\sifre.txt");
                string sifre = dr2.ReadToEnd();
                if (sifre!=null)
                {
                    txtkulaniciadi.Text = kulaniciadi;
                    txtsifre.Text = sifre;
                    cbbenihatirla.Checked = true;
                }
                dr2.Close();
            }
            dr.Close();
        }
    }
}

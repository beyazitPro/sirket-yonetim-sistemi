using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace ticariotomasyon.forms
{
    public partial class frmmail : Form
    {
        public frmmail()
        {
            InitializeComponent();
        }

        public string mail;

        void temizle()
        {
            txtmailadres.Text = mail;
            txtkonu.Text = "";
            txtmesaj.Text = "";
        }

        private void frmmail_Load(object sender, EventArgs e)
        {
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mesaj = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new NetworkCredential("Beysoglu@outlook.com", "3482Qwer");
                istemci.Port = 587;
                istemci.Host = "smtp.live.com";
                istemci.EnableSsl = true;
                mesaj.To.Add(txtmailadres.Text);
                mesaj.From = new MailAddress("Beysoglu@outlook.com");
                mesaj.Subject = txtkonu.Text;
                mesaj.Body = txtmesaj.Text;
                istemci.Send(mesaj);
                DevExpress.XtraEditors.XtraMessageBox.Show("Mail başarılı bir şekilde gönderildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bir hata ile karşılaşıldı lütfen daha sonra tekrar deneyiniz.", "hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = DevExpress.XtraEditors.XtraMessageBox.Show("iptal etmek isteğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog==DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}

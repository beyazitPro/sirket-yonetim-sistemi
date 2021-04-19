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
    public partial class frmraporlar : Form
    {
        public frmraporlar()
        {
            InitializeComponent();
        }

        sqlbaglanti sql = new sqlbaglanti();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE as 'Vergi Dairesi' from TBL_MUSTERI", sql.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select ID,AD,YETKILISTATU as 'Yetkili Statü',YETKILIADSOYAD as 'Yetkili Ad Soyad',YETKILITC as 'Yetkili TC',SEKTOR as 'Sektör',TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE as 'Vergi Dairesi',ADRES,OZELKOD1 as'Özel kod 1',OZELKOD2 as'Özel kod 2',OZELKOD3 as'Özel kod 3' from TBL_FİRMALAR", sql.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("select ID,AY,YIL,ELEKTİRİK,SU,DOGALGAZ AS 'DOĞAL GAZ',INTERNET,MAASLAR AS 'MAAŞLAR',EKSTRA,NOTLAR from TBL_GİDERLER", sql.baglanti());
            da3.Fill(dt3);
            gridControl3.DataSource = dt3;

            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("select * from TBL_PERSONEL", sql.baglanti());
            da4.Fill(dt4);
            gridControl4.DataSource = dt4;
        }

        private void frmraporlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "müşteri raporu PDF kayıt";
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.ShowDialog();
            gridControl1.ExportToPdf(saveFileDialog1.FileName);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "müşteri raporu Excel kayıt";
            saveFileDialog1.Filter = "EXCEL|*.xlsx";
            saveFileDialog1.ShowDialog();
            gridControl1.ExportToXlsx(saveFileDialog1.FileName);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "müşteri raporu Word kayıt";
            saveFileDialog1.Filter = "WORD|*.docx";
            saveFileDialog1.ShowDialog();
            gridControl1.ExportToDocx(saveFileDialog1.FileName);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "müşteri raporu TXT kayıt";
            saveFileDialog1.Filter = "TXT|*.txt";
            saveFileDialog1.ShowDialog();
            gridControl1.ExportToText(saveFileDialog1.FileName);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "müşteri raporu HTML kayıt";
            saveFileDialog1.Filter = "HTML|*.html";
            saveFileDialog1.ShowDialog();
            gridControl1.ExportToHtml(saveFileDialog1.FileName);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Firma raporu PDF kayıt";
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.ShowDialog();
            gridControl2.ExportToPdf(saveFileDialog1.FileName);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "firma raporu Excel kayıt";
            saveFileDialog1.Filter = "EXCEL|*.xlsx";
            saveFileDialog1.ShowDialog();
            gridControl2.ExportToXlsx(saveFileDialog1.FileName);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "firma raporu Word kayıt";
            saveFileDialog1.Filter = "WORD|*.docx";
            saveFileDialog1.ShowDialog();
            gridControl2.ExportToDocx(saveFileDialog1.FileName);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "firma raporu TXT kayıt";
            saveFileDialog1.Filter = "TXT|*.txt";
            saveFileDialog1.ShowDialog();
            gridControl2.ExportToText(saveFileDialog1.FileName);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Firma raporu HTML kayıt";
            saveFileDialog1.Filter = "HTML|*.html";
            saveFileDialog1.ShowDialog();
            gridControl2.ExportToHtml(saveFileDialog1.FileName);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Gider raporu PDF kayıt";
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.ShowDialog();
            gridControl3.ExportToPdf(saveFileDialog1.FileName);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "gider raporu Excel kayıt";
            saveFileDialog1.Filter = "EXCEL|*.xlsx";
            saveFileDialog1.ShowDialog();
            gridControl3.ExportToXlsx(saveFileDialog1.FileName);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "gider raporu Word kayıt";
            saveFileDialog1.Filter = "WORD|*.docx";
            saveFileDialog1.ShowDialog();
            gridControl3.ExportToDocx(saveFileDialog1.FileName);
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "gider raporu TXT kayıt";
            saveFileDialog1.Filter = "TXT|*.txt";
            saveFileDialog1.ShowDialog();
            gridControl3.ExportToText(saveFileDialog1.FileName);
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "gider raporu HTML kayıt";
            saveFileDialog1.Filter = "HTML|*.html";
            saveFileDialog1.ShowDialog();
            gridControl3.ExportToHtml(saveFileDialog1.FileName);
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Personel raporu PDF kayıt";
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.ShowDialog();
            gridControl4.ExportToPdf(saveFileDialog1.FileName);
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Personel raporu Excel kayıt";
            saveFileDialog1.Filter = "EXCEL|*.xlsx";
            saveFileDialog1.ShowDialog();
            gridControl4.ExportToXlsx(saveFileDialog1.FileName);
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "personel raporu Word kayıt";
            saveFileDialog1.Filter = "WORD|*.docx";
            saveFileDialog1.ShowDialog();
            gridControl4.ExportToDocx(saveFileDialog1.FileName);
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "personel raporu TXT kayıt";
            saveFileDialog1.Filter = "TXT|*.txt";
            saveFileDialog1.ShowDialog();
            gridControl4.ExportToText(saveFileDialog1.FileName);
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog1.Title = "Persoenel raporu HTML kayıt";
            saveFileDialog1.Filter = "HTML|*.html";
            saveFileDialog1.ShowDialog();
            gridControl4.ExportToHtml(saveFileDialog1.FileName);
        }
    }
}

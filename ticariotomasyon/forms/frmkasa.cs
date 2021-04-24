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
using DevExpress.Charts;

namespace ticariotomasyon.forms
{
    public partial class frmkasa : Form
    {
        public frmkasa()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();
        public string kulanici;

        void girishareketleriliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute musreıhareketlerı", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;

            dt = new DataTable();
            da = new SqlDataAdapter("execute firmahareketleri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void istatistik()
        {
            //Toplam tutarı çekme
            SqlCommand komut = new SqlCommand("select sum(TUTAR) from TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lbltoplamtutar.Text = okuyucu[0] + " TL";
            }
            okuyucu.Close();
            okuyucu = null;

            //ödemeleri çekme
            komut = new SqlCommand("Select (ELEKTİRİK+SU+DOGALGAZ+INTERNET+MAASLAR+EKSTRA) from TBL_GİDERLER order by ID asc", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblodemeler.Text = okuyucu[0] + " TL";
            }
            okuyucu.Close();
            okuyucu = null;

            //personel maaşları çekme
            komut = new SqlCommand("Select MAASLAR from TBL_GİDERLER order by ID asc", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblpersonelmaas.Text = okuyucu[0] + " TL";
            }
            okuyucu.Close();
            okuyucu = null;

            //müşteri sayısı çekme
            komut = new SqlCommand("Select count(*) from TBL_MUSTERI", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblmusterısayısı.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;

            //firma sayısı çekme
            komut = new SqlCommand("Select count(*) from TBL_FİRMALAR", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblfirmasayısı.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;

            //firma şehir sayısı çekme
            komut = new SqlCommand("select COUNT(Distinct(IL)) from TBL_FİRMALAR", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblsehir.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;

            //müşteri şehir sayısı çekme
            komut = new SqlCommand("select COUNT(Distinct(IL)) from TBL_MUSTERI", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblsehir2.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;

            //personel sayısı çekme
            komut = new SqlCommand("select COUNT(*) from TBL_PERSONEL", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblpersonel.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;

            //stok sayısını çekme
            komut = new SqlCommand("select sum(ADET) from TBL_URUN", bgl.baglanti());
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                lblstok.Text = okuyucu[0].ToString();
            }
            okuyucu.Close();
            okuyucu = null;
            bgl.baglanti().Close();
        }

        void giderliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AY,YIL,ELEKTİRİK,SU,DOGALGAZ AS 'DOĞAL GAZ',INTERNET,MAASLAR AS 'MAAŞLAR',EKSTRA,NOTLAR from TBL_GİDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        private void frmkasa_Load(object sender, EventArgs e)
        {
            lblkulanici.Text = kulanici;
            girishareketleriliste();
            istatistik();
            giderliste();
        }

        int sayac2 = 0;
        int ensondeger;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            SqlCommand chartkomutu;
            SqlDataReader chartokuyucu;
            if (sayac2==1)
            {
                chartkomutu = new SqlCommand("select Top 1 ID  from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    ensondeger = int.Parse(chartokuyucu[0].ToString());
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }

            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl2.Text = "Elektirik";
                chartControl2.Series["Aylar"].View.Color = Color.Yellow;
                chartkomutu = new SqlCommand("Select Top 6 ay,ELEKTİRİK from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl2.Text = "Su";
                chartControl2.Series["Aylar"].View.Color = Color.Blue;
                chartkomutu = new SqlCommand("Select top 6 ay,su from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl2.Text = "Doğal gaz";
                chartControl2.Series["Aylar"].View.Color = Color.Green;
                chartkomutu = new SqlCommand("Select top 6 ay,DOGALGAZ from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl2.Text = "İnternet";
                chartControl2.Series["Aylar"].View.Color = Color.SkyBlue;
                chartkomutu = new SqlCommand("Select top 6 ay,INTERNET from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl2.Text = "Ekstra harcamalar";
                chartControl2.Series["Aylar"].View.Color = Color.Red;
                chartkomutu = new SqlCommand("Select top 6 ay,EKSTRA from TBL_GİDERLER order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else
            {
                sayac2 = 0;
            }
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            ensondeger = ensondeger - 6;
            SqlCommand chartkomutu;
            SqlDataReader chartokuyucu;

            if (sayac > 0 && sayac <= 5)
            {
                groupControl1.Text = "Elektirik";
                chartControl1.Series["Aylar"].View.Color = Color.Yellow;
                chartkomutu = new SqlCommand("Select Top 6 ay,ELEKTİRİK from TBL_GİDERLER  where ID<=" + ensondeger, bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;

            }
            else if (sayac > 5 && sayac <= 10)
            {
                groupControl1.Text = "Su";
                chartControl1.Series["Aylar"].View.Color = Color.Blue;
                chartkomutu = new SqlCommand("Select Top 6 ay,su from TBL_GİDERLER where ID<=" + ensondeger + " order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac > 10 && sayac <= 15)
            {
                groupControl1.Text = "Doğal gaz";
                chartControl1.Series["Aylar"].View.Color = Color.Green;
                chartkomutu = new SqlCommand("Select Top 6 ay,DOGALGAZ from TBL_GİDERLER where ID<=" + ensondeger + " order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac > 15 && sayac <= 20)
            {
                groupControl1.Text = "İnternet";
                chartControl1.Series["Aylar"].View.Color = Color.SkyBlue;
                chartkomutu = new SqlCommand("Select Top 6 ay,INTERNET from TBL_GİDERLER where ID<=" + ensondeger + " order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else if (sayac > 20 && sayac <= 25)
            {
                groupControl1.Text = "Ekstra harcamalar";
                chartControl1.Series["Aylar"].View.Color = Color.Red;
                chartkomutu = new SqlCommand("Select Top 6 ay,EKSTRA from TBL_GİDERLER where ID<=" + ensondeger + " order by ID desc", bgl.baglanti());
                chartokuyucu = chartkomutu.ExecuteReader();
                while (chartokuyucu.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(chartokuyucu[0], chartokuyucu[1]));
                }
                chartokuyucu.Close();
                chartokuyucu = null;
            }
            else
            {
                sayac = 0;
            }
        }
    }
}

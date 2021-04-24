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
using System.Xml;

namespace ticariotomasyon.forms
{
    public partial class frmanamodul : Form
    {
        public frmanamodul()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();

        void listele()
        {
            //azalan stoklar
            DataTable dt = new DataTable();
            SqlDataAdapter komut = new SqlDataAdapter("select URUNAD,SUM(ADET) as 'ADET' from TBL_URUN group by URUNAD having Sum(ADET)<=10 order by sum(ADET)",bgl.baglanti());
            komut.Fill(dt);
            stoklargrid.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter komut2 = new SqlDataAdapter("select top 10 TARİHVESAAT,BASLIK from TBL_NOTLAR order by ID desc", bgl.baglanti());
            komut2.Fill(dt2);
            gridControlajanda.DataSource = dt2;

            DataTable dt3 = new DataTable();
            SqlDataAdapter komut3 = new SqlDataAdapter("exec firmahareket2", bgl.baglanti());
            komut3.Fill(dt3);
            gridControlhareket.DataSource = dt3;

            DataTable dt4 = new DataTable();
            SqlDataAdapter komut4 = new SqlDataAdapter("select AD,TELEFON1,MAIL,FAX from TBL_FİRMALAR", bgl.baglanti());
            komut4.Fill(dt4);
            gridControlfirma.DataSource = dt4;
        }

        void xmlcek()
        {
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            XmlTextReader reader = new XmlTextReader(" http://www.hurriyet.com.tr/rss/gundem");
            while (reader.Read())
            {
                if (reader.Name=="title")
                {
                    listBox1.Items.Add(reader.ReadString());
                }
            }
            reader.Close();
        }

        private void frmanamodul_Load(object sender, EventArgs e)
        {
            listele();
            xmlcek();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ticariotomasyon
{
    class sqlbaglanti
    {
        public SqlConnection baglanti()
        {
            string uygklassoru = Application.StartupPath;
            string veri;
            StreamReader sr = new StreamReader(uygklassoru + @"\gerekli dosyalar\sql.txt");
            veri = sr.ReadToEnd();
            SqlConnection bgl = new SqlConnection(veri);
            bgl.Open();
            return bgl;
        }

        public List<string> sehirlistesi()
        {
            //comboboxa şehirlerin çekilmesi
            List<string> illiste = new List<string>();
            SqlCommand sehirliste = new SqlCommand("select sehir from TBL_İLLER",baglanti());
            SqlDataReader dr = sehirliste.ExecuteReader();
            while (dr.Read())
            {
                illiste.Add(dr[0].ToString());
            }
            dr.Close();
            baglanti().Close();
            return illiste;
        }

        public List<string> ilcelistesi(int index)
        {
            //comboboxa ilerin çekilmesi
            List<string> ilceliste = new List<string>();
            ilceliste.Clear();
            SqlCommand ilcecek = new SqlCommand("select ilce from TBL_İLCELER where sehir=@p1",baglanti());
            ilcecek.Parameters.AddWithValue("@p1",index);
            SqlDataReader dr = ilcecek.ExecuteReader();
            while (dr.Read())
            {
                ilceliste.Add(dr[0].ToString());
            }
            dr.Close();
            baglanti().Close();
            return ilceliste;
        }
    }
}

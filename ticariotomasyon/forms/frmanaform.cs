using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ticariotomasyon.forms;

namespace ticariotomasyon
{
    public partial class frmanaform : Form
    {
        public frmanaform()
        {
            InitializeComponent();
        }

        frmurunler fr;
        frmmusteriler fr2;
        frmfirmalar fr3;
        frmpersoneler fr4;
        frmrehber fr5;
        frmgiderler fr6;
        frmbankalar fr7;
        faturalar fr8;
        frmnotlar fr9;
        frmhareketler fr10;
        frmraporlar fr11;
        frmstoklar fr12;
        frmayarlar fr13;
        frmkasa fr14;
        frmanamodul fr15;
        public string kullancici;

        void anamodul()
        {
            if (fr15 == null||fr15.IsDisposed)
            {
                fr15 = new frmanamodul();
                fr15.MdiParent = this;
                fr15.Show();
            }
        }

        private void btnurunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr==null || fr.IsDisposed)
            {
                fr = new frmurunler();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void btnmusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2==null || fr2.IsDisposed)
            {
                fr2 = new frmmusteriler();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }

        private void btnfirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3==null || fr3.IsDisposed)
            {
                fr3 = new frmfirmalar();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }

        private void btnpersoneler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4==null || fr4.IsDisposed)
            {
                fr4 = new frmpersoneler();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }

        private void btnrehberler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5==null || fr5.IsDisposed)
            {
                fr5 = new frmrehber();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }

        private void btngiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6==null || fr6.IsDisposed)
            {
                fr6 = new frmgiderler();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }

        private void btnbankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7==null || fr7.IsDisposed)
            {
                fr7 = new frmbankalar();
                fr7.MdiParent = this;
                fr7.Show();
            }
        }

        private void btnfatura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr8 == null || fr8.IsDisposed)
            {
                fr8 = new faturalar();
                fr8.MdiParent = this;
                fr8.Show();
            }
        }

        private void btnnotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr9==null || fr9.IsDisposed)
            {
                fr9 = new frmnotlar();
                fr9.MdiParent = this;
                fr9.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr10==null || fr10.IsDisposed)
            {
                fr10 = new frmhareketler();
                fr10.MdiParent = this;
                fr10.Show();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr11 == null || fr11.IsDisposed)
            {
                fr11 = new frmraporlar();
                fr11.MdiParent = this;
                fr11.Show();
            }
        }

        private void btnstoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr12 == null || fr12.IsDisposed)
            {
                fr12 = new frmstoklar();
                fr12.MdiParent = this;
                fr12.Show();
            }
        }

        private void frmanaform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnayarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr13 == null || fr13.IsDisposed)
            {
                fr13 = new frmayarlar();
                fr13.Show();
            }
        }

        private void btnkasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr14==null || fr14.IsDisposed)
            {
                fr14 = new frmkasa();
                fr14.kulanici = kullancici;
                fr14.MdiParent = this;
                fr14.Show();
            }
        }

        private void btnanasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anamodul();
        }

        private void frmanaform_Load(object sender, EventArgs e)
        {
            anamodul();
        }
    }
}

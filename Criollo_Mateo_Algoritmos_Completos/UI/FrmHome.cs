using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Criollo_Mateo_Algoritmos_Completos.UI
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FrmLinea frm = new FrmLinea())
            {
                frm.ShowDialog();
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FrmRelleno frm = new FrmRelleno())
            {
                frm.ShowDialog();
            }

            this.Close();
        }
    }
}

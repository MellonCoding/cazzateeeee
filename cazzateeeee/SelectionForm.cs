using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace cazzateeeee
{
    public partial class SelectionForm : Form
    {
        private static int PLAYERvsPLAYERmod = 0;
        private static int PLAYERvsBOTmod = 1;
        private static int BOTvsBOTmod = 2;

        public SelectionForm()
        {
            InitializeComponent();
        }

        private void btnPlayerPlayer_Click(object sender, EventArgs e)
        {
            GameForm gf = new GameForm(PLAYERvsPLAYERmod, this);
            gf.Show();
            this.Hide();
        }

        private void btnPlayerBot_Click(object sender, EventArgs e)
        {
            GameForm gf = new GameForm(PLAYERvsBOTmod, this);
            gf.Show();
            this.Hide();
        }

        private void btnBotBot_Click(object sender, EventArgs e)
        {
            GameForm gf = new GameForm(BOTvsBOTmod, this);
            gf.Show();
            this.Hide();
        }
    }
}

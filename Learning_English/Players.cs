using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Learning_English
{
    public partial class Players : Form
    {
        private Form1 Mainform;
        public Players(Form1 mainform)
        {
            InitializeComponent();
            Mainform = mainform;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Difficulty form1 = new Difficulty(this);
            form1.Show();
        }

        private void Players_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Spelling form1 = new Spelling(this);
            form1.Show();
        }
    }
}

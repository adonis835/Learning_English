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
    public partial class Difficulty : Form
    {
        private Players Mainform;
        public Difficulty(Players form)
        {
            InitializeComponent();
            Mainform = form;
        }
        //Επιλογή παιχνιδιού ανάλογα με το επίπεδο δυσκολίας
        private void Selection()
        {
            label1.Text = "Select Game";
            label1.Location = new Point(218, 45);
            label1.Size = new Size(437, 61);
            label2.Text = "True/False";
            label2.Location = new Point(315, 185);
            label2.Size = new Size(223, 55);
            label3.Visible = false;
            label4.Text = "Back";
            label4.Location = new Point(370, 235);
        }
        private void Selection_Medium()
        {
            label1.Text = "Select Game";
            label1.Location = new Point(218, 45);
            label1.Size = new Size(437, 61);
            label2.Text = "Describe the Picture";
            label2.Location = new Point(225, 185);
            label2.Size = new Size(400, 55);
            label3.Visible = false;
            label4.Text = "Back";
            label4.Location = new Point(370, 235);
        }
        private void Selection_Hard()
        {
            label1.Text = "Select Game";
            label1.Location = new Point(218, 45);
            label1.Size = new Size(437, 61);
            label2.Text = "Crossword";
            label2.Location = new Point(305, 185);
            label2.Size = new Size(230, 55);
            label3.Visible = false;
            label4.Text = "Back";
            label4.Location = new Point(370, 235);
        }


        private void label2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            label2.ForeColor = Color.Black;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            label3.ForeColor = Color.Red;
        }
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            label3.ForeColor = Color.Black;
        }
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            label4.ForeColor = Color.Red;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            label4.ForeColor = Color.Black;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Easy")
            {
                Selection();
                label5.Visible = false;
            }
            else if (label2.Text == "True/False")
            {
                this.Hide();
                TrueFalse form1 = new TrueFalse(this);
                form1.Show();
            }
            else if (label2.Text == "Describe the Picture")
            {
                this.Hide();
                Picture form1 = new Picture(this);
                form1.Show();
            }
            else if (label2.Text == "Crossword")
            {
                this.Hide();
                Crossword form1 = new Crossword(this);
                form1.Show();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label4.Text == "Back")
            {
                label1.Text = "Select Difficulty";
                label1.Location = new Point(218, 45);
                label1.Size = new Size(437, 61);
                label2.Text = "Easy";
                label2.Location = new Point(375, 165);
                label2.Size = new Size(106, 55);
                label3.Text = "Medium";
                label3.Location = new Point(341, 220);
                label3.Size = new Size(170, 55);
                label4.Text = "Hard";
                label4.Location = new Point(368, 275);
                label4.Size = new Size(113, 55);
                label5.Visible = true;
                label3.Visible = true;
            }
            else
            {
                Selection_Hard();
                label5.Visible = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if(label3.Text == "Medium")
            {
                Selection_Medium();
                label5.Visible = false;
            }
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            label5.ForeColor = Color.Red;
        }
        private void label5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            label5.ForeColor = Color.White;
        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            Mainform.Show();
        }

        private void Difficulty_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }
    }
}

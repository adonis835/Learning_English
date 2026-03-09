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
    public partial class Revision : Form
    {
        private Form1 Mainform;
        int image = 1;
        public Revision(Form1 form)
        {
            InitializeComponent();
            Mainform = form;
        }

        // Εμφανίζει τις εικόνες και το κουμπί που σε παίρνει στην επόμενη εικόνα
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Exit")
            {
                this.Close();
            }

            image++;
            if (image == 2)
            {
                label1.Visible = false;
                pictureBox1.Size = new Size(780, 535);
                pictureBox1.Location = new Point(12, 8);
                pictureBox1.BackgroundImage = Properties.Resources.kitchen_voc;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                button1.Text = "Bedroom";
            }
            else if (image == 3)
            {
                label1.Visible = true;
                label1.Text = "Bedroom vocabulary";
                label1.Location = new Point(180, 10);
                pictureBox1.BackgroundImage = Properties.Resources.bedroom_voc;
                pictureBox1.Size = new Size(780, 491);
                pictureBox1.Location = new Point(-2, 54);
                button1.Text = "Classroom";
            }
            else if (image == 4)
            {
                label1.Visible = true;
                label1.Text = "Classroom vocabulary";
                label1.Location = new Point(180, 10);
                pictureBox1.BackgroundImage = Properties.Resources.download;
                pictureBox1.Size = new Size(780, 491);
                pictureBox1.Location = new Point(-2, 54);
                button1.Text = "Garden";
            }
            else if (image == 5)
            {
                label1.Visible = true;
                label1.Text = "Garden vocabulary";
                label1.Location = new Point(180, 10);
                pictureBox1.BackgroundImage = Properties.Resources.garden;
                pictureBox1.Size = new Size(780, 491);
                pictureBox1.Location = new Point(-2, 54);
                button1.Text = "Activities";
            } else if(image == 6)
            {
                label1.Visible = true;
                label1.Text = "Activities vocabulary";
                label1.Location = new Point(180, 10);
                pictureBox1.BackgroundImage = Properties.Resources.activities;
                pictureBox1.Size = new Size(780, 491);
                pictureBox1.Location = new Point(-2, 54);
                button1.Text = "Exit";
            }
           
        }

        private void Revision_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }
    }
}

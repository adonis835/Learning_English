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
    public partial class Picture : Form
    {
        private Difficulty Mainform;
        int img = 1;
        // Αντικείμενα κάθε εικόνας
        String[] objects = {"clock","sofa","television","window","curtains","cushion","books",
                             "vase","plant","box","rug","shelf","bookcase","chalk"};

        String[] objects2 = {"fridge","microwave","oven","chair","table","sink","kettle",
                             "fruit","window","mug","cupboard","pot","bowl","knife","spatula","towel","drawer","hood","phone","toaster","napkins","blinds" };

        String[] objects3 = {"clock","blackboard","desk","books","pencils","pens","drawer","chair","rubber","pencil case","globe",
                             "fish tank","door","lightswitch","coats","marker","duster","coat rack","box file","fishes","bookcase","notebook"
                            ,"ruler","bin","sharpener","poster","map","window","chalk"};
        int count = 0;
        int count1 = 0;
        int count2 = 0;
        public Picture(Difficulty mainform)
        {
            InitializeComponent();
            Mainform = mainform;
            MessageBox.Show("You will see a picture and you have to name the items you see in it. " +
                "Each picture requests a different amount of items to be found! " +
                "Press the 'Submit' button or the Enter key to submit your answer.\n" +
                "Good luck!");

            listBox1.Items.Clear();
            pictureBox1.Image = Properties.Resources.living_room;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Text = "Name 8 items you see in the picture";
        }
        // Εξετάζει για κάθε εικόνα τα αντικείμενα που υπάρχουν σε αυτήν με την λέξη που πληκτρολογεί ο χρήστης
        private void Words()
        {
            string words = textBox1.Text.ToLower().Trim();
            bool found = false;
            bool alreadyInList = false;

            if (img == 1)
            {
                foreach (string obj in objects)
                {
                    if (words.Contains(obj))
                    {
                        if (!listBox1.Items.Contains(obj))
                        {
                            listBox1.Items.Add(obj);
                            count++;
                            found = true;
                        }
                        else
                        {
                            alreadyInList = true;
                            found = true;
                        }
                    }
                }
                label1.Text = $"Items Found: {count}";


                if (count == 8)
                {
                    MessageBox.Show("Congratulations! You found them!!");
                    img = 2;
                    image2();
                }
            }
            else if (img == 2)
            {

                foreach (string obj in objects2)
                {
                    if (words.Contains(obj))
                    {
                        if (!listBox1.Items.Contains(obj))
                        {
                            listBox1.Items.Add(obj);
                            count1++;
                            found = true;
                        }
                        else
                        {
                            alreadyInList = true;
                            found = true;
                        }
                    }
                }
                label1.Text = $"Items Found: {count1}";


                if (count1 == 13)
                {
                    MessageBox.Show("Congratulations! You found them.");
                    img = 3;
                    image3();
                }
            }

            else if (img == 3)
            {
                foreach (string obj in objects3)
                {
                    if (words.Contains(obj))
                    {
                        if (!listBox1.Items.Contains(obj))
                        {
                            listBox1.Items.Add(obj);
                            count2++;
                            found = true;
                        }
                        else
                        {
                            alreadyInList = true;
                            found = true;
                        }
                    }
                }
                label1.Text = $"Items Found: {count2}";

                if (count2 == 18)
                {
                    MessageBox.Show("Congratulations! You completed the game!");
                    this.Close();
                    Mainform.Show();
                }
            }

            // Εαν δεν βρεθεί το αντικείμενο ή αν υπάρχει ήδη στη λίστα, εμφανίζει κατάλληλο μήνυμα
            if (!found)
            {
                MessageBox.Show("This item is not in the picture.");
            }
            else if (alreadyInList)
            {
                MessageBox.Show("You already added this item to the list.");
            }

            textBox1.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
           Words();

        }

        // Σύνδεσα το κουμπί με το Enter ετσι ώστε να μην χρειάζεται να πατάει το κουμπί κάθε φορά
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Αποτρέπει την εισαγωγή νέας γραμμής
                Words();
                textBox1.Clear(); 
            }
        }

        private void image2()
        {
            label4.Text = "1/3";
            listBox1.Items.Clear();
            count1 = 0;
            label1.Text = "Items Found: 0";
            pictureBox1.Image = Properties.Resources.kitchen;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Text = "Name 13 items you see in the picture";
        }
        private void image3()
        {
            label4.Text = "2/3";
            listBox1.Items.Clear();
            count2 = 0;
            label1.Text = "Items Found: 0";
            pictureBox1.Image = Properties.Resources.classroom;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label2.Text = "Name 18 items you see in the picture";
        }

        private void Picture_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }
    }
}

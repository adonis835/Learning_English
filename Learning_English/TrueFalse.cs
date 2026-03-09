using System;
using System.Drawing;
using System.Windows.Forms;

namespace Learning_English
{
    public partial class TrueFalse : Form
    {
        private Difficulty Mainform;
        bool goleft, goright;

        // Θέση του σκύλου
        int dogX = 360;
        int dogY = 385;
        int Playerspeed = 10; // Ταχύτητα του σκύλου
        Bitmap Dog;

        // Διαστάσεις του σκύλου
        const int Width = 70;
        const int Height = 70;

        // Πλατφόρμα και όρια πτώσης
        int Platformtop = 385;
        int Platformleft = 55;
        int Platformright = 740;
        int Fall = 540;

        // Θέση εμφάνισης του σκύλου όταν ξεκινάει το παιχνίδι
        int spawnX = 360;
        int spawnY = 385;

        // Ταχύτητα πτώσης του σκύλου
        int Fallspeed = 10;
        bool Falling = false;


        int q = 0;
        bool[] answers = new bool[10]; 
        bool Answered = false;
        bool waiting = false;

        string[] questions = new string[10];
        Bitmap[] Images = new Bitmap[10];


        public TrueFalse(Difficulty mainform)
        {
            InitializeComponent();
            Mainform = mainform;
            MessageBox.Show("You will see the description of the image and the image. If the description matches the image " +
                "then move the dog to the right for true else move it to the left for false.\n" +
                "Good luck!");

            answers[0] = true;
            answers[1] = false;
            answers[2] = true;
            answers[3] = true;
            answers[4] = false;
            answers[5] = false;
            answers[6] = true;
            answers[7] = false;
            answers[8] = true;
            answers[9] = true;

            questions[0] = "The child is eating an apple";
            questions[1] = "The children are studying";
            questions[2] = "The lady is cooking";
            questions[3] = "The children are playing football";
            questions[4] = "The children are sleeping";
            questions[5] = "The children are fishing";
            questions[6] = "The child is watering the flower";
            questions[7] = "The child is playing with the dog";
            questions[8] = "The children are swimming in the pool";
            questions[9] = "The children are watching TV";

            Images[0] = Properties.Resources.apple;
            Images[1] = Properties.Resources.bicycle;
            Images[2] = Properties.Resources.cooking;
            Images[3] = Properties.Resources.football;
            Images[4] = Properties.Resources.party;
            Images[5] = Properties.Resources.picnic;
            Images[6] = Properties.Resources.plants;
            Images[7] = Properties.Resources.study;
            Images[8] = Properties.Resources.swimming;
            Images[9] = Properties.Resources.Tv;

            question.Text = q + "/10";

            this.KeyPreview = true; // Για να λαμβάνει τα πλήκτρα του πληκτρολογίου πριν από άλλα controls
            this.DoubleBuffered = true; // Για να μειωθεί το τρεμόπαιγμα κατά την ανανέωση του form

            Dog = Properties.Resources.dog_right1;

            GameTimer.Start();

            label1.Text = questions[q];  // για να εμφανίζεται η πρώτη ερώτηση
            pictureBox2.Image = Images[q];
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (waiting) return; // Αν περιμένουμε να γίνει reset, δεν κάνουμε τίποτα

            if (goleft) // Αν πατάμε το αριστερό βέλος
            {
                dogX -= Playerspeed; 
                Dog = Properties.Resources.dog_left;
            }
            if (goright) // Αν πατάμε το δεξί βέλος
            {
                dogX += Playerspeed;
                Dog = Properties.Resources.dog_right1;
            }
            if (dogX < 0) // Αν ο σκύλος βγει εκτός οθόνης αριστερά να μην επιτρέπεται
            {
                dogX = 0;
            }
            if (dogX + Width > this.ClientSize.Width) // Αν ο σκύλος βγει εκτός οθόνης δεξιά να μην επιτρέπεται
            {
                dogX = this.ClientSize.Width - Width;
            }

            // Πτώση από πλατφόρμα
            if (dogX < Platformleft || dogX + Width > Platformright) 
            {
                Falling = true;
            }
            else
            {
                Falling = false;
                dogY = Platformtop;
            }

            
            if (Falling)
            {
                dogY += Fallspeed;
            }

            // Έλεγχος αν ο σκύλος έχει πέσει έξω από το όριο πτώσης και ξαναεμφανίζεται στην αρχική θέση
            if (dogY > Fall && !Answered)
            {
                Rectangle dogRect = new Rectangle(dogX, dogY, Width, Height);

                if (dogRect.IntersectsWith(label3.Bounds)) // Έλεγχος αν ο σκύλος πήγε στο False
                {
                    Answered = true;
                    waiting = true;
                    HandleAnswer(false);
                }
                else if (dogRect.IntersectsWith(label2.Bounds)) // Έλεγχος αν ο σκύλος πήγε στο True
                {
                    Answered = true;
                    waiting = true;
                    HandleAnswer(true);
                }
            }

            Invalidate(); // Ανανεώνει το form
        }



        private void HandleAnswer(bool selectedAnswer) // Ελεγχος απάντησης
        {
            if (answers[q] == selectedAnswer)
            {
                q++;
                if (q >= 10) // Αν ολοκληρώθηκαν όλες οι ερωτήσεις
                {
                    MessageBox.Show("Congratulations! You finished the game!");
                    this.Close();
                    Mainform.Show();
                }
                // Ενημέρωση της ερώτησης
                if (q < 10)
                {
                    question.Text = q + "/10";
                    label1.Text = questions[q];
                    pictureBox2.Image = Images[q];

                    // Ενημέρωση θέσης ερώτησης
                    switch (q)
                    {
                        case 2:
                            label1.Location = new Point(225, 288);
                            break;
                        case 3:
                            label1.Location = new Point(110, 288);
                            break;
                        case 5:
                            label1.Location = new Point(205, 288);
                            break;
                        case 6:
                        case 7:
                            label1.Location = new Point(130, 288);
                            break;
                        case 8:
                            label1.Location = new Point(45, 288);
                            break;
                        default:
                            label1.Location = new Point(169, 288);
                            break;
                    }
                }
            }
            else // Αν η απάντηση είναι λάθος
            {
                MessageBox.Show("Wrong answer! Try again.");
            }

            // Επαναφορά του σκύλου στην αρχική θέση
            dogX = spawnX;
            dogY = spawnY;
            Falling = false;
            Answered = false;
            waiting = false;
        }




        protected override void OnPaint(PaintEventArgs e) // Σχεδίαση του σκύλου διότι με picturebox τρεμοπαίζει πολύ
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Dog, new Rectangle(dogX, dogY, Width, Height));
        }

        private void TrueFalse_KeyDown(object sender, KeyEventArgs e) // Χειρισμός πλήκτρων για μετακίνηση του σκύλου
        {
            if (e.KeyCode == Keys.Left) goleft = true;
            if (e.KeyCode == Keys.Right) goright = true;
        }

        private void TrueFalse_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }

        private void TrueFalse_KeyUp(object sender, KeyEventArgs e) // Χειρισμός πλήκτρων για σταμάτημα του σκύλου
        {
            if (e.KeyCode == Keys.Left) goleft = false;
            if (e.KeyCode == Keys.Right) goright = false;
        }
    }
}

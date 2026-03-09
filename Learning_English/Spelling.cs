using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;


namespace Learning_English
{
    public partial class Spelling : Form
    {
        int timeLeft;
        private Players Mainform;
        List<String> words = new List<string>()
        { "mattress", "leaf", "jump", "scissors", "refrigerator", "motorbike", "student",
        "ceiling", "dance", "pillow", "fence", "bookshelf","blanket","sunflower","gloves","computer","office","cinema","slippers","homework","sing" };

        int Score1 = 0;
        int Score2 = 0;
        int currentPlayer = 1;
        string currentWord = "";
        int Starter = 1;
        int Rounds = 20;
        int roundsPlayed = 0;
        // Για την αναπαραγωγή της λέξης απο το ρομπότ
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        Random rnd = new Random();
        bool Steal = false;

        public Spelling(Players mainform)
        {
            InitializeComponent();
            Mainform = mainform;
            synthesizer.SelectVoice("Microsoft Zira Desktop");

            MessageBox.Show(
                "Each player gets a random word and must spell it in the textbox within the time limit.\n" +
                "For each correct spelling, you earn 10 points.\n" +
                "If your spelling is incorrect, it's the other's player turn to steal the word and earn 15 points, but they only have 15 seconds to answer.\n" +
                "Correct words will be shown in each player's list.\n" +
                "If you did not hear the word just press the 'Repeat Word' button.\n" +
                "Press 'Submit' or the Enter key to submit your answer.\n" +
                "When you are ready press 'ok' and the game will start immediately.\n" +
                "\nGood luck!"
            );
            NextRound(); // Ξεκινάει το παιχνίδι με την πρώτη λέξη 
            listBox1.Items.Clear(); 
            listBox2.Items.Clear();

        }

        private void Spelling_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Ενημερώνει το χρονόμετρο κάθε δευτερόλεπτο
            if (timeLeft > 0)
            {
                timeLeft--;
                label6.Text = $"{timeLeft} seconds";
            }
            else
            {
                timer1.Stop();

                // Αν δεν είναι steal, τότε ο παίζει ο άλλος παίκτης και η λέξη επαναλαμβάνεται αλλιώς κανένας δεν παίρνει πόντους
                if (!Steal)
                {
                    MessageBox.Show("Time's up! The other player gets a chance to spell the word.");
                    Steal = true;
                    currentPlayer = (currentPlayer == 1) ? 2 : 1; // Αλλάζει ο παίκτης
                    RepeatWord();
                }
                else
                {
                    MessageBox.Show("Time's up again! No one gets points.");
                    Steal = false;
                    currentPlayer = (Starter == 1) ? 2 : 1;
                    NextRound();
                }
            }
        }

        private void PickWord()
        {

            int index = rnd.Next(words.Count); // Επιλέγει τυχαία μια λέξη από τη λίστα
            currentWord = words[index].ToLower();
            words.RemoveAt(index); // Αφαιρεί τη λέξη από τη λίστα για να μην επαναληφθεί

            synthesizer.SpeakAsync(currentWord);
            textBox1.Clear();

            Starter = currentPlayer; // Ο παίκτης που ξεκινάει τη λέξη, δηλώνεται και ως currentPlayer

            label3.Text = $"Player {currentPlayer}'s turn";
            timeLeft = 20;
            label6.Text = $"{timeLeft} seconds";
            timer1.Start();
        }

        private void RepeatWord()
        {
            synthesizer.SpeakAsync(currentWord);
            textBox1.Clear();
            label3.Text = $"Player {currentPlayer}'s turn.";
            timeLeft = 15;
            label6.Text = $"{timeLeft} seconds";
            timer1.Start();
        }

        // Συνάρτηση που ελέγχει την απάντηση του χρήστη
        private void CheckAnswer()
        {
            string Input = textBox1.Text.ToLower().Trim();
            timer1.Stop();

            // Ελέγχει αν η απάντηση του χρήστη είναι σωστή
            if (Input == currentWord)
            {
                if (Steal) // Αν είναι steal, τότε ο παίκτης που απαντάει σωστά κλέβει τη λέξη
                {
                    if (currentPlayer == 1) 
                    {
                        Score1 += 15;
                        listBox1.Items.Add(currentWord);
                        label4.Text = $"Player 1's score: {Score1}";
                        MessageBox.Show("Correct! +15 points for Player 1");
                        currentPlayer = 2;
                    }
                    else
                    {
                        Score2 += 15;
                        listBox2.Items.Add(currentWord);
                        label5.Text = $"Player 2's score: {Score2}";
                        MessageBox.Show("Correct! +15 points for Player 2");
                        currentPlayer = 1;
                    }
                }
                else
                {
                    if (currentPlayer == 1)
                    {
                        Score1 += 10;
                        listBox1.Items.Add(currentWord);
                        label4.Text = $"Player 1's score: {Score1}";
                        MessageBox.Show("Correct! +10 points for Player 1");
                        currentPlayer = 2;
                    }
                    else
                    {
                        Score2 += 10;
                        listBox2.Items.Add(currentWord);
                        label5.Text = $"Player 2's score: {Score2}";
                        MessageBox.Show("Correct! +10 points for Player 2");
                        currentPlayer = 1;
                    }
                }

                Steal = false;
                NextRound();
            }
            else // Αν η απάντηση του χρήστη είναι λάθος και δεν είναι steal 
            {
                if (!Steal)
                {
                    MessageBox.Show("Wrong! The other player gets a chance to spell the word.");
                    Steal = true;
                    currentPlayer = (currentPlayer == 1) ? 2 : 1;
                    RepeatWord();
                }
                else // Αν είναι steal και η απάντηση είναι λάθος για δεύτερη φορά
                {
                    MessageBox.Show("Wrong again! No one gets points.");
                    Steal = false;
                    currentPlayer = (Starter == 1) ? 2 : 1;
                    NextRound();
                }
            }
        }

        private void NextRound() // Επιλογή επόμενης λέξης και έλεγχος σε τι γύρο είμαστε
        {
            roundsPlayed++;
            if (roundsPlayed > Rounds || words.Count == 0)
            {
                EndGame();
            }
            else
            {
                PickWord();
            }
        }

        private void EndGame() // τέλος παιχνιδιού
        {
            timer1.Stop();
            string winner;
            if(Score1 > Score2)
            {
                winner = "Player 1 wins!";
            }
            else if (Score2 > Score1)
            {
                winner = "Player 2 wins!";
            }
            else
            {
                winner = "It's a tie!";
            }
            MessageBox.Show($"Game over! {winner} \nFinal Scores:\nPlayer 1: {Score1}\nPlayer 2: {Score2}");
            this.Close(); // Κλείνει το παράθυρο του παιχνιδιού
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick(); // Σαν να πάτησες button2
                e.SuppressKeyPress = true; // Αποτρέπει τον ήχο του Enter
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            synthesizer.SpeakAsync(currentWord);
        }
    }
}

 

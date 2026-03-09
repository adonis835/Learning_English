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
    public partial class Crossword : Form
    {
        private Difficulty Mainform;
        int Sizecross = 20;
        char[,] Crossgrid;
        Dictionary<(int row, int col), int> clues = new Dictionary<(int, int), int>(); // λεξικό με θέση (row, col) και αριθμό στοιχείου
        int Nextclue = 1;


        List<PlacedWord> Placedwords = new List<PlacedWord>(); // Λίστα με τις τοποθετημένες λέξεις στο σταυρόλεξο

        public Crossword(Difficulty mainform)
        {
            InitializeComponent();
            Mainform = mainform;
            MessageBox.Show("Use the clues to solve the crossword puzzle. After each word press the 'Check' button to see if it was the right word.\n" +
                "Good luck!");

            InitGrid();
            CreateCrossword();
        }
        void InitGrid() // Αρχικοποίηση του DataGridView για το σταυρόλεξο
        {
            int Width = 32;
            int Height = 35;

            dataGridView1.RowCount = Sizecross;
            dataGridView1.ColumnCount = Sizecross;

            // Αρχικοποίηση πλάτους και ύψους των κελιών
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.Width = Width;

            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Height = Height;

            // Αλλαγή ιδιοτήτων του DataGridView
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Font = new Font("Consolas", 14);
            dataGridView1.RowHeadersVisible = false; // Απόκρυψη των αριθμών γραμμών
            dataGridView1.ColumnHeadersVisible = false; // Απόκρυψη των αριθμών στηλών
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            // Ορισμός μεγέθους του DataGridView, +1 pixel για το περίγραμμα
            dataGridView1.Width = Sizecross * Width + 1;
            dataGridView1.Height = Sizecross * Height + 1;

            
            Crossgrid = new char[Sizecross, Sizecross];
            // Αρχικοποίηση του πίνακα με χαρακτήρες '#' και χρώμα γκρίζο για όλα τα κελιά
            for (int i = 0; i < Sizecross; i++)
            {
                for (int j = 0; j < Sizecross; j++)
                {
                    Crossgrid[i, j] = '#';
                    dataGridView1[j, i].Style.BackColor = Color.LightGray;
                    dataGridView1[j, i].ReadOnly = true;
                }
            }
        }

        // Δημιουργία του σταυρόλεξου με τις λέξεις και τις θέσεις τους, κάθετα ή οριζόντια
        void CreateCrossword()
        {
            AddWordHorizontal("PRINCIPAL", 5, 3);
            AddWordVertical("KITCHEN", 4, 5);
            AddWordVertical("MOVIE", 2, 8);
            AddWordVertical("POOL", 2, 11);
            AddWordHorizontal("PLATE", 2, 11);
            AddWordVertical("TEACHER", 1, 15);
            AddWordHorizontal("FLOWERS", 7, 10);
            AddWordVertical("SLEEP", 7, 16);
            AddWordHorizontal("CLOSET", 9, 12);
            AddWordHorizontal("BACKPACK", 11, 12);
            AddWordHorizontal("GARDEN", 9, 1);
            AddWordVertical("GARAGE", 9, 1);
            AddWordHorizontal("EXERCISE", 14, 1);
            AddWordVertical("BREAK", 13, 4);
            AddWordVertical("SUN", 14, 7);


            // Τα κελιάπου έχουν τοποθετημένες λέξεις γίνονται κενά για τον χρήστη και αλλάζουν χρώμα
            for (int i = 0; i < Sizecross; i++)
                for (int j = 0; j < Sizecross; j++)
                {
                    if (Crossgrid[i, j] != '#')
                    {
                        dataGridView1[j, i].Value = ""; // Κενό για τον χρήστη
                        dataGridView1[j, i].ReadOnly = false;
                        dataGridView1[j, i].Style.BackColor = Color.White;
                    }
                }

            // Προσθήκη των στοιχείων στο DataGridView με τις θέσεις και τους αριθμούς των στοιχείων
            foreach (var clue in clues)
            {
                int row = clue.Key.row;
                int col = clue.Key.col;
                int number = clue.Value;

                dataGridView1[col, row].Tag = number;
            }

        }
        // Προσθήκη λέξης οριζόντια 
        void AddWordHorizontal(string word, int row, int col)
        {
            for (int i = 0; i < word.Length; i++)
                Crossgrid[row, col + i] = word[i];

            Placedwords.Add(new PlacedWord { Word = word.ToUpper(), Row = row, Col = col, Horizontal = true });

            clues[(row, col)] = Nextclue++;
        }

        // Προσθήκη λέξης κάθετα
        void AddWordVertical(string word, int row, int col)
        {
            for (int i = 0; i < word.Length; i++)
                Crossgrid[row + i, col] = word[i];

            Placedwords.Add(new PlacedWord { Word = word.ToUpper(), Row = row, Col = col, Horizontal = false });

            clues[(row, col)] = Nextclue++;
        }


        // Επεξεργασία του κελιού για να επιτρέπεται μόνο ένα γράμμα
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBox_KeyPress;
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        // Διαχείριση του KeyPress για να επιτρέπεται μόνο ένα γράμμα σε κάθε κελί
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                return;
            }

            // Εάν το κελί έχει ήδη χαρακτήρα, τον αντικαθιστούμε με το νέο γράμμα
            if (tb.Text.Length >= 1 && !char.IsControl(e.KeyChar))
            {
                tb.Text = ""; // Καθαρίζουμε το κελί για να επιτρέψουμε μόνο ένα γράμμα
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        // Μετά την επεξεργασία του κελιού, μετατρέπουμε το γράμμα σε κεφαλαίο και το περιορίζουμε σε ένα γράμμα
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            if (cell.Value != null)
            {
                string val = cell.Value.ToString().Trim();
                if (val.Length > 1)
                    val = val.Substring(0, 1);
                cell.Value = val.ToUpper();
            }
        }
        class PlacedWord // Κλάση για την αποθήκευση των τοποθετημένων λέξεων στο σταυρόλεξο
        {
            public string Word { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }
            public bool Horizontal { get; set; }
        }

        private void check_Click(object sender, EventArgs e) // Έλεγχος των απαντήσεων του χρήστη στο σταυρόλεξο
        {
            int Correctwords = 0;
            foreach (var placed in Placedwords)
            {
                bool correct = true;

                for (int i = 0; i < placed.Word.Length; i++)
                {
                    int row = placed.Horizontal ? placed.Row : placed.Row + i;
                    int col = placed.Horizontal ? placed.Col + i : placed.Col;

                    var cell = dataGridView1[col, row];
                    // Διαβάζουμε την τιμή του κελιού και την μετατρέπουμε σε κεφαλαίο και αν είναι NULL, βάζουμε κενό string
                    string userValue = cell.Value?.ToString().ToUpper() ?? "";

                    // Ελέγχουμε αν το γράμμα του χρήστη ταιριάζει με το γράμμα της λέξης
                    if (userValue != placed.Word[i].ToString())
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    Correctwords++;
                    // Αν η λέξη είναι σωστή, αλλάζουμε το χρώμα των κελιών σε πράσινο και τα κλειδώνουμε
                    for (int i = 0; i < placed.Word.Length; i++)
                    {
                        int row = placed.Horizontal ? placed.Row : placed.Row + i;
                        int col = placed.Horizontal ? placed.Col + i : placed.Col;

                        dataGridView1[col, row].Style.BackColor = Color.LightGreen;
                        dataGridView1[col, row].ReadOnly = true; 
                    }
                }
                // Αν βρούμε όλες τις λέξεις, εμφανίζουμε μήνυμα επιτυχίας
                if (Correctwords == Placedwords.Count)
                {
                    MessageBox.Show("Congratulations! You completed the crossword puzzle!");
                    this.Close();
                    Mainform.Show();
                }
            }
        }

        // Σχεδίασε στα κελιά του DataGridView τα νούμερα των λέξεων
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            // Ελέγχουμε εαν η γραμμή και η στήλη είναι έγκυρες
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;


            // Ζωγραφίζουμε το κελί
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            var cell = dataGridView1[e.ColumnIndex, e.RowIndex];

            // Ελέγχουμε αν το κελί έχει Tag με αριθμό στοιχείου
            if (cell.Tag is int clueNumber)
            {
                using (Font font = new Font("Segoe UI", 7, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(clueNumber.ToString(), font, brush,
                        e.CellBounds.Left + 2, e.CellBounds.Top + 2); // Σχεδιάζουμε τον αριθμό στοιχείου στο κελί
                }
            }

            e.Handled = true;
        }

        private void Crossword_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mainform.Show();
        }
    }
}


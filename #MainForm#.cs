using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hangman
{
    public partial class MainForm : Form
    {
        private readonly string[] phrases = new string[] { "Okay, now guess for real.", "Well, you had a 1 in 26 chance.", "Hmm, not quite.", "Wake me up when you're done.", "You know what's not right..." };
        private readonly List<string> wordList = new List<string> { "have a great day", "hello world", "happy new year", "good luck", "have fun", "good Hangman", "progress, not perfection", "let's just dance", "let it go", "nobody is perfect", "seize the day", "embrace the weird", "remember to live", "you are enough" };
        private readonly List<string> hangmanImages = new List<string> { "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~2.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~3.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~4.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~5.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~6.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~7.png", "G:\\My Drive\\HangMan\\tumblr_inline_pg0k73U7BJ1rh6ctt_500~8.png" /*...*/ };
        private readonly Random rand = new Random();
        private string randomWord;
        private char[] guessedLetters;
        private int attempts;
        private readonly PictureBox picBoxHangman; // Declare the PictureBox

        public MainForm()
        {
            InitializeComponent();
            StartHangman();

            Hangman.OnWrongGuess += Hangman_OnWrongGuess;

            void Hangman_OnWrongGuess(object sender, EventArgs e)
            {
                // The Hangman's WrongGuessCount starts at 0 and increments with each wrong guess
                int index = Hangman.WrongGuessCount;

                // Check if the index is within the bounds of the list
                if (index < hangmanImages.Count)
                {
                    pictureBox1.Image = Image.FromFile(hangmanImages[index]);
                }
                else
                {
                    // If the player has made more wrong guesses than there are images, you might want to display a default image
                    pictureBox1.Image = Image.FromFile ("DefaultImage");
                }
            }
            // Initialize the PictureBox
            picBoxHangman = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(100, 100)
            };
            Controls.Add(picBoxHangman);
        }

        private void StartHangman()
        {
            randomWord = wordList[rand.Next(wordList.Count)];
            guessedLetters = new string('_', randomWord.Length).ToCharArray();

            for (int i = 0; i < randomWord.Length; i++)
            {
                if (randomWord[i] == ' ')
                {
                    guessedLetters[i] = ' ';
                }
            }

            attempts = randomWord.Count(c => c != ' ') + 1;

            lblPhrase.Text = "Phrase: " + new string(guessedLetters);

            // Load an image into the PictureBox
            if (attempts < hangmanImages.Count)
            {
                picBoxHangman.Image = new Bitmap(hangmanImages[attempts]);
            }
        }

        private void BtnGuess_Click(object sender, EventArgs e)
        {
            string guess = txtGuess.Text.ToLower();

            if (guess.Length != 1)
            {
                _ = MessageBox.Show("Someone's Indecisive. One at a time please.");
                return;
            }

            if (randomWord.Contains(guess))
            {
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i] == guess[0])
                    {
                        guessedLetters[i] = guess[0];
                    }
                }
            }
            else
            {
                attempts--;
                // Update the PictureBox image here
                if (attempts >= 0 && attempts < hangmanImages.Count)
                {
                    picBoxHangman.Image = new Bitmap(hangmanImages[attempts]);
                }
            }

            lblPhrase.Text = "Phrase: " + new string(guessedLetters);

            if (!new string(guessedLetters).Contains('_'))
            {
                _ = MessageBox.Show("You win! I should've given you a hard one.: " + randomWord);
                StartHangman();
            }
            else if (attempts == 0)
            {
                _ = MessageBox.Show("It's cool, I failed that one too. The phrase was: " + randomWord);
                StartHangman();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StartHangman();
        }

        private void PictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

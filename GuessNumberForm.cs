using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameCollection
{
    public class GuessNumberForm : Form
    {
        private readonly Random random = new Random();
        private int targetNumber;
        private int attempts;
        private string difficulty = "Medium";
        
        // کنترل‌های فرم
        private readonly TextBox txtGuess = new TextBox();
        private readonly Button btnSubmit = new Button();
        private readonly Label lblResult = new Label();
        private readonly Label lblAttempts = new Label();
        private readonly Button btnEasy = new Button();
        private readonly Button btnMedium = new Button();
        private readonly Button btnHard = new Button();

        public GuessNumberForm()
        {
            InitializeForm();
            StartNewGame();
        }

        private void InitializeForm()
        {
            // تنظیمات اصلی فرم
            this.Text = "Guess the Number Game";
            this.ClientSize = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // برچسب راهنما
            var lblInstruction = new Label
            {
                Text = "Guess a number between:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            // فیلد ورودی حدس
            txtGuess.Location = new Point(20, 50);
            txtGuess.Size = new Size(100, 30);
            txtGuess.KeyPress += TxtGuess_KeyPress;

            // دکمه تایید
            btnSubmit.Text = "Submit";
            btnSubmit.Location = new Point(130, 50);
            btnSubmit.Size = new Size(80, 30);
            btnSubmit.Click += BtnSubmit_Click;

            // برچسب نتیجه
            lblResult.Location = new Point(20, 90);
            lblResult.Size = new Size(350, 50);
            lblResult.AutoSize = false;

            // برچسب تعداد حدس‌ها
            lblAttempts.Location = new Point(20, 150);
            lblAttempts.AutoSize = true;

            // دکمه‌های سطح دشواری
            btnEasy.Text = "Easy (1-50)";
            btnEasy.Location = new Point(20, 190);
            btnEasy.Size = new Size(100, 30);
            btnEasy.Click += (s, e) => SetDifficulty("Easy");

            btnMedium.Text = "Medium (1-100)";
            btnMedium.Location = new Point(140, 190);
            btnMedium.Size = new Size(100, 30);
            btnMedium.Click += (s, e) => SetDifficulty("Medium");

            btnHard.Text = "Hard (1-200)";
            btnHard.Location = new Point(260, 190);
            btnHard.Size = new Size(100, 30);
            btnHard.Click += (s, e) => SetDifficulty("Hard");

            // دکمه شروع مجدد
            var btnRestart = new Button
            {
                Text = "New Game",
                Location = new Point(20, 230),
                Size = new Size(100, 30)
            };
            btnRestart.Click += (s, e) => StartNewGame();

            // اضافه کردن کنترل‌ها به فرم
            this.Controls.AddRange(new Control[]
            {
                lblInstruction,
                txtGuess,
                btnSubmit,
                lblResult,
                lblAttempts,
                btnEasy,
                btnMedium,
                btnHard,
                btnRestart
            });
        }

        private void StartNewGame()
        {
            attempts = 0;
            UpdateRange();
            txtGuess.Text = "";
            lblResult.Text = "";
            lblAttempts.Text = $"Attempts: {attempts}";
            txtGuess.Focus();
        }

        private void SetDifficulty(string newDifficulty)
        {
            difficulty = newDifficulty;
            StartNewGame();
        }

        private void UpdateRange()
        {
            switch (difficulty)
            {
                case "Easy":
                    targetNumber = random.Next(1, 51);
                    lblResult.Text = "Guess a number between 1 and 50";
                    break;
                case "Medium":
                    targetNumber = random.Next(1, 101);
                    lblResult.Text = "Guess a number between 1 and 100";
                    break;
                case "Hard":
                    targetNumber = random.Next(1, 201);
                    lblResult.Text = "Guess a number between 1 and 200";
                    break;
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuess.Text, out int guess))
            {
                attempts++;
                lblAttempts.Text = $"Attempts: {attempts}";

                if (guess < targetNumber)
                {
                    lblResult.Text = "Too low! Try a higher number.";
                }
                else if (guess > targetNumber)
                {
                    lblResult.Text = "Too high! Try a lower number.";
                }
                else
                {
                    lblResult.Text = $"Congratulations! You guessed it in {attempts} attempts.";
                    MessageBox.Show($"You won in {attempts} attempts!", "Victory", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StartNewGame();
                }
            }
            else
            {
                lblResult.Text = "Please enter a valid number!";
            }

            txtGuess.Text = "";
            txtGuess.Focus();
        }

        private void TxtGuess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSubmit_Click(sender, e);
                e.Handled = true;
            }
            
            // فقط اجازه ورود اعداد
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameCollection
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeUI();
            SetupEvents();
        }

        private void InitializeUI()
        {
            // تنظیمات اصلی فرم
            this.Text = "Game Collection";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Icon = Properties.Resources.GameIcon; // اگر آیکون دارید

            // عنوان برنامه
            var lblTitle = new Label
            {
                Text = "انتخاب بازی",
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(150, 20)
            };

            // دکمه بازی مار
            var btnSnake = new Button
            {
                Text = "بازی مار",
                Size = new Size(250, 60),
                Location = new Point(125, 80),
                Font = new Font("Tahoma", 12),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // دکمه بازی ماشین‌سواری
            var btnCar = new Button
            {
                Text = "مسابقه ماشین",
                Size = new Size(250, 60),
                Location = new Point(125, 160),
                Font = new Font("Tahoma", 12),
                BackColor = Color.LightSkyBlue,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // دکمه بازی حدس عدد
            var btnGuess = new Button
            {
                Text = "حدس عدد",
                Size = new Size(250, 60),
                Location = new Point(125, 240),
                Font = new Font("Tahoma", 12),
                BackColor = Color.LightSalmon,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // اضافه کردن کنترل‌ها به فرم
            this.Controls.Add(lblTitle);
            this.Controls.Add(btnSnake);
            this.Controls.Add(btnCar);
            this.Controls.Add(btnGuess);
        }

        private void SetupEvents()
        {
            // پیدا کردن دکمه‌ها
            var btnSnake = this.Controls.Find("btnSnake", true)[0] as Button;
            var btnCar = this.Controls.Find("btnCar", true)[0] as Button;
            var btnGuess = this.Controls.Find("btnGuess", true)[0] as Button;

            // رویدادهای کلیک
            btnSnake.Click += (s, e) => OpenGameForm(new SnakeGameForm());
            btnCar.Click += (s, e) => OpenGameForm(new CarRacingForm());
            btnGuess.Click += (s, e) => OpenGameForm(new GuessNumberForm());
        }

        private void OpenGameForm(Form gameForm)
        {
            this.Hide();
            gameForm.FormClosed += (s, args) => 
            {
                this.Show();
                this.Activate();
            };
            gameForm.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // نمایش پیام تأیید هنگام بستن برنامه
            if (MessageBox.Show("آیا می‌خواهید برنامه را ببندید؟", "خروج",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

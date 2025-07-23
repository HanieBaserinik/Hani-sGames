using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameCollection
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            // تنظیمات فرم
            this.Text = "Game Collection";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // ایجاد دکمه‌ها
            var btnSnake = new Button {
                Text = "Snake Game",
                Size = new Size(200, 50),
                Location = new Point(100, 30)
            };
            
            var btnCar = new Button {
                Text = "Car Racing",
                Size = new Size(200, 50),
                Location = new Point(100, 100)
            };
            
            var btnGuess = new Button {
                Text = "Guess Number",
                Size = new Size(200, 50),
                Location = new Point(100, 170)
            };
            
            // رویدادهای کلیک
            btnSnake.Click += (s, e) => {
                var snakeForm = new SnakeGameForm();
                snakeForm.Show();
                this.Hide();
                snakeForm.FormClosed += (ss, ee) => this.Show();
            };
            
            btnCar.Click += (s, e) => {
                var carForm = new CarRacingForm();
                carForm.Show();
                this.Hide();
                carForm.FormClosed += (ss, ee) => this.Show();
            };
            
            btnGuess.Click += (s, e) => {
                var guessForm = new GuessNumberForm();
                guessForm.Show();
                this.Hide();
                guessForm.FormClosed += (ss, ee) => this.Show();
            };
            
            // اضافه کردن دکمه‌ها به فرم
            this.Controls.Add(btnSnake);
            this.Controls.Add(btnCar);
            this.Controls.Add(btnGuess);
        }
    }
}

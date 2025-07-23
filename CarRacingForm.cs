using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameCollection
{
    public class CarRacingForm : Form
    {
        // اجزای بازی
        private PictureBox playerCar;
        private List<PictureBox> enemies = new List<PictureBox>();
        private List<PictureBox> coins = new List<PictureBox>();
        private Timer gameTimer = new Timer();
        private Random rand = new Random();
        private int roadSpeed = 5;
        private int score = 0;
        private Label lblScore = new Label();

        public CarRacingForm()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            // تنظیمات فرم
            this.Text = "Car Racing Game";
            this.ClientSize = new Size(300, 500);
            this.DoubleBuffered = true;
            this.BackColor = Color.Gray;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ایجاد ماشین بازیکن
            playerCar = new PictureBox
            {
                Size = new Size(50, 100),
                BackColor = Color.Blue,
                Location = new Point(125, 350)
            };
            this.Controls.Add(playerCar);

            // ایجاد برچسب امتیاز
            lblScore.Text = "Score: 0";
            lblScore.ForeColor = Color.White;
            lblScore.Font = new Font("Arial", 12);
            lblScore.AutoSize = true;
            lblScore.Location = new Point(10, 10);
            this.Controls.Add(lblScore);

            // تنظیم تایمر بازی
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            // رویدادهای صفحه کلید
            this.KeyDown += CarRacingForm_KeyDown;
            this.FormClosed += (s, e) => this.Close();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            MoveRoad();
            SpawnEnemies();
            MoveEnemies();
            SpawnCoins();
            MoveCoins();
            CheckCollision();
        }

        private void MoveRoad()
        {
            // منطق حرکت جاده (اختیاری)
        }

        private void SpawnEnemies()
        {
            if (rand.Next(0, 100) < 5)
            {
                var enemy = new PictureBox
                {
                    Size = new Size(50, 100),
                    BackColor = Color.Red,
                    Location = new Point(rand.Next(50, 200), -100)
                };
                enemies.Add(enemy);
                this.Controls.Add(enemy);
            }
        }

        private void MoveEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Top += roadSpeed;
                
                if (enemies[i].Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(enemies[i]);
                    enemies.RemoveAt(i);
                }
            }
        }

        private void SpawnCoins()
        {
            if (rand.Next(0, 100) < 3)
            {
                var coin = new PictureBox
                {
                    Size = new Size(30, 30),
                    BackColor = Color.Gold,
                    Location = new Point(rand.Next(50, 220), -30)
                };
                coins.Add(coin);
                this.Controls.Add(coin);
            }
        }

        private void MoveCoins()
        {
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                coins[i].Top += roadSpeed;
                
                if (coins[i].Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(coins[i]);
                    coins.RemoveAt(i);
                }
            }
        }

        private void CheckCollision()
        {
            // برخورد با دشمنان
            foreach (var enemy in enemies)
            {
                if (playerCar.Bounds.IntersectsWith(enemy.Bounds))
                {
                    GameOver();
                    return;
                }
            }

            // جمع آوری سکه‌ها
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (playerCar.Bounds.IntersectsWith(coins[i].Bounds))
                {
                    score += 10;
                    lblScore.Text = $"Score: {score}";
                    this.Controls.Remove(coins[i]);
                    coins.RemoveAt(i);
                }
            }
        }

        private void CarRacingForm_KeyDown(object sender, KeyEventArgs e)
        {
            // کنترل ماشین بازیکن
            int moveSpeed = 10;
            
            if (e.KeyCode == Keys.Left && playerCar.Left > 50)
            {
                playerCar.Left -= moveSpeed;
            }
            else if (e.KeyCode == Keys.Right && playerCar.Right < this.ClientSize.Width - 50)
            {
                playerCar.Left += moveSpeed;
            }
            else if (e.KeyCode == Keys.Up && playerCar.Top > 0)
            {
                playerCar.Top -= moveSpeed;
            }
            else if (e.KeyCode == Keys.Down && playerCar.Bottom < this.ClientSize.Height)
            {
                playerCar.Top += moveSpeed;
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game Over! Your score: {score}", "Game Over");
            this.Close();
        }
    }
}

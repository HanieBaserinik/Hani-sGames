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
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameCollection
{
    public class CarRacingForm : Form
    {
        // اجزای اصلی بازی
        private PictureBox playerCar;
        private List<PictureBox> enemies = new List<PictureBox>();
        private List<PictureBox> coins = new List<PictureBox>();
        private Timer gameTimer = new Timer();
        private Random rand = new Random();
        
        // متغیرهای بازی
        private int roadSpeed = 5;
        private int playerSpeed = 5;
        private int score = 0;
        private int coinCount = 0;
        private bool isGameOver = false;
        
        // کنترل‌های UI
        private Label lblScore = new Label();
        private Label lblCoins = new Label();
        private Label lblGameOver = new Label();

        public CarRacingForm()
        {
            InitializeGame();
            StartGame();
        }

        private void InitializeGame()
        {
            // تنظیمات اصلی فرم
            this.Text = "Car Racing Game";
            this.ClientSize = new Size(300, 500);
            this.DoubleBuffered = true;
            this.BackColor = Color.Gray;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;

            // ایجاد ماشین بازیکن
            playerCar = new PictureBox
            {
                Size = new Size(50, 100),
                BackColor = Color.Blue,
                Location = new Point(125, 350)
            };
            this.Controls.Add(playerCar);

            // ایجاد برچسب امتیاز
            lblScore.Text = $"Score: {score}";
            lblScore.ForeColor = Color.White;
            lblScore.Font = new Font("Arial", 12, FontStyle.Bold);
            lblScore.AutoSize = true;
            lblScore.Location = new Point(10, 10);
            this.Controls.Add(lblScore);

            // ایجاد برچسب سکه‌ها
            lblCoins.Text = $"Coins: {coinCount}";
            lblCoins.ForeColor = Color.White;
            lblCoins.Font = new Font("Arial", 12, FontStyle.Bold);
            lblCoins.AutoSize = true;
            lblCoins.Location = new Point(10, 40);
            this.Controls.Add(lblCoins);

            // ایجاد برچسب Game Over
            lblGameOver.Text = "GAME OVER\nPress Enter to Restart";
            lblGameOver.ForeColor = Color.Red;
            lblGameOver.Font = new Font("Arial", 14, FontStyle.Bold);
            lblGameOver.AutoSize = true;
            lblGameOver.Location = new Point(70, 200);
            lblGameOver.Visible = false;
            this.Controls.Add(lblGameOver);

            // تنظیم تایمر بازی
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;

            // رویدادهای صفحه کلید
            this.KeyDown += CarRacingForm_KeyDown;
            this.KeyUp += CarRacingForm_KeyUp;
            this.FormClosed += (s, e) => this.Close();
        }

        private void StartGame()
        {
            // Reset game state
            score = 0;
            coinCount = 0;
            isGameOver = false;
            lblScore.Text = $"Score: {score}";
            lblCoins.Text = $"Coins: {coinCount}";
            lblGameOver.Visible = false;

            // Clear existing enemies and coins
            foreach (var enemy in enemies)
                this.Controls.Remove(enemy);
            foreach (var coin in coins)
                this.Controls.Remove(coin);
            
            enemies.Clear();
            coins.Clear();

            // Reset player position
            playerCar.Location = new Point(125, 350);

            // Start game timer
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isGameOver) return;

            MoveRoad();
            SpawnEnemies();
            MoveEnemies();
            SpawnCoins();
            MoveCoins();
            CheckCollision();
        }

        private void MoveRoad()
        {
            // اینجا می‌توانید منطق حرکت جاده را پیاده‌سازی کنید
            // برای مثال حرکت خطوط جاده یا پس‌زمینه
        }

        private void SpawnEnemies()
        {
            if (rand.Next(0, 100) < 3) // 3% chance each tick
            {
                var enemy = new PictureBox
                {
                    Size = new Size(50, 100),
                    BackColor = Color.Red,
                    Location = new Point(rand.Next(50, 200), -100)
                };
                enemies.Add(enemy);
                this.Controls.Add(enemy);
                enemy.BringToFront();
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
                    score += 5;
                    lblScore.Text = $"Score: {score}";
                }
            }
        }

        private void SpawnCoins()
        {
            if (rand.Next(0, 100) < 2) // 2% chance each tick
            {
                var coin = new PictureBox
                {
                    Size = new Size(30, 30),
                    BackColor = Color.Gold,
                    Location = new Point(rand.Next(50, 220), -30)
                };
                coins.Add(coin);
                this.Controls.Add(coin);
                coin.BringToFront();
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
                    coinCount++;
                    score += 10;
                    lblCoins.Text = $"Coins: {coinCount}";
                    lblScore.Text = $"Score: {score}";
                    this.Controls.Remove(coins[i]);
                    coins.RemoveAt(i);
                }
            }
        }

        private void CarRacingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver && e.KeyCode == Keys.Enter)
            {
                StartGame();
                return;
            }

            if (isGameOver) return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (playerCar.Left > 20)
                        playerCar.Left -= playerSpeed;
                    break;
                case Keys.Right:
                    if (playerCar.Right < this.ClientSize.Width - 20)
                        playerCar.Left += playerSpeed;
                    break;
                case Keys.Up:
                    if (playerCar.Top > 0)
                        playerCar.Top -= playerSpeed;
                    break;
                case Keys.Down:
                    if (playerCar.Bottom < this.ClientSize.Height)
                        playerCar.Top += playerSpeed;
                    break;
            }
        }

        private void CarRacingForm_KeyUp(object sender, KeyEventArgs e)
        {
            // می‌توانید برای کنترل نرم‌تر از این بخش استفاده کنید
        }

        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            lblGameOver.Visible = true;
            playerCar.BackColor = Color.DarkGray;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // رسم خطوط جاده
            Graphics g = e.Graphics;
            Pen roadLinePen = new Pen(Color.Yellow, 3);
            
            for (int y = 0; y < this.ClientSize.Height; y += 60)
            {
                g.DrawLine(roadLinePen, 
                    this.ClientSize.Width / 2, y,
                    this.ClientSize.Width / 2, y + 30);
            }
        }
    }
}

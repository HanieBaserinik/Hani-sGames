using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameCollection
{
    public class SnakeGameForm : Form
    {
        private List<Point> snake = new List<Point>();
        private Point food;
        private int direction = 1; // 0=Up, 1=Right, 2=Down, 3=Left
        private int score = 0;
        private Timer gameTimer = new Timer();
        private Random rand = new Random();
        private Label lblScore = new Label();
        private bool isGameOver = false;
        private const int TileSize = 20;
        private const int GameAreaWidth = 400;
        private const int GameAreaHeight = 400;

        public SnakeGameForm()
        {
            InitializeGame();
            StartNewGame();
        }

        private void InitializeGame()
        {
            // تنظیمات اصلی فرم
            this.Text = "Snake Game";
            this.ClientSize = new Size(GameAreaWidth, GameAreaHeight + 40);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;

            // تنظیمات Label امتیاز
            lblScore.Text = $"Score: {score}";
            lblScore.Location = new Point(10, GameAreaHeight + 5);
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(lblScore);

            // تنظیمات تایمر بازی
            gameTimer.Interval = 100;
            gameTimer.Tick += UpdateGame;
            
            this.KeyDown += ChangeDirection;
        }

        private void StartNewGame()
        {
            snake.Clear();
            score = 0;
            isGameOver = false;
            direction = 1;
            
            // ایجاد مار اولیه (3 بخش)
            snake.Add(new Point(100, 100));
            snake.Add(new Point(90, 100));
            snake.Add(new Point(80, 100));
            
            GenerateFood();
            gameTimer.Start();
            lblScore.Text = $"Score: {score}";
        }

        private void GenerateFood()
        {
            int maxX = (GameAreaWidth / TileSize) - 1;
            int maxY = (GameAreaHeight / TileSize) - 1;
            
            bool foodOnSnake;
            do
            {
                foodOnSnake = false;
                food = new Point(
                    rand.Next(0, maxX) * TileSize,
                    rand.Next(0, maxY) * TileSize);
                
                foreach (var segment in snake)
                {
                    if (segment == food)
                    {
                        foodOnSnake = true;
                        break;
                    }
                }
            } while (foodOnSnake);
        }

        private void UpdateGame(object sender, EventArgs e)
        {
            if (isGameOver) return;

            // حرکت مار
            Point newHead = snake[0];
            
            switch (direction)
            {
                case 0: newHead.Y -= TileSize; break; // بالا
                case 1: newHead.X += TileSize; break; // راست
                case 2: newHead.Y += TileSize; break; // پایین
                case 3: newHead.X -= TileSize; break; // چپ
            }

            // بررسی برخورد با دیوارها
            if (newHead.X < 0 || newHead.X >= GameAreaWidth || 
                newHead.Y < 0 || newHead.Y >= GameAreaHeight)
            {
                GameOver();
                return;
            }

            // بررسی برخورد با بدن
            for (int i = 1; i < snake.Count; i++)
            {
                if (newHead == snake[i])
                {
                    GameOver();
                    return;
                }
            }

            // اضافه کردن سر جدید
            snake.Insert(0, newHead);

            // بررسی خوردن غذا
            if (newHead == food)
            {
                score += 10;
                lblScore.Text = $"Score: {score}";
                GenerateFood();
            }
            else
            {
                // اگر غذا نخوردیم، دم را حذف کنیم
                snake.RemoveAt(snake.Count - 1);
            }

            this.Invalidate(); // باعث فراخوانی OnPaint می‌شود
        }

        private void ChangeDirection(object sender, KeyEventArgs e)
        {
            if (isGameOver) return;

            switch (e.KeyCode)
            {
                case Keys.Up when direction != 2:
                    direction = 0;
                    break;
                case Keys.Right when direction != 3:
                    direction = 1;
                    break;
                case Keys.Down when direction != 0:
                    direction = 2;
                    break;
                case Keys.Left when direction != 1:
                    direction = 3;
                    break;
                case Keys.Enter when isGameOver:
                    StartNewGame();
                    break;
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            MessageBox.Show($"Game Over! Your score: {score}\nPress Enter to play again", "Game Over",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // رسم مار
            for (int i = 0; i < snake.Count; i++)
            {
                Brush brush = (i == 0) ? Brushes.DarkGreen : Brushes.Green;
                g.FillRectangle(brush, 
                    new Rectangle(snake[i].X, snake[i].Y, TileSize, TileSize));
                g.DrawRectangle(Pens.Black, 
                    new Rectangle(snake[i].X, snake[i].Y, TileSize, TileSize));
            }

            // رسم غذا
            g.FillEllipse(Brushes.Red, 
                new Rectangle(food.X, food.Y, TileSize, TileSize));
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            gameTimer.Stop();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameCollection
{
    public class SnakeGameForm : Form
    {
        // کدهای کامل بازی مار را اینجا قرار دهید
        // (می‌توانید از فایل‌های اصلی پروژه Snake کپی کنید)
        // این یک ساختار پایه است:
        
        private List<Point> snake = new List<Point>();
        private Point food;
        private int direction = 1; // 0=Up, 1=Right, 2=Down, 3=Left
        private int score = 0;
        private Timer gameTimer = new Timer();
        
        public SnakeGameForm()
        {
            this.Text = "Snake Game";
            this.ClientSize = new Size(400, 400);
            this.DoubleBuffered = true;
            
            // مقداردهی اولیه مار
            snake.Add(new Point(100, 100));
            snake.Add(new Point(90, 100));
            snake.Add(new Point(80, 100));
            
            GenerateFood();
            
            gameTimer.Interval = 100;
            gameTimer.Tick += UpdateGame;
            gameTimer.Start();
            
            this.KeyDown += ChangeDirection;
        }
        
        private void GenerateFood() { /* کد تولید غذا */ }
        private void UpdateGame(object sender, EventArgs e) { /* کد به‌روزرسانی بازی */ }
        private void ChangeDirection(object sender, KeyEventArgs e) { /* کد تغییر جهت */ }
        private void GameOver() { /* کد پایان بازی */ }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // کد رسم مار و غذا
            base.OnPaint(e);
        }
    }
}

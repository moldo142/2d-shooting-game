using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShootingGame
{
    public partial class GameForm : Form
    {
        private GameEngine gameEngine;
        private bool[] keysPressed;
        private Timer gameTimer;
        private Point lastMousePos;

        public GameForm()
        {
            InitializeComponent();
            
            this.Text = "2D Shooting Game";
            this.Width = 1024;
            this.Height = 768;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.KeyPreview = true;

            gameEngine = new GameEngine(this.ClientSize.Width, this.ClientSize.Height);
            keysPressed = new bool[256];

            gameTimer = new Timer();
            gameTimer.Interval = 16; // ~60 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += GameForm_KeyDown;
            this.KeyUp += GameForm_KeyUp;
            this.MouseDown += GameForm_MouseDown;
            this.MouseMove += GameForm_MouseMove;
            this.Paint += GameForm_Paint;

            this.Resize += (s, e) => gameEngine = new GameEngine(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            keysPressed[e.KeyCode.GetHashCode()] = true;

            // Restart on R key
            if (e.KeyCode == Keys.R && gameEngine.IsGameOver)
            {
                gameEngine.Restart();
            }

            e.Handled = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            keysPressed[e.KeyCode.GetHashCode()] = false;
            e.Handled = true;
        }

        private void GameForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !gameEngine.IsGameOver)
            {
                gameEngine.Shoot(e.Location);
            }
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            lastMousePos = e.Location;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Update game
            gameEngine.HandleInput(keysPressed, lastMousePos);
            gameEngine.Update();

            // Redraw
            this.Invalidate();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw game objects
            gameEngine.Draw(e.Graphics);

            // Draw UI
            DrawUI(e.Graphics);
        }

        private void DrawUI(Graphics g)
        {
            string scoreText = $"Score: {gameEngine.Score}";
            string livesText = $"Lives: {gameEngine.Lives}";
            string enemiesText = $"Enemies: {gameEngine.EnemyCount}";

            Font font = new Font("Arial", 16, FontStyle.Bold);
            Brush textBrush = Brushes.White;

            g.DrawString(scoreText, font, textBrush, 10, 10);
            g.DrawString(livesText, font, textBrush, 10, 40);
            g.DrawString(enemiesText, font, textBrush, 10, 70);

            if (gameEngine.IsGameOver)
            {
                Font gameOverFont = new Font("Arial", 48, FontStyle.Bold);
                Font restartFont = new Font("Arial", 24, FontStyle.Regular);
                Brush gameOverBrush = Brushes.Red;

                string gameOverText = "GAME OVER";
                string finalScoreText = $"Final Score: {gameEngine.Score}";
                string restartText = "Press R to Restart";

                SizeF gameOverSize = g.MeasureString(gameOverText, gameOverFont);
                SizeF scoreSize = g.MeasureString(finalScoreText, restartFont);

                float centerX = (this.ClientSize.Width - gameOverSize.Width) / 2;
                float centerY = (this.ClientSize.Height - gameOverSize.Height) / 2;

                g.DrawString(gameOverText, gameOverFont, gameOverBrush, centerX, centerY);
                g.DrawString(finalScoreText, restartFont, textBrush, centerX + gameOverSize.Width / 4, centerY + 70);
                g.DrawString(restartText, restartFont, textBrush, centerX + gameOverSize.Width / 4, centerY + 120);

                gameOverFont.Dispose();
                restartFont.Dispose();
            }

            font.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer?.Stop();
            gameTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}

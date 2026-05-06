using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ShootingGame
{
    public class GameEngine
    {
        private Player player;
        private List<Bullet> bullets;
        private List<Enemy> enemies;
        private Random random;
        
        private int width;
        private int height;
        private int score;
        private int lives;
        private int waveCount;
        private int enemySpawnCounter;
        private bool isGameOver;

        public int Score => score;
        public int Lives => lives;
        public int EnemyCount => enemies.Count;
        public bool IsGameOver => isGameOver;

        public GameEngine(int width, int height)
        {
            this.width = width;
            this.height = height;
            random = new Random();
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            
            player = new Player(width / 2, height / 2, 15);
            score = 0;
            lives = 3;
            waveCount = 1;
            enemySpawnCounter = 0;
            isGameOver = false;
        }

        public void HandleInput(bool[] keysPressed, Point mousePos)
        {
            if (isGameOver) return;

            // WASD movement
            float moveX = 0, moveY = 0;
            if (keysPressed['W'] || keysPressed[38]) moveY -= 1; // W or Up
            if (keysPressed['S'] || keysPressed[40]) moveY += 1; // S or Down
            if (keysPressed['A'] || keysPressed[37]) moveX -= 1; // A or Left
            if (keysPressed['D'] || keysPressed[39]) moveX += 1; // D or Right

            player.Move(moveX, moveY, width, height);
            player.UpdateAim(mousePos);
        }

        public void Shoot(Point targetPos)
        {
            Bullet bullet = player.Shoot(targetPos);
            if (bullet != null)
            {
                bullets.Add(bullet);
            }
        }

        public void Update()
        {
            if (isGameOver) return;

            // Update bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update();
                
                if (bullets[i].IsOutOfBounds(width, height))
                {
                    bullets.RemoveAt(i);
                    continue;
                }

                // Check collision with enemies
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (bullets[i].CollidesWith(enemies[j]))
                    {
                        score += 10;
                        bullets.RemoveAt(i);
                        enemies.RemoveAt(j);
                        break;
                    }
                }
            }

            // Spawn enemies
            enemySpawnCounter++;
            int spawnRate = Math.Max(30, 120 - waveCount * 5);
            
            if (enemySpawnCounter > spawnRate && enemies.Count < 3 + waveCount)
            {
                SpawnEnemy();
                enemySpawnCounter = 0;
            }

            // Update enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(player.X, player.Y);

                // Check collision with player
                if (enemies[i].CollidesWith(player))
                {
                    lives--;
                    enemies.RemoveAt(i);
                    
                    if (lives <= 0)
                    {
                        isGameOver = true;
                    }
                }
            }

            // Increase difficulty
            if (enemies.Count == 0 && score > waveCount * 100)
            {
                waveCount++;
            }
        }

        private void SpawnEnemy()
        {
            int side = random.Next(4);
            float x, y;

            switch (side)
            {
                case 0: // Top
                    x = random.Next(width);
                    y = -20;
                    break;
                case 1: // Bottom
                    x = random.Next(width);
                    y = height + 20;
                    break;
                case 2: // Left
                    x = -20;
                    y = random.Next(height);
                    break;
                default: // Right
                    x = width + 20;
                    y = random.Next(height);
                    break;
            }

            enemies.Add(new Enemy(x, y, 12, 2 + waveCount * 0.5f));
        }

        public void Draw(Graphics g)
        {
            // Draw player
            player.Draw(g);

            // Draw bullets
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(g);
            }

            // Draw enemies
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(g);
            }
        }

        public void Restart()
        {
            bullets.Clear();
            enemies.Clear();
            player = new Player(width / 2, height / 2, 15);
            score = 0;
            lives = 3;
            waveCount = 1;
            enemySpawnCounter = 0;
            isGameOver = false;
        }
    }
}

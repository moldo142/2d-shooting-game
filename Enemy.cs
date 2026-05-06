using System;
using System.Drawing;

namespace ShootingGame
{
    public class Enemy
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Radius { get; private set; }
        
        private float speed;

        public Enemy(float x, float y, float radius, float speed)
        {
            X = x;
            Y = y;
            Radius = radius;
            this.speed = speed;
        }

        public void Update(float playerX, float playerY)
        {
            // Move towards player
            float dx = playerX - X;
            float dy = playerY - Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;

                X += dx * speed;
                Y += dy * speed;
            }
        }

        public bool CollidesWith(Player player)
        {
            float dx = X - player.X;
            float dy = Y - player.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance < Radius + player.Radius;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(Pens.DarkRed, X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
    }
}

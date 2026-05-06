using System;
using System.Drawing;

namespace ShootingGame
{
    public class Bullet
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Radius { get; private set; }
        
        private float velocityX;
        private float velocityY;
        private float speed;

        public Bullet(float x, float y, float dirX, float dirY, float speed)
        {
            X = x;
            Y = y;
            Radius = 5f;
            this.speed = speed;
            velocityX = dirX * speed;
            velocityY = dirY * speed;
        }

        public void Update()
        {
            X += velocityX;
            Y += velocityY;
        }

        public bool IsOutOfBounds(int width, int height)
        {
            return X < -20 || X > width + 20 || Y < -20 || Y > height + 20;
        }

        public bool CollidesWith(Enemy enemy)
        {
            float dx = X - enemy.X;
            float dy = Y - enemy.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance < Radius + enemy.Radius;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(Pens.Orange, X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
    }
}

using System;
using System.Drawing;

namespace ShootingGame
{
    public class Player
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Radius { get; private set; }
        
        private float aimX;
        private float aimY;
        private float speed = 5f;
        private int shootCooldown = 0;

        public Player(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            aimX = x;
            aimY = y;
        }

        public void Move(float dirX, float dirY, int width, int height)
        {
            if (dirX == 0 && dirY == 0) return;

            // Normalize direction
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            dirX /= length;
            dirY /= length;

            float newX = X + dirX * speed;
            float newY = Y + dirY * speed;

            // Keep player in bounds
            newX = Math.Max(Radius, Math.Min(width - Radius, newX));
            newY = Math.Max(Radius, Math.Min(height - Radius, newY));

            X = newX;
            Y = newY;
        }

        public void UpdateAim(Point mousePos)
        {
            aimX = mousePos.X;
            aimY = mousePos.Y;
        }

        public Bullet Shoot(Point targetPos)
        {
            if (shootCooldown > 0)
            {
                shootCooldown--;
                return null;
            }

            float dx = targetPos.X - X;
            float dy = targetPos.Y - Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            if (length == 0) return null;

            dx /= length;
            dy /= length;

            shootCooldown = 8; // Cooldown between shots
            return new Bullet(X, Y, dx, dy, 8f);
        }

        public void Draw(Graphics g)
        {
            // Draw player circle
            g.FillEllipse(Brushes.Blue, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawEllipse(Pens.LightBlue, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            // Draw aim line
            float dx = aimX - X;
            float dy = aimY - Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            
            if (length > 0)
            {
                dx /= length;
                dy /= length;
                
                float endX = X + dx * 50;
                float endY = Y + dy * 50;
                
                g.DrawLine(Pens.Cyan, X, Y, endX, endY);
            }
        }

        public bool CollidesWith(float otherX, float otherY, float otherRadius)
        {
            float dx = X - otherX;
            float dy = Y - otherY;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance < Radius + otherRadius;
        }
    }
}

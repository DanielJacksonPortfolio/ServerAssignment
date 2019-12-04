using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTypes
{
    [Serializable]
    public abstract class Weapon : GameItem
    {
        protected Weapon(int x, int y, int width, int height):base(x,y,width,height) { }
        protected int range;
        protected bool collision;
        protected int damage;

        public Tex2D GetTexture() { return this.texture; }
        public int GetRange() { return this.range; }
        public int GetDamage() { return this.damage; }
        public void SetTexture(Tex2D texture) { this.texture = texture; }
        public void SetRange(int range) { this.range = range; }
        public void SetDamage(int damage) { this.damage = damage; }
        public void UpdateOrigin(int x, int y) { this.hitbox.X = x; this.hitbox.Y = y; }

        float GetAngle()
        {
            float angle = 0;
            if (Mouse.GetState().X != this.hitbox.X)
            {
                double yDiff = Mouse.GetState().Y - this.hitbox.Y;
                double absYDiff = yDiff < 0 ? -yDiff : yDiff;
                double xDiff = Mouse.GetState().X - this.hitbox.X;
                double absXDiff = xDiff < 0 ? -xDiff : xDiff;
                angle = (float)Math.Atan(absYDiff / absXDiff);
                angle = xDiff < 0 ? (yDiff > 0 ? (float)Math.PI - angle : -(float)Math.PI + angle) : (angle = yDiff > 0 ? angle : -angle);
            }
            return angle;
        }

        public void Attack(Player player)
        {
            player.Damage(damage);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawLine(spriteBatch, new Vector2(this.hitbox.X, this.hitbox.Y), (float)range, GetAngle(), this.collision ? Color.Red : Color.White);
        }
        
        public void SetCollide(bool collision) { this.collision = collision; }
        public bool Collide(Rect playerHitbox)
        {
            float angle = GetAngle();
            Vector2 line1Start = new Vector2(this.hitbox.X, this.hitbox.Y);
            Vector2 line1End = new Vector2(this.hitbox.X + (int)(this.range * Math.Cos(angle)), this.hitbox.Y + (int)(this.range * Math.Sin(angle)));

            bool left = LineLineCollision(line1Start, line1End, playerHitbox.X - playerHitbox.Width, playerHitbox.Y - playerHitbox.Height, playerHitbox.X - playerHitbox.Width, playerHitbox.Y + playerHitbox.Height);
            bool right = LineLineCollision(line1Start, line1End, playerHitbox.X + playerHitbox.Width, playerHitbox.Y - playerHitbox.Height, playerHitbox.X + playerHitbox.Width, playerHitbox.Y + playerHitbox.Height);
            bool top = LineLineCollision(line1Start, line1End, playerHitbox.X - playerHitbox.Width, playerHitbox.Y - playerHitbox.Height, playerHitbox.X + playerHitbox.Width, playerHitbox.Y - playerHitbox.Height);
            bool bottom = LineLineCollision(line1Start, line1End, playerHitbox.X - playerHitbox.Width, playerHitbox.Y + playerHitbox.Height, playerHitbox.X + playerHitbox.Width, playerHitbox.Y + playerHitbox.Height);
            if (left || right || top || bottom)
                this.collision = true;
            return this.collision;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    abstract class GameItem
    {
        protected Rectangle hitbox = new Rectangle(0, 0, 0, 0);
        protected Texture2D texture;
        protected KeyboardState oldKeyboardState = Keyboard.GetState();
        protected MouseState oldMouseState = Mouse.GetState();

        public GameItem(int x, int y, int width, int height)
        {
            hitbox = new Rectangle(x, y, width, height);
        }

        public void LoadTexture(Texture2D texture) { this.texture = texture; }

        public virtual void Draw(SpriteBatch spriteBatch) { }
        public Rectangle GetHitbox() { return this.hitbox; }
        protected virtual Rectangle GetDrawHitbox() { return new Rectangle(this.hitbox.X - this.hitbox.Width, this.hitbox.Y - this.hitbox.Height, this.hitbox.Width * 2, this.hitbox.Height * 2); }
        public void SetHitbox(Rectangle newBox) { this.hitbox = newBox; }
        public void SetHitbox(int x, int y, int w, int h) { this.hitbox = new Rectangle(x, y, w, h); }

        protected bool LineLineCollision(Vector2 line1Start, Vector2 line1End, float x3, float y3, float x4, float y4)
        {
            // calculate the distance to intersection point
            float uA = ((x4 - x3) * (line1Start.Y - y3) - (y4 - y3) * (line1Start.X - x3)) / ((y4 - y3) * (line1End.X - line1Start.X) - (x4 - x3) * (line1End.Y - line1Start.Y));
            float uB = ((line1End.X - line1Start.X) * (line1Start.Y - y3) - (line1End.Y - line1Start.Y) * (line1Start.X - x3)) / ((y4 - y3) * (line1End.X - line1Start.X) - (x4 - x3) * (line1End.Y - line1Start.Y));

            // if uA and uB are between 0-1, lines are colliding
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                // optionally, draw a circle where the lines meet
                float intersectionX = line1Start.X + (uA * (line1End.X - line1Start.X));
                float intersectionY = line1Start.Y + (uA * (line1End.Y - line1Start.Y));
                return true;
            }
            return false;
        }

        private Texture2D GetTexture(SpriteBatch spriteBatch)
        {
            if (texture == null)
            {
                texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                texture.SetData(new[] { Color.White });
            }
            return texture;
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(GetTexture(spriteBatch), point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}

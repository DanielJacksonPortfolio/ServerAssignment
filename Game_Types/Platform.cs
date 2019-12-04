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
    public class Platform : GameItem
    {
        public Platform(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(this.texture.GetTexture(spriteBatch.GraphicsDevice) != null)
                spriteBatch.Draw(this.texture.GetTexture(spriteBatch.GraphicsDevice), GetDrawHitbox(), Color.White);
        }

        public void Update(GameTime gameTime, Player player)
        {
            Rect pHitbox = player.GetHitbox();

            if (pHitbox.X + pHitbox.Width > this.hitbox.X - this.hitbox.Width  && 
                pHitbox.X - pHitbox.Width < this.hitbox.X + this.hitbox.Width  && 
                pHitbox.Y + pHitbox.Height > this.hitbox.Y - this.hitbox.Height && 
                pHitbox.Y - pHitbox.Height < this.hitbox.Y + this.hitbox.Height) // Collision
            {
                int nextX, nextY;
                int xDiff, yDiff;

                if (pHitbox.X > this.hitbox.X)
                    xDiff = Math.Abs(pHitbox.X - pHitbox.Width - this.hitbox.X - this.hitbox.Width);
                else
                    xDiff = Math.Abs(pHitbox.X + pHitbox.Width - this.hitbox.X + this.hitbox.Width);

                if (pHitbox.Y > this.hitbox.Y)
                    yDiff = Math.Abs(pHitbox.Y - pHitbox.Height - this.hitbox.Y - this.hitbox.Height);
                else
                    yDiff = Math.Abs(pHitbox.Y + pHitbox.Height - this.hitbox.Y + this.hitbox.Height);

                if (xDiff > yDiff)
                {
                    nextX = pHitbox.X;
                    if (pHitbox.Y > this.hitbox.Y)
                    {
                        nextY = this.hitbox.Y + this.hitbox.Height + pHitbox.Height; // Bottom of Object Collision
                        player.SetVelocity(player.GetVelocity().X, 0);
                        player.SetCollidingBottom(true);
                    }
                    else
                    {
                        nextY = this.hitbox.Y - this.hitbox.Height - pHitbox.Height; // Top of Object Collision
                        player.SetVelocity(player.GetVelocity().X, 0);
                        player.SetCollidingTop(true);
                    }
                }
                else
                {
                    if (pHitbox.X > this.hitbox.X)
                    { 
                        nextX = this.hitbox.X + this.hitbox.Width + pHitbox.Width; // Right of Object Collision
                        player.SetVelocity(0, player.GetVelocity().Y);
                        player.SetCollidingRight(true);

                    }
                    else
                    { 
                        nextX = this.hitbox.X - this.hitbox.Width - pHitbox.Width; // Left of Object Collision
                        player.SetVelocity(0, player.GetVelocity().Y);
                        player.SetCollidingLeft(true);
                    }
                    nextY = pHitbox.Y;
                }

                player.SetHitbox(nextX, nextY, pHitbox.Width, pHitbox.Height);
                player.SetColliding(true);
            }
        }
    }
}

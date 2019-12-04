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
    class Level
    {
        List<Platform> platforms = new List<Platform>();

        public void AddPlatform(Platform platform)
        {
            platforms.Add(platform);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, List<Player> players)
        {
            foreach(Platform platform in platforms)
            {
                foreach (Player player in players)
                {
                    platform.Update(gameTime, player);
                }
            }
            foreach (Player player in players)
            {
                player.WeaponCollide(platforms, players);
            }
        }
    }
}

using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;

using PacketData;
using GameTypes;

namespace Client
{
    public class Client_Game
    {
        Level level;
        List<Player> players;// = new List<Player>();
        //Player clientPlayer;
        int playerID;

        public Client_Game(InitGamePacket packet)
        {
            playerID = packet.playerID;
            players = packet.players;
            level = packet.level;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            //FileStream fileStream = new FileStream("Content/2D/Stick.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            //Texture2D stickSheet = Texture2D.FromStream(GraphicsDevice, fileStream);
            //fileStream = new FileStream("Content/2D/platform.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            //Texture2D platformImage = Texture2D.FromStream(GraphicsDevice, fileStream);
            //fileStream.Close();
    
            for(int i = 0; i < players.Count; ++i)
            {
                players[i].LoadTexture("Content/2D/Stick.png");
            }
            level.TexturePlatforms("Content/2D/platform.png");
        }

        public void Update(GameTime gameTime)
        {
            foreach (Player player in players)
                player.Update(gameTime);
            level.Update(gameTime, players);
        }

        public void Draw(GameTime gameTime, MonoGame.Forms.Services.MonoGameService Editor)
        {
            level.Draw(Editor.spriteBatch);
            foreach (Player player in players)
                player.Draw(Editor.spriteBatch);
        }
    }

}

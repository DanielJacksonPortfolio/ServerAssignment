using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;

using PacketData;
using Game;

namespace Client
{
    public class Client_Game
    {
        Level level = new Level();
        List<Player> players = new List<Player>();
        Player clientPlayer;
        int playerID;

        public List<float> GetPlayerData()
        {
            List<float> data = new List<float>();
            data.Add(clientPlayer.GetHitbox().X);
            data.Add(clientPlayer.GetHitbox().Y);
            data.Add((float)clientPlayer.GetFrameID());
            //data.Add((float)clientPlayer.GetAnimCounter());
            //data.Add((float)clientPlayer.GetDamageCounter());
            return data;
        }
        public int GetPlayerID()
        {
            return playerID;
        }

        public Client_Game(InitGamePacket packet)
        {
            Keys[] player1Controls = { Keys.W, Keys.A, Keys.D, Keys.S };

            level.AddPlatform(new Platform(500, 550, 200, 25));
            level.AddPlatform(new Platform(200, 225, 100, 25));
            level.AddPlatform(new Platform(720, 785, 720, 25));
            level.AddPlatform(new Platform(720, 25, 720, 25));
            level.AddPlatform(new Platform(25, 405, 25, 355));
            level.AddPlatform(new Platform(1415, 405, 25, 355));
            level.AddPlatform(new Platform(1100, 325, 150, 25));

            playerID = packet.playerID;
            foreach(List<float> playerData in packet.players)
                players.Add(new Player((int)playerData[0], (int)playerData[1], 17, 50, new Color((int)playerData[2], (int)playerData[3], (int)playerData[4]), player1Controls, true));
            clientPlayer = new Player((int)packet.clientPlayerData[0], (int)packet.clientPlayerData[1], 17, 50, new Color((int)packet.clientPlayerData[2], (int)packet.clientPlayerData[3], (int)packet.clientPlayerData[4]), player1Controls, true);
            foreach (List<float> platformData in packet.level)
                level.AddPlatform(new Platform((int)platformData[0], (int)platformData[1], (int)platformData[2], (int)platformData[3]));
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            FileStream fileStream = new FileStream("Content/2D/Stick.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            Texture2D stickSheet = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream = new FileStream("Content/2D/platform.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            Texture2D platformImage = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Close();

            clientPlayer.LoadTexture(stickSheet);
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].LoadTexture(stickSheet);
            }
            level.TexturePlatforms(platformImage);
        }


        public void WorldUpdate(WorldUpdatePacket packet)
        {
            for (int i = 0; i < packet.players.Count; ++i)
            {
                players[i].SetHitbox((int)packet.players[i][0], (int)packet.players[i][1], players[i].GetHitbox().Width, players[i].GetHitbox().Height);
                players[i].SetFrame((Player.AnimFrame)packet.players[i][2]);
                //players[i].SetAnimCounter((int)packet.players[i][3]);
                //players[i].SetDamageCounter((int)packet.players[i][4]);
            }
        }

        public void Update(GameTime gameTime)
        {
            clientPlayer.Update(gameTime);
            //foreach (Player player in players)
            //    player.Update(gameTime,false);
            level.Update(gameTime, players);
            level.Update(gameTime, clientPlayer, players);
        }

        public void Draw(GameTime gameTime, MonoGame.Forms.Services.MonoGameService Editor)
        {
            level.Draw(Editor.spriteBatch);
            clientPlayer.Draw(Editor.spriteBatch);
            foreach (Player player in players)
                player.Draw(Editor.spriteBatch);
        }
    }

}

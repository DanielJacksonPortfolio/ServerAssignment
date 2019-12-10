using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;
using PacketData;

namespace Client
{
    public class MonoGameWindow : MonoGameControl
    {
        Client_Game game;
        GameTime gameTime;
        Client_Client client;

        public MonoGameWindow(InitGamePacket packet, Client_Client client)
        {
            game = new Client_Game(packet);
            this.client = client;
        }
        //public MonoGameWindow()
        //{
        //}

        protected override void Initialize()
        {
            base.Initialize();
            game.LoadContent(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            game.Update(gameTime);
            this.gameTime = gameTime;

            client.UDPSend(client.CreatePlayerUpdatePacket(game.GetPlayerData(),game.GetPlayerID()));
        }
        public void WorldUpdate(WorldUpdatePacket packet)
        {
            game.WorldUpdate(packet);
        }

        protected override void Draw()
        {
            base.Draw();
            GraphicsDevice.Clear(Color.Black);
            Editor.spriteBatch.Begin();
            game.Draw(gameTime, Editor);
            Editor.spriteBatch.End();
        }
    }

}

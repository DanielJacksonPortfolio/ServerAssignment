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

        public MonoGameWindow(InitGamePacket packet)
        {
            game = new Client_Game(packet);
        }

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

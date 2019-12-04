using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;

namespace Client
{
    public class MonoGameWindow : MonoGameControl
    {
        Client_Game game = new Client_Game();
        GameTime gameTime;

        protected override void Initialize()
        {
            base.Initialize();
            game.LoadContent();
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
        //protected void Draw(GameTime gameTime, MonoGame.Forms.Services.MonoGameService Editor)
        //{
        //    level.Draw(Editor.spriteBatch);
        //    foreach (Player player in players)
        //        player.Draw(Editor.spriteBatch);
        //}
    }

}

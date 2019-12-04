using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.IO;

namespace Game
{
    public class MonoGameWindow : MonoGameControl
    {
        Level level;
        List<Player> players = new List<Player>();
        Random rand = new Random();

        protected override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }

        Color RandColor()
        {
            float r = rand.Next(0, 255);
            float g = rand.Next(0, 255);
            float b = rand.Next(0, 255);
            return Color.FromNonPremultiplied(new Vector4(r / 255, g / 255, b / 255, 1.0f));
        }

        protected void LoadContent()
        {
            FileStream fileStream = new FileStream("Content/2D/Stick.png", FileMode.Open);
            Texture2D stickSheet = Texture2D.FromStream(this.GraphicsDevice, fileStream);// this.Content.Load<Texture2D>("2D/Stick");
            fileStream = new FileStream("Content/2D/platform.png", FileMode.Open);
            Texture2D platformImage = Texture2D.FromStream(this.GraphicsDevice, fileStream); ;// this.Content.Load<Texture2D>("2D/platform");
            Keys[] player1Controls = { Keys.W, Keys.A, Keys.D, Keys.S };
            Keys[] player2Controls = { Keys.Up, Keys.Left, Keys.Right, Keys.Down };
            Keys[] player3Controls = { Keys.T, Keys.F, Keys.H, Keys.G };
            Keys[] player4Controls = { Keys.I, Keys.J, Keys.L, Keys.K };
            Player player1 = new Player(475, 60, 17, 50, RandColor(), player1Controls, true);
            player1.LoadTexture(stickSheet);
            players.Add(player1);
            Player player2 = new Player(775, 60, 17, 50, RandColor(), player2Controls, false);
            player2.LoadTexture(stickSheet);
            players.Add(player2);
            Player player3 = new Player(105, 600, 17, 50, RandColor(), player3Controls, true);
            player3.LoadTexture(stickSheet);
            players.Add(player3);
            Player player4 = new Player(1005, 60, 17, 50, RandColor(), player4Controls, false);
            player4.LoadTexture(stickSheet);
            players.Add(player4);

            level = new Level();

            Platform platform1 = new Platform(500, 550, 200, 25);
            platform1.LoadTexture(platformImage);
            Platform platform2 = new Platform(200, 225, 100, 25);
            platform2.LoadTexture(platformImage);
            Platform platform3 = new Platform(720, 785, 720, 25);
            platform3.LoadTexture(platformImage);
            Platform platform4 = new Platform(720, 25, 720, 25);
            platform4.LoadTexture(platformImage);
            Platform platform5 = new Platform(25, 405, 25, 355);
            platform5.LoadTexture(platformImage);
            Platform platform6 = new Platform(1415, 405, 25, 355);
            platform6.LoadTexture(platformImage);
            Platform platform7 = new Platform(1100, 325, 150, 25);
            platform7.LoadTexture(platformImage);

            level.AddPlatform(platform1);
            level.AddPlatform(platform2);
            level.AddPlatform(platform3);
            level.AddPlatform(platform4);
            level.AddPlatform(platform5);
            level.AddPlatform(platform6);
            level.AddPlatform(platform7);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (Player player in players)
                player.Update(gameTime);
            level.Update(gameTime, players);

            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();
            Draw(new GameTime());
        }
        protected void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Editor.spriteBatch.Begin();
            level.Draw(Editor.spriteBatch);
            foreach (Player player in players)
                player.Draw(Editor.spriteBatch);
            Editor.spriteBatch.End();
        }
    }

}

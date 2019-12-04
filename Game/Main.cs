//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//using System;
//using System.Collections.Generic;

//namespace Game
//{
//    public class Main : Game
//    {
//        GraphicsDeviceManager graphics;
//        SpriteBatch spriteBatch;
//        Level level;
//        List<Player> players = new List<Player>();
//        Random rand = new Random();

//        public Main()
//        {
//            graphics = new GraphicsDeviceManager(this)
//            {
//                PreferredBackBufferWidth = 1440,
//                PreferredBackBufferHeight = 810
//            };
//            graphics.ApplyChanges();

//            Content.RootDirectory = "Content";
//        }

//        protected override void Initialize()
//        {
//            this.IsMouseVisible = true;
//            spriteBatch = new SpriteBatch(GraphicsDevice);
//            base.Initialize();
//        }

//        Color RandColor()
//        {
//            float r = rand.Next(0, 255);
//            float g = rand.Next(0, 255);
//            float b = rand.Next(0, 255);
//            return Color.FromNonPremultiplied(new Vector4(r/255, g/255, b/255, 1.0f));
//        }

//        protected override void LoadContent()
//        {
//            Texture2D stickSheet = this.Content.Load<Texture2D>("2D/Stick");
//            Texture2D platformImage = this.Content.Load<Texture2D>("2D/platform");
//            Keys[] player1Controls = { Keys.W, Keys.A, Keys.D, Keys.S };
//            Keys[] player2Controls = { Keys.Up, Keys.Left, Keys.Right, Keys.Down };
//            Player clientPlayer = new Player(475, 60, 17, 50, RandColor(), player1Controls,true);
//            clientPlayer.LoadTexture(stickSheet);
//            players.Add(clientPlayer);
//            Player player2 = new Player(775, 60, 17, 50, RandColor(), player2Controls,false);
//            player2.LoadTexture(stickSheet);
//            players.Add(player2);

//            level = new Level();

//            Platform platform1 = new Platform(500, 550, 200, 25);
//            platform1.LoadTexture(platformImage);
//            Platform platform2 = new Platform(200, 225, 100, 25);
//            platform2.LoadTexture(platformImage);
//            Platform platform3 = new Platform(720, 785, 720, 25);
//            platform3.LoadTexture(platformImage);
//            Platform platform4 = new Platform(720, 25, 720, 25);
//            platform4.LoadTexture(platformImage);
//            Platform platform5 = new Platform(25, 405, 25, 355);
//            platform5.LoadTexture(platformImage);
//            Platform platform6 = new Platform(1415, 405, 25, 355);
//            platform6.LoadTexture(platformImage);
//            Platform platform7 = new Platform(1100, 325, 150, 25);
//            platform7.LoadTexture(platformImage);

//            level.AddPlatform(platform1);
//            level.AddPlatform(platform2);
//            level.AddPlatform(platform3);
//            level.AddPlatform(platform4);
//            level.AddPlatform(platform5);
//            level.AddPlatform(platform6);
//            level.AddPlatform(platform7);
//        }

//        protected override void UnloadContent()
//        {
//            // TODO: Unload any non ContentManager content here
//        }

//        protected override void Update(GameTime gameTime)
//        { 
//            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
//                Exit();

//            foreach (Player player in players)
//                player.Update(gameTime);
//            level.Update(gameTime, players);

//            base.Update(gameTime);
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.Black);

//            spriteBatch.Begin();
//            level.Draw(spriteBatch);
//            foreach (Player player in players)
//                player.Draw(spriteBatch);
//            spriteBatch.End();
            
//            base.Draw(gameTime);
//        }
//    }
        
//    public static class Program
//    {
//        static void Main()
//        {
//            using (var game = new Main())
//                game.Run();
//        }
//    }
//}
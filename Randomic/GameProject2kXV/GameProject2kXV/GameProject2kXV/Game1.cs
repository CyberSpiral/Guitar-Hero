using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameProject2kXV
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GenerateNewFloor genFloor;
        Map map;


        int screenWidth;
        int screenHeight;
        SpriteFont sprFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight += 200;
            graphics.PreferredBackBufferWidth += 200;
        }

        protected override void Initialize()
        {
            map = new Map();
            genFloor = new GenerateNewFloor();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //sprFont = Content.Load<SpriteFont>("sprFont");


            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;


            Rooms.Content = Content;

            map.Generate(genFloor.GenerateRooms());
        }

        protected override void UnloadContent()
        {

        }   //nothing yet

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                map = new Map();
                map.Generate(genFloor.GenerateRooms());
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            map.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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

namespace TheWorld {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Texture2D> background;
        KeyboardState oldState;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = World.RoomWidth;
            graphics.PreferredBackBufferHeight = World.RoomHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new List<Texture2D>();
            background.Add(Content.Load<Texture2D>("back1"));
            background.Add(Content.Load<Texture2D>("back2"));
            World.GenerateFloor();
            World.GenerateRooms(background);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.Q)) {
                World.GenerateFloor();
                World.GenerateRooms(background);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W)) {
                World.CurrentRoom[1] -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S)) {
                World.CurrentRoom[1] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D)) {
                World.CurrentRoom[0] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A)) {
                World.CurrentRoom[0] -= 1;
            }

            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(World.Rooms[World.CurrentRoom[0], World.CurrentRoom[1]].Background, new Vector2(0, 0), Color.White);
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    if (World.ActiveRooms[i,q] == true) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * i, 20 + 10 * q, 9, 9),Color.White);
                    }
                }
            }
            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * World.CurrentRoom[0], 20 + 10 * World.CurrentRoom[1], 9, 9), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

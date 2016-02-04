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
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Texture2D> background;
        List<Texture2D> objects;
        KeyboardState oldState;
        Room CurrentRoom {
            get { return World.Rooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1]]; }
        }

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = World.RoomWidth;
            graphics.PreferredBackBufferHeight = World.RoomHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new List<Texture2D>();
            objects = new List<Texture2D>();
            background.Add(Content.Load<Texture2D>("back1"));
            background.Add(Content.Load<Texture2D>("back2"));
            objects.Add(Content.Load<Texture2D>("dot"));
            World.GenerateFloor();
            World.GenerateRooms(background, objects);

            
        }
        
        protected override void UnloadContent() {
        }
        
        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.Q)) {
                World.GenerateFloor();
                World.GenerateRooms(background,objects);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W)) {
                World.CurrentRoomLocationCode[1] -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S)) {
                World.CurrentRoomLocationCode[1] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D)) {
                World.CurrentRoomLocationCode[0] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A)) {
                World.CurrentRoomLocationCode[0] -= 1;
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

            spriteBatch.Draw(CurrentRoom.Background, new Vector2(0, 0), Color.White);
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    if (World.ActiveRooms[i,q] == true) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * i, 20 + 10 * q, 9, 9),Color.White);
                    }
                }
            }
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    if (i == 0 || q == 0 || i == 24 || q == 24) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * i, 20 + 10 * q, 9, 9), Color.White);
                    }
                }
            }
            foreach (GameObject item in CurrentRoom.Props) {
                item.Draw(spriteBatch);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * CurrentRoom.XCoordinate, 20 + 10 * CurrentRoom.YCoordinate, 9, 9), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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

namespace MusicGame {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player p;
        Monster t;
        List<Texture2D> objects;
        Texture2D tex;
        Texture2D back;
        World world;

        MapDraw drawMap;



        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Room.height;
            graphics.PreferredBackBufferWidth = Room.width;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
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
            p = new Player(Content.Load<Texture2D>("character"), new Vector2(200, 200), 3, 1, 9, 9, 100);
            t = new Monster(Content.Load<Texture2D>("zombiesheet2"), new Vector2(400, 400), 1, 1, 4, 4, 500);
            tex = Content.Load<Texture2D>("dot");
            back = Content.Load<Texture2D>("back2");
            objects = new List<Texture2D>();
            objects.Add(Content.Load<Texture2D>("box"));
            world = new World(objects);

            Room.Content = Content;
            drawMap = new MapDraw();
            drawMap.Generate();

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                drawMap.Generate();
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            p.Update(elapsed, Keyboard.GetState(), Keyboard.GetState(), Mouse.GetState());
            t.Update(elapsed, p.Position);


            if (p.CollisionBox.Intersects(t.CollisionBox)) {
                p.Position = p.OldPos;
                t.Position = t.OldPos;
            }
                

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();
            spriteBatch.Draw(back, Vector2.Zero, Color.White);

            p.Draw(spriteBatch);
            t.Draw(spriteBatch);
            if (Keyboard.GetState().IsKeyDown(Keys.G)) {
                foreach (var obj in world.currentRoom.Props) {
                    spriteBatch.Draw(tex, obj.CollisionBox, Color.Blue);
                }
                spriteBatch.Draw(tex, p.CollisionBox, Color.Blue);
                spriteBatch.Draw(tex, t.CollisionBox, Color.Blue);
            }

            drawMap.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
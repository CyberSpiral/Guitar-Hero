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
        List<RoomGraphic> roomGraphic;
        List<Texture2D> objects;
        List<Texture2D> monsters;
        KeyboardState oldState;
        Room CurrentRoom {
            get { return World.Rooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1]]; }
        }

        List<Door> doors;
        Player p;

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
            Static.s = Content.Load<SpriteFont>("text");
            monsters = new List<Texture2D>();
            monsters.Add(Content.Load<Texture2D>("zombiesheet2"));
            monsters.Add(Content.Load<Texture2D>("zombiesheet2"));
            roomGraphic = new List<RoomGraphic>();
            objects = new List<Texture2D>();
            objects.Add(Content.Load<Texture2D>("dot"));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("back1"), Content.Load<Texture2D>("background_overlay_number_1"), Content.Load<Texture2D>("door")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("back2"), Content.Load<Texture2D>("background_overlay_number_2"), Content.Load<Texture2D>("door2")));
            World.GenerateFloor();
            World.GenerateRooms(roomGraphic, objects, monsters);

            doors = new List<Door>();

            doors.Add(new Door(roomGraphic[1].Door, new Vector2(38/2, World.RoomHeight / 2), 1, 1, 1, 0));
            doors.Add(new Door(roomGraphic[1].Door, new Vector2(World.RoomWidth - (38 / 2), World.RoomHeight / 2), 1, 1, 1, 0));
            doors.Add(new Door(roomGraphic[1].Door, new Vector2((World.RoomWidth) / 2, 38 / 2), 1, 1, 1, 0));
            doors.Add(new Door(roomGraphic[1].Door, new Vector2((World.RoomWidth) / 2, World.RoomHeight - (38 / 2)), 1, 1, 1, 0));

            p = new Player(Content.Load<Texture2D>("character"), new Vector2(200, 200), 5, 1, 9, 9, 200);

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
            {
                this.Exit();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Q)) {
                World.GenerateFloor();
                World.GenerateRooms(roomGraphic,objects, monsters);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I)) {
                World.CurrentRoomLocationCode[1] -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K)) {
                World.CurrentRoomLocationCode[1] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L)) {
                World.CurrentRoomLocationCode[0] += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J) && oldState.IsKeyUp(Keys.J)) {
                World.CurrentRoomLocationCode[0] -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (World.CurrentRoomLocationCode[0] == World.LastRoom[0] && World.CurrentRoomLocationCode[1] == World.LastRoom[1])
                {
                    World.GenerateFloor();
                    World.GenerateRooms(roomGraphic, objects, monsters);
                }
            }

            oldState = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            p.Update(elapsed, Keyboard.GetState(), Keyboard.GetState(), Mouse.GetState());
            CurrentRoom.Zombies.ForEach(m => m.Update(elapsed, p.Position));
            CurrentRoom.SpitZombies.ForEach(m => m.Update(elapsed, p.Position));
            doors.ForEach(d => p.Position = d.Update(elapsed, p.CollisionBox, p.Position));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(CurrentRoom.RoomGraphic.Background, new Vector2(0, 0), Color.White);
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    if (World.ActiveRooms[i,q] == true) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * i, 20 + 10 * q, 9, 9),Color.White);
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (doors[i].active)
                {
                    doors[i].Draw(spriteBatch);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach (GameObject item in CurrentRoom.Props)
                {
                    item.Draw(spriteBatch);
                }
            }

            p.Draw(spriteBatch);
            CurrentRoom.Zombies.ForEach(m => m.Draw(spriteBatch));
            CurrentRoom.SpitZombies.ForEach(m => m.Draw(spriteBatch));

            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(20 + 10 * CurrentRoom.XCoordinate, 20 + 10 * CurrentRoom.YCoordinate, 9, 9), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

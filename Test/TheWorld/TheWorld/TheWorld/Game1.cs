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
        MouseState ms;
        MouseState msOld;
        Room CurrentRoom {
            get { return World.Rooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1]]; }
        }

        Menu menu;
        Player p;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = World.RoomWidth;
            graphics.PreferredBackBufferHeight = World.RoomHeight + World.UIBar;
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
            monsters.Add(Content.Load<Texture2D>("SpitMoving"));
            monsters.Add(Content.Load<Texture2D>("SpitSpit"));
            roomGraphic = new List<RoomGraphic>();
            objects = new List<Texture2D>();
            objects.Add(Content.Load<Texture2D>("dot"));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("back1"), Content.Load<Texture2D>("door"), Content.Load<Texture2D>("Overlay1")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("back2"), Content.Load<Texture2D>("door2"), Content.Load<Texture2D>("Overlay2")));
            World.GenerateFloor();
            World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"));


            menu = new Menu(Content.Load<Texture2D>("PLAY_button"), Content.Load<Texture2D>("PLAY_flash_button"), Content.Load<Texture2D>("EXIT_button"), 
                Content.Load<Texture2D>("EXIT_flash_button"), Content.Load<Texture2D>("dot"), Content.Load<Texture2D>("dot"));
            p = new Player(Content.Load<Texture2D>("Character_sprite_v2"), Content.Load<Texture2D>("health"), new Vector2(544, 306), 3, 1, 19, 19, 100,
                new Weapon(1f, 3, WeaponType.ElectricGuitar, Content.Load<Texture2D>("dot")));


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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                this.Exit();
            }
            ms = Mouse.GetState();
            menu.Update(ms, msOld);

            if (menu.menuType == MenuType.InGame)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                World.GenerateFloor();
                World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"));
            }
                if (Keyboard.GetState().IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
                {
                World.CurrentRoomLocationCode[1] -= 1;
            }
                if (Keyboard.GetState().IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K))
                {
                World.CurrentRoomLocationCode[1] += 1;
            }
                if (Keyboard.GetState().IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L))
                {
                World.CurrentRoomLocationCode[0] += 1;
            }
                if (Keyboard.GetState().IsKeyDown(Keys.J) && oldState.IsKeyUp(Keys.J))
                {
                World.CurrentRoomLocationCode[0] -= 1;
            }

                if (Keyboard.GetState().IsKeyDown(Keys.G))
                {
                    if (World.CurrentRoomLocationCode[0] == World.LastRoom[0] && World.CurrentRoomLocationCode[1] == World.LastRoom[1])
                    {
                    World.GenerateFloor();
                    World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("heart"));
                    World.CurrentLevel += 1;
                }
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            p.Update(elapsed, Keyboard.GetState(), oldState, Mouse.GetState(), msOld);
            p.Position = p.Position.X < 50 ? p.OldPos : p.Position;
            p.Position = p.Position.X > World.RoomWidth - 50 ? p.OldPos : p.Position;
            p.Position = p.Position.Y < 60 ? p.OldPos : p.Position;
            p.Position = p.Position.Y > World.RoomHeight - 60 ? p.OldPos : p.Position;

                #region Collision
                CurrentRoom.Doors.ForEach(d => p.Position = d.Update(elapsed, p.CollisionBox, p.Position));
                foreach (Zombie z in CurrentRoom.Monsters.Where(x => x is Zombie))
                {
                z.Update(elapsed, p.Position);
                    if (z.CollisionBox.Intersects(p.CollisionBox) && p.invTmr <= 0)
                    {
                    p.Health -= 1;
                    p.invTmr = 1.5f;
                }
            }
                foreach (SpitZombie sZ in CurrentRoom.Monsters.Where(x => x is SpitZombie))
                {
                sZ.Update(elapsed, p.Position, CurrentRoom.Props);
                    for (int i = 0; i < sZ.SpitList.Count; i++)
                    {
                        if (sZ.SpitList[i].CollisionBox.Intersects(p.CollisionBox) && p.invTmr <= 0)
                        {
                        p.Health--;
                        p.invTmr = 1.5f;
                        sZ.SpitList.RemoveAt(i);
                        i--;
                    }
                        else if (sZ.SpitList[i].CollisionBox.Intersects(p.CollisionBox))
                        {
                        sZ.SpitList.RemoveAt(i);
                        i--;
                    }
                }

            }

                foreach (var item in CurrentRoom.Props)
                {
                    if (item.CollisionBox.Intersects(p.CollisionBox))
                    {
                    p.Position = p.OldPos;
                }
                    foreach (Zombie z in CurrentRoom.Monsters.Where(x => x is Zombie))
                    {
                        if (z.CollisionBox.Intersects(item.CollisionBox))
                        {
                        z.Position = z.OldPos;
                    }
                }
                    foreach (SpitZombie sZ in CurrentRoom.Monsters.Where(x => x is SpitZombie))
                    {
                        if (sZ.CollisionBox.Intersects(item.CollisionBox))
                        {
                        sZ.Position = sZ.OldPos;
                        sZ.facingTowards = true;
                    }
                }
            }
                for (int i = 0; i < CurrentRoom.Monsters.Count; i++)
                {
                    for (int q = 0; q < p.Weapon.hit.Count; q++)
                    {
                        if (CurrentRoom.Monsters[i].CollisionBox.Intersects(p.Weapon.hit[q].HitCollisionBox))
                        {
                        CurrentRoom.Monsters[i].Health -= p.Weapon.damage;
                            if (CurrentRoom.Monsters[i].Health <= 0)
                            {
                                if (CurrentRoom.Monsters[i] is Zombie)
                                {
                                CurrentRoom.Animations.Add(new TempObject(Content.Load<Texture2D>("Zombie_death_sprite"), CurrentRoom.Monsters[i].Position
                                    , 1, 15, 15, 200, CurrentRoom.Monsters[i].Rotation));
                            }
                                CurrentRoom.Monsters[i].Collectable = true;
                        }
                    }

                }
                    for (int q = 0; q < p.Weapon.projectile.Count; q++)
                    {
                        if (CurrentRoom.Monsters[i].CollisionBox.Intersects(p.Weapon.projectile[q].HitCollisionBox))
                        {
                            CurrentRoom.Monsters[i].Health -= p.Weapon.damage;
                            if (CurrentRoom.Monsters[i].Health <= 0)
                            {
                                if (CurrentRoom.Monsters[i] is Zombie)
                                {
                                    CurrentRoom.Animations.Add(new TempObject(Content.Load<Texture2D>("Zombie_death_sprite"), CurrentRoom.Monsters[i].Position
                                        , 1, 15, 15, 200, CurrentRoom.Monsters[i].Rotation));
                                }
                                CurrentRoom.Monsters[i].Collectable = true;
                            }
                            p.Weapon.projectile[q].Collectable = true;
                        }

                    }
                }
                #endregion
                #region A
                #endregion
                CurrentRoom.Animations.ForEach(x => x.Update(elapsed));
                #region Garbage
                for (int i = 0; i < CurrentRoom.Animations.Count; i++)
                {
                    if (CurrentRoom.Animations[i].Collectable)
                    {
                    CurrentRoom.Animations.RemoveAt(i);
                    i--;
                }
            }
                for (int i = 0; i < CurrentRoom.Monsters.Count; i++)
                {
                    if (CurrentRoom.Monsters[i].Collectable)
                    {
                        CurrentRoom.Monsters.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < p.Weapon.projectile.Count; i++)
                {
                    if (p.Weapon.projectile[i].Collectable)
                    {
                        p.Weapon.projectile.RemoveAt(i);
                        i--;
                    }
                }
                #endregion
            }

            msOld = Mouse.GetState();
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(0, World.UIBar, 0));

            if (menu.menuType == MenuType.InGame)
            {
            CurrentRoom.Draw(spriteBatch);

                for (int i = 0; i < 25; i++)
                {
                    for (int q = 0; q < 25; q++)
                    {
                        if (World.ActiveRooms[i, q] == true)
                        {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * i, -40 + 10 * q - World.UIBar, 9, 9), Color.White);
                    }
                }
            }
            CurrentRoom.Doors.Where(x => x.active).ToList().ForEach(x => x.Draw(spriteBatch));

                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                CurrentRoom.Props.ForEach(x => spriteBatch.Draw(Content.Load<Texture2D>("dot"), x.CollisionBox, Color.Red));
                    foreach (Zombie z in CurrentRoom.Monsters.Where(x => x is Zombie))
                    {
                    spriteBatch.Draw(Content.Load<Texture2D>("dot"), z.CollisionBox, Color.Red);
                }
                spriteBatch.Draw(Content.Load<Texture2D>("dot"), p.CollisionBox, Color.Red);
            }
            p.Draw(spriteBatch);
            p.Weapon.hit.ForEach(x => spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle((int)x.Position.X, (int)x.Position.Y, (int)x.Size.X, (int)x.Size.Y), Color.Green));
            p.Weapon.projectile.ForEach(x => spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle((int)x.Position.X, (int)x.Position.Y, (int)x.Size.X, (int)x.Size.Y), Color.Green));

            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * World.LastRoom[0], -40 + 10 * World.LastRoom[1] - World.UIBar, 9, 9), Color.BlueViolet);
            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * CurrentRoom.XCoordinate, -40 + 10 * CurrentRoom.YCoordinate - World.UIBar, 9, 9), Color.Red);

            }
            else
            {
                menu.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

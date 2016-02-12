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
        Texture2D stairway, drumsticks, electricGuitar, guitar, triangle, drumsticksOnGround, electricGuitarOnGround, guitarOnGround, triangleOnGround, note, hudTexture;
        int monsterCountOld;
        Vector2 weaponOnGroundPosition;
        KeyboardState oldState;
        MouseState ms;
        MouseState msOld;
        float winElapsed;
        bool winAni = false;
        Room CurrentRoom {
            get { return World.Rooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1]]; }
        }

        WeaponOnGround weaponOnGround;
        Weapon tmpWeapon;
        Menu menu;
        Player p;

        Rectangle weaponInHUD;
        Texture2D tmpWeaponForHUD;

        SoundEffect musicMenu;
        SoundEffect musicGame;

        SoundEffectInstance sEI;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = World.RoomWidth;
            graphics.PreferredBackBufferHeight = World.RoomHeight + World.HUD;
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
            monsters.Add(Content.Load<Texture2D>("Charger"));
            roomGraphic = new List<RoomGraphic>();
            objects = new List<Texture2D>();
            objects.Add(Content.Load<Texture2D>("Trummor"));
            objects.Add(Content.Load<Texture2D>("Keyboard"));
            objects.Add(Content.Load<Texture2D>("DJ_table"));
            objects.Add(Content.Load<Texture2D>("Desk"));
            objects.Add(Content.Load<Texture2D>("Broken_Guitar"));
            stairway = Content.Load<Texture2D>("stairway");
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("Background_Number_1_Pixelated"), Content.Load<Texture2D>("door"), Content.Load<Texture2D>("Background_Overlay_Number_1")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("Background_Number_2_Pixelated"), Content.Load<Texture2D>("door2"), Content.Load<Texture2D>("Background_Overlay_Number_2")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("Background_Number_3_Pixelated"), Content.Load<Texture2D>("Dörr_3"), Content.Load<Texture2D>("Background_Overlay_Number_3")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("Background_Number_4"), Content.Load<Texture2D>("Dörr_4"), Content.Load<Texture2D>("Background_Overlay_Number_4")));
            roomGraphic.Add(new RoomGraphic(Content.Load<Texture2D>("Background_Number_5"), Content.Load<Texture2D>("door2"), Content.Load<Texture2D>("Background_Overlay_Number_5")));
            hudTexture = Content.Load<Texture2D>("Game_UI");
            World.GenerateFloor();
            World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"), stairway);
            monsterCountOld = 0;
            weaponOnGroundPosition = new Vector2(World.RoomWidth / 2, World.RoomHeight / 2);
            drumsticks = Content.Load<Texture2D>("Trumpinnar");
            electricGuitar = Content.Load<Texture2D>("El_guitar");
            guitar = Content.Load<Texture2D>("Guitar");
            triangle = Content.Load<Texture2D>("Triangel");
            drumsticksOnGround = Content.Load<Texture2D>("Trumpinnar");
            electricGuitarOnGround = Content.Load<Texture2D>("El_guitar");
            guitarOnGround = Content.Load<Texture2D>("Guitar");
            triangleOnGround = Content.Load<Texture2D>("Heart");
            note = Content.Load<Texture2D>("Note");

            musicGame = Content.Load<SoundEffect>("HarshBckGrndSndTrack");
            musicMenu = Content.Load<SoundEffect>("LightBckGrndSndTrack");

            sEI = musicGame.CreateInstance();
            sEI.IsLooped = true;
            sEI.Play();

            menu = new Menu(Content.Load<Texture2D>("PLAY_button"), Content.Load<Texture2D>("PLAY_flash_button"), Content.Load<Texture2D>("EXIT_button"),
                Content.Load<Texture2D>("EXIT_flash_button"), Content.Load<Texture2D>("CREDIT_button"), Content.Load<Texture2D>("CREDIT_flash_button"),
                Content.Load<Texture2D>("main_menu_NO_buttons"), Content.Load<Texture2D>("Game_Credits"));
            p = new Player(Content.Load<Texture2D>("Character_sprite_v2"), Content.Load<Texture2D>("health"), new Vector2(544, 456), 3, 1, 19, 19, 100,
                new Weapon(0.3f, 1f, WeaponType.Guitar, guitar));
            


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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !(menu.menuType == MenuType.CreditMenu)) {
                this.Exit();
            }
            ms = Mouse.GetState();
            menu.Update(ms, msOld);

            if (menu.menuType == MenuType.CreditMenu && (Keyboard.GetState().GetPressedKeys().Length > 0)) {
                    menu.menuType = MenuType.StartMenu;
            }
            if (menu.menuType == MenuType.WinMenu) {
                winElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (winElapsed > 400)
                {
                    if (winAni)
                    {
                        winElapsed -= 400;
                        winAni = false;
                    }
                    else {
                        winElapsed -= 400;
                        winAni = true;
                    }
                }
            }

            #region game
            if (menu.menuType == MenuType.InGame)
            {
                
                
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    World.GenerateFloor();
                    World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"), stairway);
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

                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                p.Update(elapsed, Keyboard.GetState(), oldState, Mouse.GetState(), msOld);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && msOld.LeftButton == ButtonState.Released && p.Weapon.weaponType == WeaponType.Drumsticks)
                    CurrentRoom.Animations.Add(new TempObject(Content.Load<Texture2D>("Rings"), p.Position, 1, 4, 4, 100, 0));
                p.Position = p.Position.X < 50 ? p.OldPos : p.Position;
                p.Position = p.Position.X > World.RoomWidth - 50 ? p.OldPos : p.Position;
                p.Position = p.Position.Y < 60 ? p.OldPos : p.Position;
                p.Position = p.Position.Y > World.RoomHeight - 60 ? p.OldPos : p.Position;

                if (Keyboard.GetState().IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X) && CurrentRoom.WOP != null && CurrentRoom.Monsters.Count == 0)
                {
                    if (Vector2.Distance(p.Position, CurrentRoom.WOP.Position) < 100)
                    {
                        tmpWeapon = CurrentRoom.WOP.ContainedWeapon;
                        CurrentRoom.WOP = new WeaponOnGround(p.Weapon.weaponTexture, weaponOnGroundPosition, p.Weapon);


                        if (p.Weapon.weaponType == WeaponType.Drumsticks)
                        {
                            CurrentRoom.WOP.Texture = drumsticksOnGround;
                        }
                        else if (p.Weapon.weaponType == WeaponType.ElectricGuitar)
                        {
                            CurrentRoom.WOP.Texture = electricGuitarOnGround;
                        }
                        else if (p.Weapon.weaponType == WeaponType.Guitar)
                        {
                            CurrentRoom.WOP.Texture = guitarOnGround;
                        }
                        else if (p.Weapon.weaponType == WeaponType.Triangle)
                        {
                            CurrentRoom.WOP.Texture = triangleOnGround;
                        }
                        p.Weapon = tmpWeapon;
                    }
                }

                #region Collision

                
                if (!p.Dead)
                {
                    foreach (Zombie z in CurrentRoom.Monsters.Where(x => x is Zombie))
                    {
                    z.Update(elapsed, p.Position);
                        if (z.CollisionBox.Intersects(p.CollisionBox) && p.invTmr <= 0)
                        {
                        p.Health -= 1;
                        p.invTmr = 1.5f;
                    }
                }
                    foreach (Charger c in CurrentRoom.Monsters.Where(x => x is Charger))
                    {
                    c.Update(elapsed, p.Position);
                        if (c.CollisionBox.Intersects(p.CollisionBox) && p.invTmr <= 0)
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
                            sZ.SpitList[i].Collectable = true;
                        }
                            else if (sZ.SpitList[i].CollisionBox.Intersects(p.CollisionBox))
                            {
                            sZ.SpitList[i].Collectable = true;
                        }
                            for (int q = 0; q < p.Weapon.hit.Count; q++)
                            {
                                if (sZ.SpitList[i].CollisionBox.Intersects(p.Weapon.hit[q].CollisionBox))
                                {
                                sZ.SpitList[i].Collectable = true;
                            }
                        }
                    }

                }
                }
                if (!(World.CurrentRoomLocationCode[0] == World.LastRoom[0] && World.CurrentRoomLocationCode[1] == World.LastRoom[1]))
                {
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
                        foreach (Charger c in CurrentRoom.Monsters.Where(x => x is Charger))
                        {
                            if (c.CollisionBox.Intersects(item.CollisionBox))
                            {
                                c.Position = c.OldPos;
                                c.charging = false;
                                c.wait = 0;
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
                        for (int i = 0; i < p.Weapon.projectile.Count; i++)
                        {
                            if (
                                p.Weapon.projectile[i].HitCollisionBox.Intersects(item.CollisionBox))
                            {
                                p.Weapon.projectile[i].Collectable = true;
                            }
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
                            if (p.Weapon.weaponType == WeaponType.ElectricGuitar)
                                p.Weapon.projectile[q].Collectable = true;
                        }

                    }
                }
                #endregion
                CurrentRoom.Animations.ForEach(x => x.Update(elapsed));
                #region Garbage
                foreach (SpitZombie sZ in CurrentRoom.Monsters.Where(x => x is SpitZombie))
                {
                    for (int i = 0; i < sZ.SpitList.Count; i++)
                    {
                        if (sZ.SpitList[i].Collectable)
                        {
                            sZ.SpitList.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (p.Weapon.weaponType == WeaponType.Drumsticks)
                {
                    for (int i = 0; i < p.Weapon.projectile.Count; i++)
                    {
                        if (Vector2.Distance(p.Weapon.projectile[i].Position, p.Position) > 100)
                        {
                            p.Weapon.projectile[i].Collectable = true;
                        }
                    }
                }
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

                if (CurrentRoom.Monsters.Count == 0 && monsterCountOld != 0 && CurrentRoom.WOP == null)//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!
                {
                    if (Static.GetNumber(100) < 101)//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!
                    {
                        int tmp = Static.GetNumber(4);//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!
                        if (tmp == 0)       //NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!//NEED TO CHANGE DAMAGE RANGE ETC!!!
                        {
                            CurrentRoom.WOP = new WeaponOnGround(drumsticksOnGround, weaponOnGroundPosition, new Weapon(0.001f + 0.002f * World.CurrentLevel, 2f, WeaponType.Drumsticks, note));
                        }
                        if (tmp == 1)
                        {
                            CurrentRoom.WOP = new WeaponOnGround(electricGuitarOnGround, weaponOnGroundPosition, new Weapon(0.2f + 0.1f * World.CurrentLevel, 3f, WeaponType.ElectricGuitar, note));
                        }
                        if (tmp == 2)
                        {
                            CurrentRoom.WOP = new WeaponOnGround(guitarOnGround, weaponOnGroundPosition, new Weapon(0.3f + 0.2f * World.CurrentLevel, 1f, WeaponType.Guitar, guitar));
                        }
                        if (tmp == 3)
                        {
                            CurrentRoom.WOP = new WeaponOnGround(triangleOnGround, weaponOnGroundPosition, new Weapon(0.02f + 0.01f * World.CurrentLevel, 1f, WeaponType.Triangle, null));
                        }
                    }
                }
                monsterCountOld = CurrentRoom.Monsters.Count;
                Vector2 tempPos = p.Position;
                CurrentRoom.Doors.ForEach(d => p.Position = d.Update(elapsed, p.CollisionBox, p.Position, CurrentRoom.Monsters.Count));
                if (tempPos != p.Position)
                {
                    p.Weapon.hit.ForEach(x => x.Collectable = true);
                    p.Weapon.projectile.ForEach(x => x.Collectable = true);
                }

                if (World.CurrentRoomLocationCode[0] == World.LastRoom[0] && World.CurrentRoomLocationCode[1] == World.LastRoom[1])
                {
                    if (p.CollisionBox.Intersects(CurrentRoom.Props[0].CollisionBox))
                    {
                        if (World.CurrentLevel == 10)
                        {
                            menu.menuType = MenuType.WinMenu;
                        }
                        else {
                            World.CurrentLevel += 1;
                            World.GenerateFloor();
                            World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"), stairway);
                            p.Position = new Vector2(544, 306 + 150);
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Y) && p.Dead == true)
                {
                p.Health = 10;
                World.CurrentLevel = 1;
                World.GenerateFloor();
                World.GenerateRooms(roomGraphic, objects, monsters, Content.Load<Texture2D>("health"), stairway);
                p.Position = new Vector2(544, 306 + 150);
                CurrentRoom.Animations.Add(new TempObject(Content.Load<Texture2D>("SATAN"), p.Position, 1, 5, 5, 200, MathHelper.ToRadians(90)));
                p.Dead = false;
            }

            msOld = Mouse.GetState();
            oldState = Keyboard.GetState();
                if (p.Health <= 0)
                {
                p.Dead = true;
                    CurrentRoom.Monsters.Clear();
                }
            }
            else
            {
                
            }
#endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(0, World.HUD, 0));
            if (menu.menuType == MenuType.CreditMenu) {
                spriteBatch.Draw(hudTexture, new Vector2(0, -World.HUD), Color.White);
            }

            if (menu.menuType == MenuType.InGame) {
                CurrentRoom.Draw(spriteBatch);
                spriteBatch.Draw(hudTexture, new Vector2(0,-World.HUD), Color.White);
                
                CurrentRoom.Doors.Where(x => x.active).ToList().ForEach(x => x.Draw(spriteBatch));

                if (Keyboard.GetState().IsKeyDown(Keys.E)) {
                    CurrentRoom.Props.ForEach(x => spriteBatch.Draw(Content.Load<Texture2D>("dot"), x.CollisionBox, Color.Red));
                    foreach (Zombie z in CurrentRoom.Monsters.Where(x => x is Zombie)) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), z.CollisionBox, Color.Red);
                    }
                    spriteBatch.Draw(Content.Load<Texture2D>("dot"), p.CollisionBox, Color.Red);
                }
                if (CurrentRoom.WOP != null && CurrentRoom.Monsters.Count == 0) {
                    CurrentRoom.WOP.Draw(spriteBatch);
                }
                
                if (!p.Dead) {
                p.Draw(spriteBatch);
                }

                p.Weapon.hit.ForEach(x => spriteBatch.Draw(Content.Load<Texture2D>("Note"), x.HitCollisionBox, new Rectangle(0, 0, 52, 56), Color.White));
                
                for (int i = 0; i < 25; i++) {
                    for (int q = 0; q < 25; q++) {
                        if (World.ActiveRooms[i, q] == true) {
                            spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * i, -40 + 10 * q - World.HUD, 9, 9), Color.White);
                        }
                    }
                }
                spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * World.LastRoom[0], -40 + 10 * World.LastRoom[1] - World.HUD, 9, 9), Color.BlueViolet);
                spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * World.FirstRoom[0], -40 + 10 * World.FirstRoom[1] - World.HUD, 9, 9), Color.LawnGreen);
                spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(-40 + 10 * CurrentRoom.XCoordinate, -40 + 10 * CurrentRoom.YCoordinate - World.HUD, 9, 9), Color.Red);
                if (p.Dead) {
                    spriteBatch.Draw(Content.Load<Texture2D>("Continue"), new Vector2(0), Color.White);
                }



                switch (p.Weapon.weaponType) {
                    case WeaponType.Drumsticks:
                        spriteBatch.Draw(drumsticks, new Rectangle(World.RoomWidth - 400, -World.HUD + 20, drumsticks.Width, drumsticks.Height), Color.White);
                        break;
                    case WeaponType.ElectricGuitar:
                        spriteBatch.Draw(electricGuitar, new Rectangle(World.RoomWidth - 400, -World.HUD + 20, electricGuitar.Width, electricGuitar.Height), Color.White);
                        break;
                    case WeaponType.Guitar:
                        spriteBatch.Draw(guitar, new Rectangle(World.RoomWidth - 400, -World.HUD + 20, guitar.Width, guitar.Height), Color.White);
                        break;
                    case WeaponType.Triangle:
                        spriteBatch.Draw(triangle, new Rectangle(World.RoomWidth - 400, -World.HUD + 20, triangle.Width, triangle.Height), Color.White);
                        break;
                    default:
                        break;
                }
                for (int i = 0; i < p.Weapon.damage; i++) {
                    spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(World.RoomWidth - 500 + 20 * i, -World.HUD + 20, 20, 20), Color.Red);
                }
                for (int i = 0; i < p.Weapon.range; i++) {
                    spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(World.RoomWidth - 500 + 20 * i, -World.HUD + 50, 20, 20), Color.Red);
                }
            }
            if (CurrentRoom.WOP != null) {
                if (Vector2.Distance(p.Position, CurrentRoom.WOP.Position) < 100) {
                    switch (CurrentRoom.WOP.ContainedWeapon.weaponType) {
                        case WeaponType.Drumsticks:
                            spriteBatch.Draw(drumsticks, new Rectangle(World.RoomWidth - 100, -World.HUD + 20, drumsticks.Width, drumsticks.Height), Color.White);
                            break;
                        case WeaponType.ElectricGuitar:
                            spriteBatch.Draw(electricGuitar, new Rectangle(World.RoomWidth - 100, -World.HUD + 20, electricGuitar.Width, electricGuitar.Height), Color.White);
                            break;
                        case WeaponType.Guitar:
                            spriteBatch.Draw(guitar, new Rectangle(World.RoomWidth - 100, -World.HUD + 20, guitar.Width, guitar.Height), Color.White);
                            break;
                        case WeaponType.Triangle:
                            spriteBatch.Draw(triangle, new Rectangle(World.RoomWidth - 100, -World.HUD + 20, triangle.Width, triangle.Height), Color.White);
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < CurrentRoom.WOP.ContainedWeapon.damage; i++) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(World.RoomWidth - 200 + 20 * i, -World.HUD + 20, 20, 20), Color.Red);
                    }
                    for (int i = 0; i < CurrentRoom.WOP.ContainedWeapon.range; i++) {
                        spriteBatch.Draw(Content.Load<Texture2D>("dot"), new Rectangle(World.RoomWidth - 200 + 20 * i, -World.HUD + 50, 20, 20), Color.Red);
            }
                }
            }



            else if (menu.menuType == MenuType.WinMenu) {
                if (winAni) {
                    spriteBatch.Draw(Content.Load<Texture2D>("Endingscene_KLAR_Bild_1"), new Vector2(0), Color.White);
                }
                if (!winAni) {
                    spriteBatch.Draw(Content.Load<Texture2D>("Endingscene_KLAR_Bild_2"), new Vector2(0), Color.White);
                }
            }
            else if(menu.menuType == MenuType.CreditMenu) {
                spriteBatch.Draw(Content.Load<Texture2D>("Game_Credits"), new Vector2(0), Color.White);
            }

            else {
                menu.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

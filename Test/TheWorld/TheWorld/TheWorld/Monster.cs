using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld {
    class Monster : GameObject {
        public float Health { get; set; }
        protected Texture2D Heart;
        public Monster(Texture2D texture, Texture2D heartTexture, Vector2 position, int health, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed) {
            Health = health;
            Heart = heartTexture;
        }
        public void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
        }
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            for (int i = 0; i < Health * 2; i++) {
                if (i % 4 == 0) {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 20 + i * 5, (int)Position.Y - 50, 5, 20), new Rectangle(0, 0, 7, 30), Color.White);
                }
                else if (i % 4 == 1) {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 20 + i * 5, (int)Position.Y - 50, 5, 20), new Rectangle(7, 0, 8, 30), Color.White);
                }
                else if (i % 4 == 2) {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 20 + i * 5, (int)Position.Y - 50, 5, 20), new Rectangle(15, 0, 7, 30), Color.White);
                }
                else if (i % 4 == 3) {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 20 + i * 5, (int)Position.Y - 50, 5, 20), new Rectangle(22, 0, 8, 30), Color.White);
                }
            }
        }
    }

    class Zombie : Monster {
        public Zombie(Texture2D texture, Texture2D heartTexture, Vector2 position, int health, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, heartTexture, position, health, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
        }

        new public void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
            Vector2 direction = playerPos - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();
            Rotation = (float)Math.Atan2(direction.Y, direction.X);

            OldPos = Position;
            Position += direction * speed;

        }


    }

    class Spit : GameObject {
        protected Vector2 Direction { get; set; }
        public Spit(Texture2D texture, Vector2 position, Vector2 direction) : base(texture, position) {
            Texture = texture;
            Position = position;
            Direction = direction;
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public void Update() {
            Position += Direction * 3;

        }
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation + (float)(Math.PI * 0.5f), new Vector2(Texture.Width / 2, Texture.Height / 2), 1f, SpriteEffects.None, 0);
        }
    }

    class SpitZombie : Monster {

        protected Vector2 direction;
        protected Vector2 randomPos;
        public List<Spit> SpitList { get; set; }
        protected Texture2D spitTexture;

        protected float spitElapsed;
        public bool facingTowards;
        public SpitZombie(Texture2D texture, Texture2D heartTexture, Vector2 position, Texture2D spitTexture, int health, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, heartTexture, position, health, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
            SpitList = new List<Spit>();
            direction = new Vector2(Static.GetNumber(World.RoomWidth), Static.GetNumber(World.RoomHeight));
            if (direction != Vector2.Zero)
                direction.Normalize();
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
            this.spitTexture = spitTexture;
        }

        public void Update(float elapsed, Vector2 playerPos, List<GameObject> props) {
            base.Update(elapsed);
            spitElapsed += elapsed;
            if (spitElapsed > 1000) {
                if (Vector2.Distance(playerPos, Position) > 200) {
                    direction = playerPos - Position;
                    if (direction != Vector2.Zero)
                        direction.Normalize();
                    SpitList.Add(new Spit(spitTexture, Position, direction));
                    Rotation = (float)Math.Atan2(direction.Y, direction.X);
                    facingTowards = true;
                }
                else if (facingTowards) {
                    do {
                        randomPos = new Vector2(Static.GetNumber(68, World.RoomWidth - 68), Static.GetNumber(68, World.RoomHeight - 68));
                    } while (Vector2.Distance(randomPos, playerPos) < 200);
                    direction = randomPos - Position;
                    if (direction != Vector2.Zero)
                        direction.Normalize();
                    Rotation = (float)Math.Atan2(direction.Y, direction.X);
                    facingTowards = false;
                }
                spitElapsed -= 1000;
            }

            if (!facingTowards && !(Position.X > World.RoomWidth - 68) && !(Position.Y > World.RoomHeight - 68) && !(Position.X < 68) && !(Position.Y < 68)) {
                OldPos = Position;
                Position += direction * speed;
            }
            else {
                Position = OldPos;
                facingTowards = true;
            }
            SpitList.ForEach(x => x.Update());
            foreach (var p in props) {
                for (int i = 0; i < SpitList.Count; i++) {
                    if (SpitList[i].CollisionBox.Intersects(p.CollisionBox)) {
                        SpitList.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            SpitList.ForEach(x => x.Draw(spriteBatch));
        }
    }

    class Charger : Monster {

        protected Vector2 direction;
        public bool charging;
        public float wait;
        public Charger(Texture2D texture, Texture2D heartTexture, Vector2 position, int health, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, heartTexture, position, health, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
            Rotation = 0;
        }

        public new void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
            wait += elapsed;
            if (charging) {
                OldPos = Position;
                Position += direction * speed;
            }
            else if(!charging && wait > 2000){
                direction = playerPos - Position;
                if (direction != Vector2.Zero)
                    direction.Normalize();
                Rotation = (float)Math.Atan2(direction.Y, direction.X);
                charging = true;
            }
            if ((Position.X > World.RoomWidth - 68) || (Position.Y > World.RoomHeight - 68) || (Position.X < 68) || (Position.Y < 68)) {
                wait = 0;
                charging = false;
                Position = OldPos;
            }
        }
    }


}
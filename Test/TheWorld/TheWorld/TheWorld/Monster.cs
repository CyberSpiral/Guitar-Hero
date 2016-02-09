using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld {
    class Monster : GameObject {
        public int Health { get; set; }
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
            for (int i = 0; i < Health; i++) {
                if (i % 2 == 0) {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 20 + i * 10, (int)Position.Y - 50, 10, 20), new Rectangle(0, 0, 15, 30), Color.Red);
                }
                else {
                    spriteBatch.Draw(Heart, new Rectangle((int)Position.X - 22 + i * 10, (int)Position.Y - 50, 10, 20), new Rectangle(15, 0, 15, 30), Color.Red);
                }
            }
        }
    }

    class Zombie : Monster {
        public new Rectangle CollisionBox {
            get { return new Rectangle((int)Position.X - (Texture.Width / Columns / 2), (int)Position.Y - (Texture.Height / Rows / 2), Texture.Width / Columns, Texture.Height - 28); }
            set { value = CollisionBox; }
        }
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
            rotation = (float)Math.Atan2(direction.Y, direction.X);

            OldPos = Position;
            Position += direction * speed;

        }


    }

    class SpitZombie : Monster {

        protected Vector2 direction;
        protected Vector2 randomPos;

        protected float spitElapsed;
        public bool facingTowards;
        public SpitZombie(Texture2D texture, Texture2D heartTexture, Vector2 position, int health, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, heartTexture, position, health, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;

            direction = new Vector2(Static.GetNumber(World.RoomWidth), Static.GetNumber(World.RoomHeight));
            if (direction != Vector2.Zero)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);

        }

        new public void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
            spitElapsed += elapsed;
            if (spitElapsed > 1000) {
                if (Vector2.Distance(playerPos, Position) > 400) {
                    direction = playerPos - Position;
                    if (direction != Vector2.Zero)
                        direction.Normalize();
                    rotation = (float)Math.Atan2(direction.Y, direction.X);
                    facingTowards = true;
                }
                else if (facingTowards) {
                    do {
                        randomPos = new Vector2(Static.GetNumber(68, World.RoomWidth - 68), Static.GetNumber(68, World.RoomHeight - 68));
                    } while (Vector2.Distance(randomPos, playerPos) < 400);
                    direction = randomPos - Position;
                    if (direction != Vector2.Zero)
                        direction.Normalize();
                    rotation = (float)Math.Atan2(direction.Y, direction.X);
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

        }
    }


}
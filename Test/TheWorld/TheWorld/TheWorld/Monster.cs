using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld {
    class Zombie : GameObject {
        public Zombie(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
        }

        public void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
            Vector2 direction = playerPos - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);

            OldPos = Position;
            Position += direction * speed;

        }


    }
    class SpitZombie : GameObject {

        protected Vector2 direction;
        protected Vector2 randomPos;

        protected float spitElapsed;
        protected bool facingTowards;
        public SpitZombie(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;

            Random r = new Random();
            direction = new Vector2(r.Next(World.RoomWidth), r.Next(World.RoomHeight));
            if (direction != Vector2.Zero)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);

        }

        public void Update(float elapsed, Vector2 playerPos) {
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
                    Random r = new Random();
                    do {
                        randomPos = new Vector2(r.Next(World.RoomWidth), r.Next(World.RoomHeight));
                    } while (Vector2.Distance(randomPos, playerPos) < 400);
                    direction = randomPos - Position;
                    if (direction != Vector2.Zero)
                        direction.Normalize();
                    rotation = (float)Math.Atan2(direction.Y, direction.X);
                    facingTowards = false;
                }
                spitElapsed -= 1000;
            }

            if (!facingTowards && !(Position.X > World.RoomWidth) && !(Position.Y > World.RoomHeight) && !(Position.X < 0) && !(Position.Y < 0)) {
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
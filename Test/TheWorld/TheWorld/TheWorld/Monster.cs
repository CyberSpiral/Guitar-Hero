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
        protected float spitElapsed;
        public SpitZombie(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns,
            int totalFrames, int animationSpeed)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
        }

        public void Update(float elapsed, Vector2 playerPos) {
            base.Update(elapsed);
            spitElapsed += elapsed;
            if (spitElapsed > 100) {
                Vector2 direction = playerPos - Position;
                if (direction != Vector2.Zero)
                    direction.Normalize();
                rotation = (float)Math.Atan2(direction.Y, direction.X);

                Random r = new Random();
                direction = new Vector2(r.Next(World.RoomWidth), r.Next(World.RoomHeight))

                OldPos = Position;
                Position += direction * speed;
            }
        }


    }
}

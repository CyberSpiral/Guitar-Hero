using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame {
    class Monster : GameObject {
        public Monster(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns, int totalFrames)
            : base(texture, position, textureRows, textureColumns, totalFrames) {
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
}

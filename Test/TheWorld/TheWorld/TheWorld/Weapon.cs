using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class Weapon {
        public List<WeaponHit> hit;
        protected float totalElapsed;
        public Weapon() {
            hit = new List<WeaponHit>();
        }
        public virtual void Update(float elapsed) {
            totalElapsed += elapsed;
            if (totalElapsed > 1) {
                hit.Clear();
                totalElapsed -= 1;
            }
        }

        public virtual void Execute(Vector2 rotation, Vector2 playerPosition) {
            hit.Add(new WeaponHit(playerPosition - new Vector2(17, 17) + (rotation * 50), new Vector2(40, 40)));
        }

    }
    class WeaponHit : GameObject {
        public Vector2 Size { get; set; }
        public Rectangle HitCollisionBox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
            set { HitCollisionBox = value; }
        }
        public WeaponHit(Vector2 position, Vector2 size) : base(null, position) {
            Position = position;
            Size = size;
        }
    }
    class WeaponOnGround : GameObject {
        public Weapon ContainedWeapon { get; protected set; }
        public bool existing;
        public WeaponOnGround(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames
            , Weapon containedWeapon, int animationSpeed)
            : base(texture, position, rows, columns, totalFrames, animationSpeed) {
            ContainedWeapon = containedWeapon;
            existing = true;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 playerPosition) {
            if (Vector2.Distance(playerPosition, Position) < 70) {
                spriteBatch.DrawString(spriteFont, "X", new Vector2(0, 0), Color.White);
            }
        }
    }
}
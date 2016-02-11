using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    enum WeaponType {
        Drumsticks, Trumpet, ElectricGuitar, Guitar, Triangle, Keyboard
    }
    class Weapon {

        public int damage;
        public float knockback;
        public float range;
        public WeaponType weaponType { get; set; }
        protected Texture2D weaponTexture;

        public List<WeaponHit> hit;
        protected float totalElapsed;
        public Weapon(int damage, float range, WeaponType weaponType, Texture2D texture) {
            hit = new List<WeaponHit>();
            this.damage = damage;
            this.range = range;
            this.weaponType = weaponType;
            weaponTexture = texture;
        }
        public virtual void Update(float elapsed) {
            totalElapsed += elapsed;
            if (totalElapsed > 1) {
                hit.Clear();
                totalElapsed -= 1;
            }
        }

        public virtual void Execute(Vector2 rotation, Vector2 playerPosition) {
            hit.Add(new WeaponHit(playerPosition - new Vector2(17, 17) + (rotation * 50), new Vector2(40, 40), weaponTexture));
        }

    }
    class WeaponHit : GameObject {
        public Vector2 Size { get; set; }
        public Rectangle HitCollisionBox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
            set { HitCollisionBox = value; }
        }
        public WeaponHit(Vector2 position, Vector2 size, Texture2D texture) : base(texture, position) {
            Position = position;
            Size = size;
        }
    }
    class WeaponProjectile : GameObject {
        public Vector2 Size { get; set; }
        public Vector2 Rotation { get; set; }
        public Rectangle HitCollisionBox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
            set { HitCollisionBox = value; }
        }
        public WeaponProjectile(Vector2 position, Vector2 size, Texture2D texture, Vector2 rotation) : base(texture, position) {
            Position = position;
            Size = size;
        }
        public void Update() {
        }
    }
    class WeaponOnGround : GameObject {
        public Weapon ContainedWeapon { get; protected set; }
        public bool existing;
        public Texture2D playerTexture;
        public WeaponOnGround(Texture2D texture, Vector2 position, Weapon containedWeapon,
            Texture2D playerTexture) : base(texture, position) {

            ContainedWeapon = containedWeapon;
            existing = true;
            this.playerTexture = playerTexture;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 playerPosition) {
            if (Vector2.Distance(playerPosition, Position) < 70) {
                spriteBatch.DrawString(spriteFont, "X", new Vector2(Position.X, Position.Y), Color.White);
            }
        }
    }
}
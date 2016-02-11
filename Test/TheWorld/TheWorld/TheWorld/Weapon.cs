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

        public float damage;
        public float knockback;
        public float range;
        public WeaponType weaponType { get; set; }
        protected Texture2D weaponTexture;

        public List<WeaponHit> hit;
        protected float totalElapsed;
        public Weapon(float damage, float range, WeaponType weaponType, Texture2D texture) {
            hit = new List<WeaponHit>();
            this.damage = damage;
            this.range = range;
            this.weaponType = weaponType;
            weaponTexture = texture;
        }
        public virtual void Update(float elapsed) {
            switch (weaponType) {
                case WeaponType.Drumsticks:
                    break;
                case WeaponType.Trumpet:
                    break;
                case WeaponType.ElectricGuitar:
                    break;
                case WeaponType.Guitar: {
                        totalElapsed += elapsed;
                        if (totalElapsed > 1) {
                            hit.Clear();
                            totalElapsed -= 1;
                        }
                        break;
                    }
                case WeaponType.Triangle: {
                        totalElapsed += elapsed;
                        if (totalElapsed > 1) {
                            hit.Clear();
                            totalElapsed -= 1;
                        }
                        break;
                    }
                case WeaponType.Keyboard:
                    break;
                default:
                    break;
            }
        }

        public virtual void Execute(Vector2 rotation, Vector2 playerPosition) {
            switch (weaponType) {
                case WeaponType.Drumsticks:
                    break;
                case WeaponType.Trumpet:
                    break;
                case WeaponType.ElectricGuitar:
                    break;
                case WeaponType.Guitar:
                    hit.Add(new WeaponHit(playerPosition - new Vector2(17, 17) + (rotation * 50), new Vector2(40, 40), weaponTexture));
                    break;
                case WeaponType.Triangle:
                    hit.Add(new WeaponHit(new Vector2(0, 0), new Vector2(World.RoomWidth, World.RoomHeight), weaponTexture));
                    break;
                case WeaponType.Keyboard:
                    break;
                default:
                    break;
            }
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
        public WeaponProjectile(Vector2 position, Vector2 size, Texture2D texture, Vector2 rotation, float speed) : base(texture, position) {
            Size = size;
            Rotation = rotation;
        }
        public void Update() {
            Position = Rotation * speed;
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
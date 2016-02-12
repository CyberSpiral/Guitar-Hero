using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld
{
    enum WeaponType
    {
        Drumsticks, ElectricGuitar, Guitar, Triangle
    }
    class Weapon
    {

        public float damage;
        public float knockback;
        public float range;
        public WeaponType weaponType { get; set; }
        public Texture2D weaponTexture;

        public List<WeaponHit> hit;
        public List<WeaponProjectile> projectile;
        protected float totalElapsed;
        private float totalElapsedAni;
        private float animationSpeed = 100;
        private int currentFrame = 0;
        private int totalFrames = 2;

        public Weapon(float damage, float range, WeaponType weaponType, Texture2D texture)
        {
            hit = new List<WeaponHit>();
            projectile = new List<WeaponProjectile>();
            this.damage = damage;
            this.range = range;
            this.weaponType = weaponType;
            weaponTexture = texture;
        }

        public void Update(float elapsed)
        {
            switch (weaponType)
            {
                case WeaponType.Drumsticks:
                    totalElapsed += elapsed;
                    projectile.ForEach(x => x.Update());
                    if (totalElapsed > 2000)
                    {
                        projectile.Clear();
                        totalElapsed -= 2000;
                    }
                    break;
                case WeaponType.ElectricGuitar:
                    projectile.ForEach(x => x.Update());
                    totalElapsedAni += elapsed;
                    {
                        if (totalElapsedAni > animationSpeed)
                        {
                            currentFrame++;
                            if (currentFrame == totalFrames)
                                currentFrame = 0;
                            totalElapsedAni = 0;
                        }
                    }
                    break;
                case WeaponType.Guitar:
                    {
                        totalElapsed += elapsed;
                        if (totalElapsed > 1)
                        {
                            hit.Clear();
                            totalElapsed -= 1;
                        }
                        break;
                    }
                case WeaponType.Triangle:
                    {
                        totalElapsed += elapsed;
                        if (totalElapsed > 1)
                        {
                            hit.Clear();
                            totalElapsed -= 1;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public void Execute(Vector2 rotation, Vector2 playerPosition)
        {
            switch (weaponType)
            {
                case WeaponType.Drumsticks:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i % 2 == 0)
                            projectile.Add(new WeaponProjectile(playerPosition - new Vector2(37 / 3, 56 / 3), new Vector2(10, 40), weaponTexture
                                , (new Vector2((float)Math.Cos(MathHelper.ToRadians(90 * i)), (float)Math.Sin(MathHelper.ToRadians(90 * i)))), range));
                        else
                            projectile.Add(new WeaponProjectile(playerPosition - new Vector2(37 / 3, 56 / 3), new Vector2(40, 10), weaponTexture
                            , (new Vector2((float)Math.Cos(MathHelper.ToRadians(90 * i)), (float)Math.Sin(MathHelper.ToRadians(90 * i)))), range));
                    }
                    break;
                case WeaponType.ElectricGuitar:
                    projectile.Add(new WeaponProjectile(playerPosition - new Vector2(17) + (rotation * 20), new Vector2(30), weaponTexture, rotation, range));
                    break;
                case WeaponType.Guitar:
                    hit.Add(new WeaponHit(playerPosition - new Vector2(17) + (rotation * 50), new Vector2(weaponTexture.Width,weaponTexture.Height), weaponTexture));
                    break;
                case WeaponType.Triangle:
                    hit.Add(new WeaponHit(new Vector2(0), new Vector2(World.RoomWidth, World.RoomHeight), weaponTexture));
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (weaponType)
            {
                case WeaponType.Drumsticks:
                    break;
                case WeaponType.ElectricGuitar:
                    foreach (var x in projectile)
                    {
                        int width = x.Texture.Width / 2;
                        int height = x.Texture.Height / 1;
                        int row = currentFrame / 2;
                        int column = currentFrame % 2;

                        Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                        Rectangle destinationRectangle = new Rectangle((int)x.Position.X, (int)x.Position.Y, (int)x.Size.X, (int)x.Size.X);

                        spriteBatch.Draw(x.Texture, destinationRectangle, sourceRectangle, Color.White, x.Rotation, new Vector2(0), SpriteEffects.None, 0);
                    }
                    break;
                case WeaponType.Guitar:
                    break;
                case WeaponType.Triangle:
                    break;
                default:
                    break;
            }
        }

    }
    class WeaponHit : GameObject
    {
        public Vector2 Size { get; set; }
        public Rectangle HitCollisionBox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
            set { HitCollisionBox = value; }
        }
        public WeaponHit(Vector2 position, Vector2 size, Texture2D texture) : base(texture, position)
        {
            Position = position;
            Size = size;
        }
    }
    class WeaponProjectile : GameObject
    {
        public Vector2 Size { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public Rectangle HitCollisionBox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
            set { HitCollisionBox = value; }
        }
        public WeaponProjectile(Vector2 position, Vector2 size, Texture2D texture, Vector2 direction, float speed) : base(texture, position)
        {
            Size = size;
            Direction = direction;
            this.speed = speed;
            if (Direction != null)
                Direction.Normalize();
            Rotation = (float)Math.Atan2(Direction.X, Direction.Y);
        }
        public void Update()
        {
            Position += Direction * speed;
        }
    }
    class WeaponOnGround : GameObject
    {
        public Weapon ContainedWeapon { get; protected set; }
        public bool existing;
        public WeaponOnGround(Texture2D texture, Vector2 position, Weapon containedWeapon) : base(texture, position)
        {

            ContainedWeapon = containedWeapon;
            existing = true;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 playerPosition)
        {
            if (Vector2.Distance(playerPosition, Position) < 70)
            {
                spriteBatch.DrawString(spriteFont, "X", new Vector2(Position.X, Position.Y), Color.White);
            }
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height), Color.White);
        }
    }
}
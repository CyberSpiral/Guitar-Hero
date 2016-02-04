using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld
{
    class Weapon
    {
        public Weapon()
        {

        }

        public virtual void Execute(Vector2 rotation, Vector2 playerPosition)
        {

        }

    }
    class WeaponOnGround : GameObject
    {
        public Weapon ContainedWeapon { get; protected set; }
        public bool existing;
        public WeaponOnGround(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames
            , Weapon containedWeapon, int animationSpeed)
            : base(texture, position, rows, columns, totalFrames, animationSpeed)
        {
            ContainedWeapon = containedWeapon;
            existing = true;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 playerPosition)
        {
            if (Vector2.Distance(playerPosition, Position) < 70)
            {
                spriteBatch.DrawString(spriteFont, "X", new Vector2(0, 0), Color.White);
            }
        }
    }
}

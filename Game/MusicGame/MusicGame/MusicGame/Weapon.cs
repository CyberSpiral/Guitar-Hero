using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicGame {
    class Weapon {
        public Weapon() {

        }

        public virtual void Execute(Vector2 rotation, Vector2 playerPosition) {

        }

    }
    class WeaponOnGround : GameObject {
        public Weapon ContainedWeapon { get; protected set; }
        public WeaponOnGround(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, Weapon containedWeapon)
            : base(texture, position, rows, columns, totalFrames) {
            ContainedWeapon = containedWeapon;
        }
    }
}

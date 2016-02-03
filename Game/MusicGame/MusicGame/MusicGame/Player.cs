using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame {
    class Player : GameObject {
        public Weapon weapon { get; protected set; }
        public Player(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns, int totalFrames) 
            : base(texture, position, textureRows, textureColumns, totalFrames) {
            this.speed = speed;
        }
        public void Update(float elapsed, KeyboardState currentKey, KeyboardState oldKey, MouseState mouse) {
            base.Update(elapsed);
            Vector2 direction = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            
            #region CurrentMovement
            //Can be changed
            if (currentKey.IsKeyDown(Keys.W))
                Velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            if (currentKey.IsKeyDown(Keys.S))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(180)), (float)Math.Sin(rotation + MathHelper.ToRadians(180)));
            if (currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(90)), (float)Math.Sin(rotation + MathHelper.ToRadians(90)));
            if (currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(-90)), (float)Math.Sin(rotation + MathHelper.ToRadians(-90)));


            if (currentKey.IsKeyDown(Keys.W) && currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(45)), (float)Math.Sin(rotation + MathHelper.ToRadians(45)));
            if (currentKey.IsKeyDown(Keys.W) && currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(-45)), (float)Math.Sin(rotation + MathHelper.ToRadians(-45)));
            if (currentKey.IsKeyDown(Keys.S) && currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(135)), (float)Math.Sin(rotation + MathHelper.ToRadians(135)));
            if (currentKey.IsKeyDown(Keys.S) && currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(rotation + MathHelper.ToRadians(-135)), (float)Math.Sin(rotation + MathHelper.ToRadians(-135)));

            if (currentKey.IsKeyUp(Keys.W) && currentKey.IsKeyUp(Keys.A) && currentKey.IsKeyUp(Keys.S) && currentKey.IsKeyUp(Keys.D))
                Velocity = Vector2.Zero;

            if (Velocity != null)
                Velocity.Normalize();

            OldPos = Position;
            Position += Velocity * speed;
            #endregion
        }


    }

}
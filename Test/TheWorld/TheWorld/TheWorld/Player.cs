using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld
{
    class Player : GameObject
    {
        public Weapon Weapon { get; set; }
        protected int storedAnimationSpeed;
        public Player(Texture2D texture, Vector2 position, float speed, int textureRows, int textureColumns
            , int totalFrames, int animationSpeed)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed)
        {
            this.speed = speed;
            storedAnimationSpeed = animationSpeed;
            Weapon = new Weapon();
        }
        public void Update(float elapsed, KeyboardState currentKey, KeyboardState oldKey, MouseState mouse)
        {
            base.Update(elapsed);
            Vector2 direction = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);

            if (currentKey.IsKeyDown(Keys.Space) && oldKey.IsKeyUp(Keys.Space)) {
                Weapon.Execute(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)), Position);
            }   
            Weapon.Update(elapsed);

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
            {
                animationSpeed = int.MaxValue;
                Velocity = Vector2.Zero;
            }
            else {
                animationSpeed = storedAnimationSpeed;
            }

            if (Velocity != null)
                Velocity.Normalize();

            OldPos = Position;
            Position += Velocity * speed;
            #endregion
        }
    }
}
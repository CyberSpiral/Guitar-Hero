using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld {
    class Player : GameObject {
        public Weapon Weapon { get; set; }
        public int Health { get; set; }
        public float invTmr { get; set; }
        public bool Dead { get; set; }
        protected Texture2D Heart;

        protected int storedAnimationSpeed;

        public Player(Texture2D texture, Texture2D heartTexture, Vector2 position, float speed, int textureRows, int textureColumns
            , int totalFrames, int animationSpeed, Weapon weapon)
            : base(texture, position, textureRows, textureColumns, totalFrames, animationSpeed) {
            this.speed = speed;
            storedAnimationSpeed = animationSpeed;
            Health = 1;
            Heart = heartTexture;
            Weapon = weapon;
        }
        public void Update(float elapsed, KeyboardState currentKey, KeyboardState oldKey, MouseState currentMouse, MouseState oldMouse) {
            totalElapsed += elapsed;
            if (animated) {
                if (totalElapsed > animationSpeed) {
                    currentFrame++;
                    if (currentFrame == totalFrames - 2)
                        currentFrame = 0;
                    totalElapsed = 0;
                }
            }
            Vector2 direction = new Vector2(Mouse.GetState().X, Mouse.GetState().Y - World.HUD) - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();
            Rotation = (float)Math.Atan2(direction.Y, direction.X);

            Weapon.Update(elapsed);
            if (currentMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released) {
                Weapon.Execute(new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)), Position);
            }
            if (invTmr > 0) {
                invTmr -= elapsed / 1000;
            }

            #region CurrentMovement
            //Can be changed
            if (currentKey.IsKeyDown(Keys.W))
                Velocity = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            if (currentKey.IsKeyDown(Keys.S))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(180)), (float)Math.Sin(Rotation + MathHelper.ToRadians(180)));
            if (currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(90)), (float)Math.Sin(Rotation + MathHelper.ToRadians(90)));
            if (currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(-90)), (float)Math.Sin(Rotation + MathHelper.ToRadians(-90)));


            if (currentKey.IsKeyDown(Keys.W) && currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(45)), (float)Math.Sin(Rotation + MathHelper.ToRadians(45)));
            if (currentKey.IsKeyDown(Keys.W) && currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(-45)), (float)Math.Sin(Rotation + MathHelper.ToRadians(-45)));
            if (currentKey.IsKeyDown(Keys.S) && currentKey.IsKeyDown(Keys.D))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(135)), (float)Math.Sin(Rotation + MathHelper.ToRadians(135)));
            if (currentKey.IsKeyDown(Keys.S) && currentKey.IsKeyDown(Keys.A))
                Velocity = new Vector2((float)Math.Cos(Rotation + MathHelper.ToRadians(-135)), (float)Math.Sin(Rotation + MathHelper.ToRadians(-135)));

            if (currentKey.IsKeyUp(Keys.W) && currentKey.IsKeyUp(Keys.A) && currentKey.IsKeyUp(Keys.S) && currentKey.IsKeyUp(Keys.D)) {
                Velocity = Vector2.Zero;
                currentFrame = totalFrames - 1;
            }
            else if (currentFrame == totalFrames - 1)
                currentFrame = 0;
            animationSpeed = currentKey.IsKeyUp(Keys.W) && currentKey.IsKeyUp(Keys.A) && currentKey.IsKeyUp(Keys.S) && currentKey.IsKeyUp(Keys.D)
                ? int.MaxValue : storedAnimationSpeed;

            if (Velocity != null)
                Velocity.Normalize();

            OldPos = Position;
            Position += Velocity * speed;
            #endregion
        }
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            Weapon.Draw(spriteBatch);
            for (int i = 0; i < Health; i++) {
                if (i % 2 == 0) {
                    spriteBatch.Draw(Heart, new Rectangle(240 + i * 12 * 2, 10 - World.HUD, 20, 40), new Rectangle(0, 0, 15, 30), Color.White);
                }
                else {
                    spriteBatch.Draw(Heart, new Rectangle(236 + i * 12 * 2, 10 - World.HUD, 20, 40), new Rectangle(15, 0, 15, 30), Color.White);
                }
            }
        }
    }
}
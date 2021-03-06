﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame {
    class GameObject {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; protected set; }
        public Vector2 OldPos { get; set; }
        public Rectangle CollisionBox {
            get { return new Rectangle((int)Position.X - (Texture.Width / Columns / 2), (int)Position.Y - (Texture.Height / Rows / 2), Texture.Width / Columns, Texture.Height / Rows); }
            private set { value = CollisionBox; }
        }

        public int Rows { get; protected set; }
        public int Columns { get; protected set; }

        protected int currentFrame;
        protected int totalFrames;
        protected float totalElapsed;
        protected int animationSpeed;

        protected float rotation;
        protected float speed;

        public GameObject(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, int animationSpeed) {
            Texture = texture;
            Position = position;
            totalElapsed = 0;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            this.animationSpeed = animationSpeed;
        }
        public void Update(float elapsed) {
            totalElapsed += elapsed;
            if (totalElapsed > animationSpeed) {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                totalElapsed -= animationSpeed;
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, rotation - (float)(Math.PI * 0.5f), new Vector2((width / 2), (height / 2)), SpriteEffects.None, 0);
        }
        public virtual void ChangeTexture(Texture2D texture, int rows, int columns, int totalFrames) {
            Texture = texture;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

    }
    class Door : GameObject {
        public Door(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, int animationSpeed) : base(texture, position, rows, columns, totalFrames, animationSpeed) {

        }
    }
}
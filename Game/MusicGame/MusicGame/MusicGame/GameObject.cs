using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MusicGame
{
    class GameObject
    {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; }
        public Vector2 Velocity { get; }
       
        public int Rows { get; protected set; }
        public int Columns { get; protected set; }

        protected int currentFrame;
        protected int totalFrames;
        protected float totalElapsed;

        public GameObject(Texture2D texture, Vector2 position, int rows, int columns)
        {
            Texture = texture;
            Position = position;
            totalElapsed = 0;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
        public GameObject(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames)
        {
            Texture = texture;
            Position = position;
            totalElapsed = 0;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }
        public void Update(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > 50)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                totalElapsed -= 50;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void ChangeTexture(Texture2D texture, int rows, int columns, int totalFrames)
        {
            Texture = texture;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

    }
}

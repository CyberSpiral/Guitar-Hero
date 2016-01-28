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
        public List<Texture2D> Textures { get; }
        public Vector2 Position { get; }
        public Vector2 Velocity { get; }

        protected int currentTexture;
        protected float totalElapsed;

        public GameObject(List<Texture2D> textures, Vector2 position)
        {
            Textures = textures;
            Position = position;
            totalElapsed = 0;
        }
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalElapsed += elapsed;
            if (totalElapsed > 2)
            {
                currentTexture++;
                if (currentTexture > Textures.Count)
                    currentTexture = 0;
                totalElapsed -= 2;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures[currentTexture], Position, Color.White);
        }

    }
}

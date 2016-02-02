using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicGame
{
    class RoomClass
    {
        protected Texture2D texture;
        
        public Rectangle Rectangle { get; set; }
        
        public static ContentManager Content { get; set;}

        public int roomVersion;
        public int[] roomVersionDoors;

        public void RoomCreate(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("room" + (i));
            this.Rectangle = newRectangle;
        }

        public RoomClass(int RoomVersion, int[] RoomVersionDoors)
        {
            roomVersion = RoomVersion;
            roomVersionDoors = RoomVersionDoors;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle = new Rectangle(Rectangle.X, Rectangle.Y, 16, 16);
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject2kXV
{
    class Rooms
    {
        protected Texture2D texture;

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public int roomVersion;
        public int[] roomVersionDoors;
        
        public void RoomCreate(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("room" + (i));
            this.Rectangle = newRectangle;
        }

        public Rooms(int RoomVersion, int[] RoomVersionDoors)
        {
            roomVersion = RoomVersion;
            roomVersionDoors = RoomVersionDoors;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, 16, 16);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}

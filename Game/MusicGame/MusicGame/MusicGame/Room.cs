using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MusicGame {
    class Room {
        public const int width = 1088;
        public const int height = 612;

        protected Texture2D texture;

        public Rectangle Rectangle { get; set; }

        public static ContentManager Content { get; set; }

        public int roomVersion;
        public int[] roomVersionDoors;

        public List<GameObject> Props { get; private set; }
        public List<Rectangle> ProtectedSpace { get; private set; }

        public Room(List<GameObject> props, List<Rectangle> protectedSpace, int RoomVersion, int[] RoomVersionDoors) {
            Props = new List<GameObject>();
            ProtectedSpace = new List<Rectangle>();
            Props = props;
            ProtectedSpace = protectedSpace;

            roomVersion = RoomVersion;
            roomVersionDoors = RoomVersionDoors;
        }
        public void RoomCreate(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("room" + (i));
            this.Rectangle = newRectangle;
        }
        public void Update() {

        }
        public void Draw() {

        }
        public void DrawForMap(SpriteBatch spriteBatch)
        {
            Rectangle = new Rectangle(Rectangle.X, Rectangle.Y, 16, 16);
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}

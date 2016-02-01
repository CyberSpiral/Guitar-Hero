using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProject2kXV
{
    class Map
    {
        private List<Rooms> rooms;

        public List<Rooms> Rooms
        {
            get { return rooms; }
        }

        public Map() 
        {
            rooms = new List<Rooms>();
        }


        public void Generate(Rooms[,] map, int textureWidth, int textureHeight)
        {
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int number = map[x, y].roomVersion;

                    if (number > 0)
                    {
                        rooms.Add(map[x, y]);
                        rooms[rooms.Count - 1].RoomCreate(number, new Rectangle(x * textureWidth, y * textureHeight, textureWidth, textureHeight));
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i].Draw(spriteBatch);
            }
        }
    }
}

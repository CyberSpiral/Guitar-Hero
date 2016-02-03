using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicGame
{
    class Map
    {
        private List<Room> rooms;

        public List<Room> Rooms
        {
            get { return rooms; }
        }

        public Room[,] mapTileSet;
        private GenerateNewFloor generateFloor;

        public Map()
        {
            rooms = new List<Room>();
            mapTileSet = new Room[25, 25];

            generateFloor = new GenerateNewFloor();
        }


        public void Generate()
        {
            rooms = new List<Room>();
            mapTileSet = generateFloor.GenerateRooms();
            for (int x = 0; x < mapTileSet.GetLength(0); x++)
                for (int y = 0; y < mapTileSet.GetLength(1); y++)
                {
                    int number = mapTileSet[x, y].roomVersion;

                    if (number > 0)
                    {
                        rooms.Add(mapTileSet[x, y]);
                        rooms[rooms.Count - 1].RoomCreate(number, new Rectangle(x * 16, y * 16, 16, 16));
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i].DrawForMap(spriteBatch);
            }
        }
    }
}
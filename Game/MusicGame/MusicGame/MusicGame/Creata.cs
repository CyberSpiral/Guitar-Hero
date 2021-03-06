﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicGame
{
    class CreateMap
    {
        private List<Rooms> rooms;

        public List<Rooms> Rooms
        {
            get { return rooms; }
        }

        public CreateMap()
        {
            rooms = new List<Rooms>();
        }


        public void Generate(Rooms[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int number = map[x, y].roomVersion;

                    if (number > 0)
                    {
                        rooms.Add(map[x, y]);
                        rooms[rooms.Count - 1].RoomCreate(number, new Rectangle(x * 16, y * 16, 16, 16));
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

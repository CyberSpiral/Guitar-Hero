using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame {
    class World {
        public List<Room> rooms { get; private set; }
        public int[] currentRoomNumber;
        public Room currentRoom { get; set; }
        public Map currentMap { get; set; }

        public World(List<Texture2D> gameObjects) {
            rooms = new List<Room>();
            currentMap = new Map();
            rooms.Add(GenerateRoom(gameObjects,2,new int[] {1,1,1,1}));
            currentRoom = rooms[0];
            currentRoomNumber = new int[2];
        }

        public void NewFloor(Room[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x,y].roomVersion == 1)
                    {
                        currentRoomNumber[0] = x;
                        currentRoomNumber[1] = y;
                        x = map.GetLength(0);
                        y = map.GetLength(1);

                        currentRoom = map[currentRoomNumber[0], currentRoomNumber[1]];
                    }
                }
            }
        }

        public void MoveBetweenRooms(Room[,] map, Direction direction)
        {
            if (direction == Direction.Up)
            {
                currentRoomNumber[1] -= 1;
            }
            if (direction == Direction.Left)
            {
                currentRoomNumber[0] -= 1;
            }
            if (direction == Direction.Down)
            {
                currentRoomNumber[1] += 1;
            }
            if (direction == Direction.Right)
            {
                currentRoomNumber[0] += 1;
            }
            currentRoom = map[currentRoomNumber[0], currentRoomNumber[1]];
        }

        public Room GenerateRoom(List<Texture2D> gameObjects, int roomVersion, int[] roomDoors) {
            Random r = new Random();
            List<GameObject> objects = new List<GameObject>();
            List<Rectangle> protectedSpace = new List<Rectangle>();
            switch(r.Next(3)) {
                case 0:
                    protectedSpace.Add(new Rectangle(0,272,1088,68));
                    protectedSpace.Add(new Rectangle(476,0,136,612));
                    break;
                case 1:
                    protectedSpace.Add(new Rectangle(0,272,1088,68));
                    protectedSpace.Add(new Rectangle(476,0,136,612));
                    break;
                case 2:
                    protectedSpace.Add(new Rectangle(0,272,1088,68));
                    protectedSpace.Add(new Rectangle(476,0,136,612));
                    break;
            }

            for (int i = 0; i < 50; i++) {
                Texture2D tempTex = gameObjects[r.Next(gameObjects.Count)];
                GameObject temp = new GameObject(tempTex,new Vector2(r.Next(15) * 68 + 34, r.Next(9) * 68 + 34),1,1,1,10000);
                bool tempBool = false;
                foreach (var rec in protectedSpace) {
                    if (rec.Intersects(temp.CollisionBox)) {
                        tempBool = true;
                    }
                }
                if (!tempBool) {
                    objects.Add(temp);
                }
            }


            return new Room(objects,protectedSpace, roomVersion, roomDoors);
        }
    }
}

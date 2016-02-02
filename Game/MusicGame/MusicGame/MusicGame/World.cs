using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame {
    class World {
        public List<Room> rooms { get; private set; }
        public Room currentRoom { get; private set; }

        public World(List<Texture2D> gameObjects) {
            rooms = new List<Room>();
            rooms.Add(GenerateRoom(gameObjects));
            currentRoom = rooms[0];
        }

        public Room GenerateRoom(List<Texture2D> gameObjects) {
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

            for (int i = 0; i < 4; i++) {
                GameObject temp = new GameObject(gameObjects[r.Next(gameObjects.Count)],new Vector2(r.Next(Room.width),r.Next(Room.height)),1,1,1);
                bool tempBool = false;
                foreach (var rec in protectedSpace) {
                    if (rec.Intersects(temp.collisionBox)) {
                        tempBool = true;
                    }
                }
                if (!tempBool) {
                    objects.Add(temp);
                }
            }


            return new Room(objects,protectedSpace);
        }
    }
}

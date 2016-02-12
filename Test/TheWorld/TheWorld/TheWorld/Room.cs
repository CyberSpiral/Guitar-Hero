using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class Room {
        public RoomGraphic RoomGraphic { get; set; }

        public int Level { get; protected set; }
        public List<GameObject> Props { get; protected set; }
        public List<Monster> Monsters { get; protected set; }
        public List<Rectangle> ProtectedSpace { get; protected set; }
        public List<Door> Doors { get; protected set; }
        public List<TempObject> Animations { get; set; }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }


        public Room(RoomGraphic roomGraphic, List<Rectangle> protectedSpace, List<GameObject> objects, List<Monster> monsters, int level) {
            Doors = new List<Door>();
            Animations = new List<TempObject>();
            RoomGraphic = roomGraphic;
            ProtectedSpace = protectedSpace;
            Props = objects;
            Level = level;
            Monsters = monsters;

            Doors.Add(new Door(roomGraphic.Door, new Vector2(38 / 2, World.RoomHeight / 2), 1, 1, 1, 0));
            Doors.Add(new Door(roomGraphic.Door, new Vector2(World.RoomWidth - (38 / 2), World.RoomHeight / 2), 1, 1, 1, 0));
            Doors.Add(new Door(roomGraphic.Door, new Vector2((World.RoomWidth) / 2, 38 / 2), 1, 1, 1, 0));
            Doors.Add(new Door(roomGraphic.Door, new Vector2((World.RoomWidth) / 2, World.RoomHeight - (38 / 2)), 1, 1, 1, 0));
        }

        public void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Draw(RoomGraphic.Background, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(RoomGraphic.OverLay, new Vector2(0, 0), Color.White);
            Props.ForEach(x => x.Draw(spriteBatch));
            Animations.ForEach(x => x.Draw(spriteBatch));
            foreach (Zombie z in Monsters.Where(x => x is Zombie)) {
                z.Draw(spriteBatch);
            }
            foreach (SpitZombie sZ in Monsters.Where(x => x is SpitZombie)) {
                sZ.Draw(spriteBatch);
            }
            foreach (Charger c in Monsters.Where(x => x is Charger)) {
                c.Draw(spriteBatch);
            }
        }

    }
}

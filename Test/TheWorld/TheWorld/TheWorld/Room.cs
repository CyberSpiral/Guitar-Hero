using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class Room {
        public RoomGraphic RoomGraphic { get; set; }

        public List<GameObject> Props { get; protected set; }
        public List<Rectangle> ProtectedSpace { get; protected set; }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }


        public Room(RoomGraphic roomGraphic, List<Rectangle> protectedSpace, List<GameObject> objects) {
            RoomGraphic = roomGraphic;
            ProtectedSpace = protectedSpace;
            Props = objects;
        }

    }
}

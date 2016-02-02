using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MusicGame {
    class Room {
        public const int width = 1088;
        public const int height = 612;

        public List<GameObject> Props { get; private set; }
        public List<Rectangle> ProtectedSpace { get; private set; }

        public Room(List<GameObject> props, List<Rectangle> protectedSpace) {
            Props = new List<GameObject>();
            ProtectedSpace = new List<Rectangle>();
            Props = props;
            ProtectedSpace = protectedSpace;
        }
        public void Update() {

        }
        public void Draw() {

        }
    }
}

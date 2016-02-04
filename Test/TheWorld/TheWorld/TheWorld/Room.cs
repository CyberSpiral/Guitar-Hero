using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class Room {
        public Texture2D Background { get; set; }

        public List<Object> Props { get; private set; }
        public List<Rectangle> ProtectedSpace { get; private set; }

        public Room(Texture2D background) {
            Background = background;
        }

    }
}

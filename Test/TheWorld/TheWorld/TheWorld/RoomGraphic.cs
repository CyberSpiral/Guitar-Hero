using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class RoomGraphic {
        public Texture2D Background { get; set; }
        public Texture2D Edge { get; set; }
        public Texture2D Door { get; set; }

        public RoomGraphic(Texture2D background, Texture2D edge, Texture2D door) {
            Background = background;
            Edge = edge;
            Door = door;
        }
    }
}

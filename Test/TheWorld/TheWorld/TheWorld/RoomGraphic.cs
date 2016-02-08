using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class RoomGraphic {
        public Texture2D Background { get; set; }
        public Texture2D Door { get; set; }
        public Texture2D OverLay { get; set; }

        public RoomGraphic(Texture2D background, Texture2D door, Texture2D overlay) {
            Background = background;
            Door = door;
            OverLay = overlay;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld {
    static class Static {
        public static Random r = new Random();
        public static SpriteFont s;

        public static int GetNumber() {
            return r.Next();
        }
        public static int GetNumber(int maxNumber) {
            return r.Next(maxNumber);
        }
        public static int GetNumber(int minNumber,int maxNumber) {
            return r.Next(minNumber, maxNumber);
        }
    }
}

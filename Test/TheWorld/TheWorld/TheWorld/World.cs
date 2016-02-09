using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    static class World {
        public const int RoomWidth = 1088;
        public const int RoomHeight = 612;


        public static Room[,] Rooms { get; set; }
        public static bool[,] ActiveRooms { get; set; }
        public static int[] CurrentRoomLocationCode { get; set; }
        public static int[] LastRoom { get; set; }
        public static int CurrentLevel { get; set; } = 10;
        
        public static void GenerateFloor() {
            ActiveRooms = new bool[25, 25];
            CurrentRoomLocationCode = new int[2];
            LastRoom = new int[2];
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    ActiveRooms[i, q] = false;
                }
            }

            ActiveRooms[12, 12] = true;
            for (int o = 0; o < 20; o++)
            {
                for (int i = 0; i < ActiveRooms.GetLength(0); i++)
                {
                    for (int q = 0; q < ActiveRooms.GetLength(1); q++)
                    {
                        if (Static.GetNumber(100) < 10)
                        {
                            try
                            {
                                if (ActiveRooms[i, q - 1] || ActiveRooms[i, q + 1] || ActiveRooms[i - 1, q] || ActiveRooms[i + 1, q])
                                {
                                    ActiveRooms[i, q] = true;
                                    CurrentRoomLocationCode[0] = i;
                                    CurrentRoomLocationCode[1] = q;
                                    break;
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            int tmpMinX = 25;
            int tmpMaxX = 0;
            int tmpMinY = 25;
            int tmpMaxY = 0;
            for (int i = 0; i < ActiveRooms.GetLength(0); i++)
            {
                for (int j = 0; j < ActiveRooms.GetLength(1); j++)
                {
                    if (ActiveRooms[i,j] == true)
                    {
                        if (tmpMinX > i) tmpMinX = i;
                        if (tmpMaxX < i)
                            tmpMaxX = i;
                        if (tmpMinY > j) tmpMinY = j;
                        if (tmpMaxY < j) tmpMaxY = j;
                    }
                }
            }
            int diffX = tmpMaxX - tmpMinX;
            int diffY = tmpMaxY - tmpMinY;
            int Q = 0;
            while (0 == LastRoom[0] || 0 == LastRoom[1])
            {
                Q += 1;
                for (int i = 0; i < ActiveRooms.GetLength(0); i++)
                {
                    for (int j = 0; j < ActiveRooms.GetLength(1); j++)
                    {
                        if (ActiveRooms[i, j] == true)
                        {
                            if (Math.Abs(CurrentRoomLocationCode[0] - i) > diffX && Math.Abs(CurrentRoomLocationCode[1] - j) > diffX)
                            {
                                if (Static.GetNumber(200) < 20)
                                {
                                    LastRoom[0] = i;
                                    LastRoom[1] = j;
                                }
                            }
                            if (Q > 30)
                            {
                                diffX -= 1;
                                diffY -= 1;
                                Q = 0;
                            }
                        }
                    }
                }
            }
        }
        public static void GenerateRooms(List<RoomGraphic> graphic, List<Texture2D> objectTextures, List<Texture2D> monsterTextures, Texture2D heartTexture) {
            Rooms = new Room[25, 25];
            for (int i = 0; i < Rooms.GetLength(0); i++) {
                for (int q = 0; q < Rooms.GetLength(1); q++)
                {

                    RoomMould thisRoom = new RoomMould(objectTextures, monsterTextures, heartTexture);

                    Rooms[i, q] = new Room(graphic[Static.GetNumber(graphic.Count)], thisRoom.ProtectedSpace, thisRoom.Props, thisRoom.Zombies, thisRoom.Spits, World.CurrentLevel);
                    Rooms[i, q].XCoordinate = i;
                    Rooms[i, q].YCoordinate = q;

                }
            }
        }
    }
}
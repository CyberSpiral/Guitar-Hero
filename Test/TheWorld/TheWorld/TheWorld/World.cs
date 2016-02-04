﻿using Microsoft.Xna.Framework;
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
        public static int[] CurrentRoom { get; set; }
        
        public static void GenerateFloor() {
            ActiveRooms = new bool[25, 25];
            CurrentRoom = new int[2];
            Random r = new Random();
            for (int i = 0; i < 25; i++) {
                for (int q = 0; q < 25; q++) {
                    ActiveRooms[i, q] = false;
                }
            }

            ActiveRooms[12, 12] = true;
            for (int o = 0; o < 20; o++) {
                for (int i = 0; i < ActiveRooms.GetLength(0); i++) {
                    for (int q = 0; q < ActiveRooms.GetLength(1); q++) {
                        if (r.Next(100) < 10) {
                            try {
                                if (ActiveRooms[i, q - 1] || ActiveRooms[i, q + 1] || ActiveRooms[i - 1, q] || ActiveRooms[i + 1, q]) {
                                    ActiveRooms[i, q] = true;
                                    CurrentRoom[0] = i;
                                    CurrentRoom[1] = q;
                                    break;
                                }
                            }
                            catch {

                            }
                        }
                    }
                }
            }
        }
        public static void GenerateRooms(List<Texture2D> background) {
            Rooms = new Room[25, 25];
            Random r = new Random();
            for (int i = 0; i < Rooms.GetLength(0); i++) {
                for (int q = 0; q < Rooms.GetLength(1); q++) {
                    List<Object> objects = new List<Object>();
                    List<Rectangle> protectedSpace = new List<Rectangle>();
                    switch (r.Next(3)) {
                        case 0:
                            protectedSpace.Add(new Rectangle(0, 272, 1088, 68));
                            protectedSpace.Add(new Rectangle(476, 0, 136, 612));
                            break;
                        case 1:
                            protectedSpace.Add(new Rectangle(0, 272, 1088, 68));
                            protectedSpace.Add(new Rectangle(476, 0, 136, 612));
                            break;
                        case 2:
                            protectedSpace.Add(new Rectangle(0, 272, 1088, 68));
                            protectedSpace.Add(new Rectangle(476, 0, 136, 612));
                            break;
                    }
                    Rooms[i, q] = new Room(background[r.Next(background.Count)]);
                }
            }
        }
    }
}

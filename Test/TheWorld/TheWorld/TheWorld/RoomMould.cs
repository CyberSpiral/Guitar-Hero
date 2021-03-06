﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld {
    class RoomMould {
        public List<GameObject> Props { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<Rectangle> ProtectedSpace { get; set; }

        public RoomMould(List<Texture2D> objectTextures, List<Texture2D> monsterTextures, Texture2D heartTexture) {
            Props = new List<GameObject>();
            Monsters = new List<Monster>();
            ProtectedSpace = new List<Rectangle>();
            switch (Static.GetNumber(3)) {
                case 0: {
                        ProtectedSpace.Add(new Rectangle(0, 204, 1088, 204));
                        ProtectedSpace.Add(new Rectangle(476, 0, 204, 612));
                        for (int o = 0; o < 20; o++) {
                            Texture2D tempTex = objectTextures[Static.GetNumber(objectTextures.Count)];
                            GameObject temp = new GameObject(tempTex, new Vector2(Static.GetNumber(14) * 68 + 102, Static.GetNumber(8) * 68 + 102));
                            bool tempBool = false;
                            foreach (var item in ProtectedSpace) {
                                if (item.Intersects(temp.CollisionBox)) {
                                    tempBool = true;
                                }
                            }
                            if (!tempBool) {
                                Props.Add(temp);
                            }
                        }
                        for (int o = 0; o < Math.Round(2f * (World.CurrentLevel / 1.5f)); o++) {
                            switch (Static.GetNumber(3)) {
                                case 0: {
                                        Zombie temp = new Zombie(monsterTextures[0], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 1.5f, 1, 4, 4, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 1: {
                                        SpitZombie temp = new SpitZombie(monsterTextures[1], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , monsterTextures[2], 6, 2.5f, 1,2, 2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 2: {
                                        Charger temp = new Charger(monsterTextures[3], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 4f, 1, 2, 2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                            }
                        }


                        break;
                    }
                case 1: {
                        ProtectedSpace.Add(new Rectangle(0, 204, 1088, 204));
                        ProtectedSpace.Add(new Rectangle(476, 0, 204, 612));
                        for (int o = 0; o < 20; o++) {
                            Texture2D tempTex = objectTextures[Static.GetNumber(objectTextures.Count)];
                            GameObject temp = new GameObject(tempTex, new Vector2(Static.GetNumber(14) * 68 + 102, Static.GetNumber(8) * 68 + 102));
                            bool tempBool = false;
                            foreach (var item in ProtectedSpace) {
                                if (item.Intersects(temp.CollisionBox)) {
                                    tempBool = true;
                                }
                            }
                            if (!tempBool) {
                                Props.Add(temp);
                            }
                        }
                        for (int o = 0; o < Math.Round(2f * (World.CurrentLevel / 1.5f)); o++) {
                            switch (Static.GetNumber(3)) {
                                case 0: {
                                        Zombie temp = new Zombie(monsterTextures[0], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 1.5f, 1, 4, 4, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 1: {
                                        SpitZombie temp = new SpitZombie(monsterTextures[1], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , monsterTextures[2], 6, 2.5f, 1, 2, 2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 2: {
                                        Charger temp = new Charger(monsterTextures[3], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 4f, 1, 2, 2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                            }
                        }


                        break;
                    }
                case 2: {
                        ProtectedSpace.Add(new Rectangle(0, 204, 1088, 204));
                        ProtectedSpace.Add(new Rectangle(476, 0, 204, 612));
                        for (int o = 0; o < 20; o++) {
                            Texture2D tempTex = objectTextures[Static.GetNumber(objectTextures.Count)];
                            GameObject temp = new GameObject(tempTex, new Vector2(Static.GetNumber(14) * 68 + 102, Static.GetNumber(8) * 68 + 102));
                            bool tempBool = false;
                            foreach (var item in ProtectedSpace) {
                                if (item.Intersects(temp.CollisionBox)) {
                                    tempBool = true;
                                }
                            }
                            if (!tempBool) {
                                Props.Add(temp);
                            }
                        }
                        for (int o = 0; o < Math.Round(2f * (World.CurrentLevel / 1.5f)); o++) {
                            switch (Static.GetNumber(3)) {
                                case 0: {
                                        Zombie temp = new Zombie(monsterTextures[0], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 1.5f, 1, 4, 4, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 1: {
                                        SpitZombie temp = new SpitZombie(monsterTextures[1], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , monsterTextures[2], 6, 2.5f, 1, 2, 2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                                case 2: {
                                        Charger temp = new Charger(monsterTextures[3], heartTexture, new Vector2(Static.GetNumber(476, 576), Static.GetNumber(68, 500))
                                            , 6, 4f, 1, 2,2, 500);
                                        bool tempBool = false;
                                        foreach (var item in Props) {
                                            if (temp.CollisionBox.Intersects(item.CollisionBox)) {
                                                tempBool = true;
                                            }
                                        }
                                        if (!tempBool) {
                                            Monsters.Add(temp);
                                        }
                                        break;
                                    }
                            }
                        }


                        break;
                    }
            }
        }
    }
}

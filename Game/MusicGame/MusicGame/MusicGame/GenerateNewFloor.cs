using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicGame
{
    class GenerateNewFloor
    {

        private enum Direction : int
        {
            Up, Left, Down, Right
        }

        private Direction direction;
        private Random random = new Random();

        FloorClass[,] roomsOnFloor;

        private int[] positionOnFloor;
        private int[] empty;
        private int numOfRooms;
        private int tmpNumOfRooms;

        private int startRoom;
        private int endRoom;
        private int itemRoom;
        private int specialRoom;
        private int differentRoom;


        public GenerateNewFloor()
        {
            roomsOnFloor = new FloorClass[25, 25];
            positionOnFloor = new int[2];
            empty = new int[4] { 0, 0, 0, 0 };

            startRoom = 1;
            endRoom = 1;
            itemRoom = 1;
            specialRoom = 4;
            differentRoom = 8;
        }

        public FloorClass[,] GenerateRooms()
        {
            startRoom = 1;
            endRoom = 1;
            itemRoom = 1;
            specialRoom = 4;
            differentRoom = 8;

            for (int i = 0; i < roomsOnFloor.GetLength(0); i++)
            {
                for (int j = 0; j < roomsOnFloor.GetLength(1); j++)
                {
                    roomsOnFloor[i, j] = new FloorClass(0, empty);
                }
            }

            numOfRooms = random.Next(6, 8);
            tmpNumOfRooms = 0;

            //place rooms
            for (int i = 0; i < 3; i++)
            {
                positionOnFloor[0] = 13;
                positionOnFloor[1] = 13;
                while (true)
                {
                    direction = (Direction)random.Next(4);

                    if (direction == Direction.Up)
                    {
                        positionOnFloor[1] -= 1;
                    }
                    if (direction == Direction.Left)
                    {
                        positionOnFloor[0] -= 1;
                    }
                    if (direction == Direction.Down)
                    {
                        positionOnFloor[1] += 1;
                    }
                    if (direction == Direction.Right)
                    {
                        positionOnFloor[0] += 1;
                    }

                    if (roomsOnFloor[positionOnFloor[0], positionOnFloor[1]].roomVersion == 0)
                    {
                        roomsOnFloor[positionOnFloor[0], positionOnFloor[1]] = new FloorClass(2, empty);
                        tmpNumOfRooms += 1;
                    }

                    if (tmpNumOfRooms > numOfRooms)
                    {
                        tmpNumOfRooms = 0;
                        numOfRooms = random.Next(4, 10);
                        break;
                    }
                }
            }
            roomsOnFloor[13, 13] = new FloorClass(2, empty);

            //checking number of entrances for each room and where they are
            for (int x = 0; x < roomsOnFloor.GetLength(0); x++)
            {
                for (int y = 0; y < roomsOnFloor.GetLength(1); y++)
                {
                    if (roomsOnFloor[x, y].roomVersion != 0)
                    {
                        roomsOnFloor[x, y].roomVersionDoors = new int[4] { 0, 0, 0, 0 };

                        for (int i = 0; i < 4; i++)
                        {
                            positionOnFloor[0] = x;
                            positionOnFloor[1] = y;

                            direction = (Direction)i;

                            if (direction == Direction.Up)
                            {
                                positionOnFloor[1] -= 1;
                            }
                            if (direction == Direction.Left)
                            {
                                positionOnFloor[0] -= 1;
                            }
                            if (direction == Direction.Down)
                            {
                                positionOnFloor[1] += 1;
                            }
                            if (direction == Direction.Right)
                            {
                                positionOnFloor[0] += 1;
                            }

                            if (roomsOnFloor[positionOnFloor[0], positionOnFloor[1]].roomVersion != 0)
                            {
                                if (direction == Direction.Up)
                                {
                                    roomsOnFloor[x, y].roomVersionDoors[0] = 1;
                                }
                                if (direction == Direction.Left)
                                {
                                    roomsOnFloor[x, y].roomVersionDoors[1] = 1;
                                }
                                if (direction == Direction.Down)
                                {
                                    roomsOnFloor[x, y].roomVersionDoors[2] = 1;
                                }
                                if (direction == Direction.Right)
                                {
                                    roomsOnFloor[x, y].roomVersionDoors[3] = 1;
                                }
                            }
                        }
                    }
                }
            }

            //fixing different types of rooms example 
            while (endRoom > 0 || startRoom > 0 || itemRoom > 0 || specialRoom > 0)
            {
                for (int x = 0; x < roomsOnFloor.GetLength(0); x++)
                {
                    for (int y = 0; y < roomsOnFloor.GetLength(1); y++)
                    {
                        if (roomsOnFloor[x, y].roomVersion == 2)
                        {
                            int numberOfDoors = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                numberOfDoors += roomsOnFloor[x, y].roomVersionDoors[i];
                            }
                            int chance = random.Next(8 * 3);

                            if (numberOfDoors == 1)
                            {
                                if (chance == 1 && endRoom > 0)
                                {
                                    roomsOnFloor[x, y].roomVersion = 5;
                                    endRoom -= 1;
                                    continue;
                                }
                            }
                            else if (numberOfDoors == 2)
                            {
                                if (chance == 1 && endRoom > 0)
                                {
                                    roomsOnFloor[x, y].roomVersion = 5;
                                    endRoom -= 1;
                                    continue;
                                }
                                else if (chance == 1 && startRoom > 0)
                                {
                                    roomsOnFloor[x, y].roomVersion = 1;
                                    startRoom -= 1;
                                    continue;
                                }
                                else if (chance == 1 && itemRoom > 0)
                                {
                                    roomsOnFloor[x, y].roomVersion = 4;
                                    itemRoom -= 1;
                                    continue;
                                }
                            }
                            if (chance == 1 && startRoom > 0)
                            {
                                roomsOnFloor[x, y].roomVersion = 1;
                                startRoom -= 1;
                            }
                            else if (chance == 1 && itemRoom > 0)
                            {
                                roomsOnFloor[x, y].roomVersion = 4;
                                itemRoom -= 1;
                            }
                            else if (chance == 1 && specialRoom > 0)
                            {
                                roomsOnFloor[x, y].roomVersion = 3;
                                specialRoom -= 1;
                            }
                        }
                    }
                }


            }
            return roomsOnFloor;
        }
    }
}

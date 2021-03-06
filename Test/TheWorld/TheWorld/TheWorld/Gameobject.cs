﻿using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TheWorld
{
    class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; protected set; }
        public Vector2 OldPos { get; set; }
        public bool Collectable { get; set; } = false;
        public Rectangle CollisionBox
        {
            get { return animated ? new Rectangle((int)Position.X - (Texture.Width / Columns / 2), (int)Position.Y - (Texture.Height / Rows / 2), Texture.Width / Columns, Texture.Height / Rows) 
                    : new Rectangle((int)Position.X - (Texture.Width / 2), (int)Position.Y - (Texture.Height / 2), Texture.Width, Texture.Height);
            }
            private set { value = CollisionBox; }
        }

        public int Rows { get; protected set; }
        public int Columns { get; protected set; }

        protected int currentFrame;
        protected int totalFrames;
        protected float totalElapsed;
        protected int animationSpeed;
        protected bool animated;

        public float Rotation { get; set; }
        protected float speed;

        public GameObject(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, int animationSpeed)
        {
            Texture = texture;
            Position = position;
            totalElapsed = 0;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            this.animationSpeed = animationSpeed;
            animated = true;
        }
        public GameObject(Texture2D texture, Vector2 position) {
            Texture = texture;
            Position = position;
            totalElapsed = 0;
            animated = false;
        }
        public virtual void Update(float elapsed)
        {
            totalElapsed += elapsed;
            if (animated) {
                if (totalElapsed > animationSpeed) {
                    currentFrame++;
                    if (currentFrame == totalFrames)
                        currentFrame = 0;
                    totalElapsed = 0;
                }
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animated) {
                int width = Texture.Width / Columns;
                int height = Texture.Height / Rows;
                int row = currentFrame / Columns;
                int column = currentFrame % Columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, Rotation - (float)(Math.PI * 0.5f), new Vector2((width / 2), (height / 2)), SpriteEffects.None, 0);
            }
            else {
                spriteBatch.Draw(Texture, Position, null, Color.White, Rotation - (float)(Math.PI * 0.5f), new Vector2((Texture.Width / 2), (Texture.Height / 2)), 1, SpriteEffects.None, 0);
            }
        }
        public virtual void ChangeTexture(Texture2D texture, int rows, int columns, int totalFrames)
        {
            Texture = texture;

            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

    }
    class Door : GameObject
    {
        private enum Direction : int
        {
            Up, Left, Down, Right
        }
        private Direction direction;
        public bool active;

        public Door(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, int animationSpeed)
            : base(texture, position, rows, columns, totalFrames, animationSpeed)
        {

            if (position.X < World.RoomWidth / 3)
            {
                direction = Direction.Left;
                Rotation = (float)Math.PI / 2;
            }
            else if (position.X > World.RoomWidth - (World.RoomWidth / 3))
            {
                direction = Direction.Right;
                Rotation = (float)Math.PI * 1.5f;
            }
            else if (position.Y < World.RoomHeight / 3)
            {
                direction = Direction.Up;
                Rotation = (float)Math.PI;
            }
            else if (position.Y > World.RoomHeight - (World.RoomHeight / 3))
            {
                direction = Direction.Down;
                Rotation = (float)Math.PI * 0;
            }
            ActivateDoors(position);
        }

        public void ActivateDoors(Vector2 position)
        {
            if (position.X < World.RoomWidth / 3)
            {
                active = World.ActiveRooms[World.CurrentRoomLocationCode[0] - 1, World.CurrentRoomLocationCode[1]] ? true : false;
            }
            else if (position.X > World.RoomWidth - (World.RoomWidth / 3))
            {
                active = World.ActiveRooms[World.CurrentRoomLocationCode[0] + 1, World.CurrentRoomLocationCode[1]] ? true : false;
            }
            else if (position.Y < World.RoomHeight / 2)
            {
                active = World.ActiveRooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1] - 1] ? true : false;
            }
            else if (position.Y > World.RoomHeight - (World.RoomHeight / 2))
            {
                active = World.ActiveRooms[World.CurrentRoomLocationCode[0], World.CurrentRoomLocationCode[1] + 1] ? true : false;
            }
        }

        public Vector2 Update(float elapsed, Rectangle playerCollisionBox, Vector2 playerPosition, int monsterCount)
        {
            base.Update(elapsed);
            if (active)
            {
                if (monsterCount == 0)
                {
                    if (CollisionBox.Intersects(playerCollisionBox))
                    {
                        if (direction == Direction.Up)
                        {
                            World.CurrentRoomLocationCode[1] -= 1;
                            playerPosition -= new Vector2(0, (playerPosition.Y - World.RoomHeight) + 100);
                        }
                        else if (direction == Direction.Left)
                        {
                            World.CurrentRoomLocationCode[0] -= 1;
                            playerPosition -= new Vector2((playerPosition.X - World.RoomWidth) + 100, 0);
                        }
                        else if (direction == Direction.Down)
                        {
                            World.CurrentRoomLocationCode[1] += 1;
                            playerPosition -= new Vector2(0, World.RoomHeight - (World.RoomHeight - playerPosition.Y) - 100);
                        }
                        else if (direction == Direction.Right)
                        {
                            World.CurrentRoomLocationCode[0] += 1;
                            playerPosition -= new Vector2(World.RoomWidth - (World.RoomWidth - playerPosition.X) - 100, 0);
                        }
                    }
                }
            }
            ActivateDoors(Position);
            return playerPosition;
        }
    }
    class TempObject : GameObject {
        public TempObject(Texture2D texture, Vector2 position, int rows, int columns, int totalFrames, int animationSpeed, float rotation) : base(texture,position,rows,columns,totalFrames,animationSpeed) {
            Texture = texture;
            Position = position;
            totalElapsed = 0;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            this.animationSpeed = animationSpeed;
            animated = true;
            Collectable = false;
            Rotation = rotation;
        }
        public override void Update(float elapsed) {
            base.Update(elapsed);
            if (currentFrame >= totalFrames-1) {
                Collectable = true;
            }
        }
    }
}
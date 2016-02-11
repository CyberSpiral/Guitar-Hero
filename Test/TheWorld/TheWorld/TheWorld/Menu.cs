using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorld
{
    public enum MenuType
    {
        StartMenu, PauseMenu, InGame, DeathMenu //add more??
    }

    class Menu
    {
        public MenuType menuType;

        MenuButton startButton;
        MenuButton exitButton;
        MenuButton pauseButton;


        public Menu()
        {
            menuType = MenuType.StartMenu;

        }

        public void Update(MouseState ms)
        {
            if (menuType == MenuType.StartMenu)
            {
                //exit, start
            }
            else if (menuType == MenuType.DeathMenu)
            {
                ////exit to menu, continue play
            }
            else if (menuType == MenuType.PauseMenu)
            {
                //exit to menu, continue play
            }
            else if (menuType == MenuType.InGame)
            {
                //pause button??
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (menuType == MenuType.StartMenu)
            {
                //exit, start
            }
            else if (menuType == MenuType.DeathMenu)
            {
                ////exit to menu, continue play
            }
            else if (menuType == MenuType.PauseMenu)
            {
                //exit to menu, continue play
            }
            else if (menuType == MenuType.InGame)
            {
                //pause button??
            }
        }
    }

    class MenuButton
    {
        Texture2D texture;
        Texture2D textureMouseHovering;
        Rectangle collisionBox;
        Rectangle mouseCollisionBox;

        bool mouseHovering;

        public MenuButton(Texture2D texture, Texture2D textureMouseHovering, Rectangle button)
        {
            this.texture = texture;
            this.textureMouseHovering = textureMouseHovering;
            this.collisionBox = button;
            mouseHovering = false;
        }

        public void Update(MouseState ms)
        {
            mouseCollisionBox = new Rectangle(ms.X, ms.Y, 1, 1);
            if (collisionBox.Intersects(mouseCollisionBox))
            {
                mouseHovering = true;
            }
            else
            {
                mouseHovering = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (collisionBox.Intersects(mouseCollisionBox))
            {
                spriteBatch.Draw(texture, collisionBox, Color.White);
            }
            else
            {
                spriteBatch.Draw(textureMouseHovering, collisionBox, Color.White);
            }
        }
    }
}

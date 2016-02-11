using Microsoft.Xna.Framework.Graphics;
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

        public Menu()
        {
            menuType = MenuType.StartMenu;
        }

        public void Update()
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
}

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
        StartMenu, PauseMenu, InGame, CreditMenu, DeathMenu //add more??
    }

    class Menu
    {
        public MenuType menuType;

        MenuButton startButton;
        Texture2D startTexture;
        Texture2D startTextureMouseHovering;

        MenuButton exitButton;
        Texture2D exitTexture;
        Texture2D exitTextureMouseHovering;

        MenuButton pauseButton;
        Texture2D pauseTexture;
        Texture2D pauseTextureMouseHovering;

        MenuButton creditButton;
        Texture2D creditTexture;
        Texture2D creditTextureMouseHovering;

        public Menu(Texture2D sT, Texture2D sTMH, Texture2D eT, Texture2D eTMH, Texture2D pT, Texture2D pTMH, Texture2D cT, Texture2D cTMH)
        {
            menuType = MenuType.StartMenu;
            startTexture = sT;
            startTextureMouseHovering = sTMH;
            exitTexture = eT;
            exitTextureMouseHovering = eTMH;
            pauseTexture = pT;
            pauseTextureMouseHovering = pTMH;
            creditTexture = cT;
            creditTextureMouseHovering = cTMH;

            startButton = new MenuButton(startTexture, startTextureMouseHovering, new Rectangle(100, 100, 100, 100));
            exitButton = new MenuButton(exitTexture, exitTextureMouseHovering, new Rectangle(500, 100, 100, 100));
            pauseButton = new MenuButton(pauseTexture, pauseTextureMouseHovering, new Rectangle(100, 300, 100, 100));
            creditButton = new MenuButton(creditTexture, creditTextureMouseHovering, new Rectangle(500, 300, 100, 100));

        }

        public void Update(MouseState ms)
        {
            if (menuType == MenuType.StartMenu)
            {
                //exit, start
                if (exitButton.mouseClicked)
                {
                    Environment.Exit(0);
                }
                if (startButton.mouseClicked)
                {
                    menuType = MenuType.InGame;
                }
            }
            else if (menuType == MenuType.DeathMenu)
            {
                ////exit to menu, continue play
                if (exitButton.mouseClicked)
                {
                    menuType = MenuType.StartMenu;
                }
                if (startButton.mouseClicked)
                {
                    menuType = MenuType.InGame;
                }
            }
            else if (menuType == MenuType.PauseMenu)
            {
                //exit to menu, continue play
                if (exitButton.mouseClicked)
                {
                    menuType = MenuType.StartMenu;
                }
                if (startButton.mouseClicked)
                {
                    menuType = MenuType.InGame;
                }
            }
            else if (menuType == MenuType.InGame)
            {
                //pause button??
            }
            else if (menuType == MenuType.CreditMenu)
            {
                //our names
                if (exitButton.mouseClicked)
                {
                    menuType = MenuType.StartMenu;
                }
                if (startButton.mouseClicked)
                {
                    menuType = MenuType.InGame;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (menuType == MenuType.StartMenu)
            {
                //exit, start
                startButton.Draw(spriteBatch);
                exitButton.Draw(spriteBatch);
                creditButton.Draw(spriteBatch);
            }
            else if (menuType == MenuType.DeathMenu)
            {
                ////exit to menu, continue play
                startButton.Draw(spriteBatch);
                exitButton.Draw(spriteBatch);
                creditButton.Draw(spriteBatch);
            }
            else if (menuType == MenuType.PauseMenu)
            {
                //exit to menu, continue play
                startButton.Draw(spriteBatch);
                exitButton.Draw(spriteBatch);
            }
            else if (menuType == MenuType.InGame)
            {
                //pause button??
                //pauseButton.Draw(spriteBatch);
            }
            else if (menuType == MenuType.CreditMenu)
            {
                //our names
                exitButton.Draw(spriteBatch);
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
        public bool mouseClicked;

        public MenuButton(Texture2D texture, Texture2D textureMouseHovering, Rectangle button)
        {
            this.texture = texture;
            this.textureMouseHovering = textureMouseHovering;
            this.collisionBox = button;
            mouseHovering = false;
            mouseClicked = false;
        }

        public void Update(MouseState ms, MouseState msOld)
        {
            mouseCollisionBox = new Rectangle(ms.X, ms.Y, 1, 1);
            if (collisionBox.Intersects(mouseCollisionBox))
            {
                mouseHovering = true;
                if (ms.LeftButton == ButtonState.Pressed && msOld.LeftButton == ButtonState.Released)
                {
                    mouseClicked = true;
                }
                else
                {
                    mouseClicked = false;
                }
            }
            else
            {
                mouseHovering = false;
                mouseClicked = false;
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

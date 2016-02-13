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
        StartMenu, PauseMenu, InGame, CreditMenu, DeathMenu, WinMenu //add more??
    }

    class Menu
    {
        public MenuType menuType;
        Texture2D mainMenu;
        Rectangle mainMenuRec;

        MenuButton startButton;
        Texture2D startTexture;
        Texture2D startTextureMouseHovering;

        MenuButton exitButton;
        Texture2D exitTexture;
        Texture2D exitTextureMouseHovering;

        MenuButton creditButton;
        Texture2D creditTexture;
        Texture2D credits;
        Texture2D creditTextureMouseHovering;

        public Menu(Texture2D startTexture, Texture2D startTextureMouseHovering, Texture2D exitTexture, Texture2D exitTextureMouseHovering, 
            Texture2D creditTexture, Texture2D creditTextureMouseHovering, Texture2D mainMenu, Texture2D credits)
        {
            menuType = MenuType.StartMenu;
            this.startTexture = startTexture;
            this.startTextureMouseHovering = startTextureMouseHovering;
            this.exitTexture = exitTexture;
            this.exitTextureMouseHovering = exitTextureMouseHovering;
            this.creditTexture = creditTexture;
            this.creditTextureMouseHovering = creditTextureMouseHovering;
            this.mainMenu = mainMenu;
            this.credits = credits;
            mainMenuRec = new Rectangle(0, -World.HUD, World.RoomWidth, World.RoomHeight + World.HUD);

            startButton = new MenuButton(startTexture, startTextureMouseHovering, new Rectangle((World.RoomWidth - 530)/2 + 30, -110, 530, 196));
            exitButton = new MenuButton(exitTexture, exitTextureMouseHovering, new Rectangle(80, 400, 329, 128));
            creditButton = new MenuButton(creditTexture, creditTextureMouseHovering, new Rectangle(80, 200, 329, 128));

        }

        public void Update(MouseState ms, MouseState msOld)
        {
            startButton.Update(ms, msOld);
            exitButton.Update(ms, msOld);
            creditButton.Update(ms, msOld);

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
                if (creditButton.mouseClicked)
                {
                    menuType = MenuType.CreditMenu;
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
                if (creditButton.mouseClicked)
                {
                    menuType = MenuType.CreditMenu;
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
                if (creditButton.mouseClicked)
                {
                    menuType = MenuType.CreditMenu;
                }
            }
            else if (menuType == MenuType.InGame)
            {

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
                spriteBatch.Draw(mainMenu, mainMenuRec, Color.White);
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

                spriteBatch.Draw(credits, new Vector2(0), Color.White);
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
        
        public bool mouseClicked;

        public MenuButton(Texture2D texture, Texture2D textureMouseHovering, Rectangle button)
        {
            this.texture = texture;
            this.textureMouseHovering = textureMouseHovering;
            this.collisionBox = button;
            mouseClicked = false;
        }

        public void Update(MouseState ms, MouseState msOld)
        {
            mouseCollisionBox = new Rectangle(ms.X, ms.Y - World.HUD, 1, 1);
            if (collisionBox.Intersects(mouseCollisionBox))
            {
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
                mouseClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (collisionBox.Intersects(mouseCollisionBox))
            {
                spriteBatch.Draw(textureMouseHovering, collisionBox, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, collisionBox, Color.White);
            }
        }
    }
}

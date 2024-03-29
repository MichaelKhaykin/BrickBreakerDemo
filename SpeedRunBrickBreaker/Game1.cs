﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SpeedRunBrickBreaker
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)(960 * Globals.ScreenScale.X);
            graphics.PreferredBackBufferHeight = (int)(920 * Globals.ScreenScale.Y);
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
            Globals.Random = new Random();

            MediaPlayer.IsRepeating = true;

            Globals.GameSong = Content.Load<Song>("Gaslamp Funworks");
            MediaPlayer.Volume = 0;
            MediaPlayer.Play(Globals.GameSong);

            Globals.CurrentScreen = ScreenStates.MainMenu;
            Globals.TopScreen = ScreenStates.MainMenu;

            Type colorType = typeof(Color);
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);

            Color color = default;

            foreach (var propInfo in propInfos)
            {
                var name = propInfo.Name;

                if (name.Contains("Black") || name.Contains("Blue") || name.Contains("Transparent")
                    || name.Contains("Transparency") || name.Contains("White")) continue;

                Globals.AllColors.Add((Color)propInfo.GetValue(color));
            }

            Globals.ScreenManager.Add(ScreenStates.MainMenu, new MainMenu(Content, graphics, (960, 920)));
          
     

             // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            Globals.MouseState = Mouse.GetState();
            Globals.KeyboardState = Keyboard.GetState();

            Globals.ScreenManager[Globals.TopScreen].Update(gameTime);

            Globals.OldMouseState = Globals.MouseState;
            Globals.OldKeyboardState = Globals.KeyboardState;

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Globals.ScreenManager[Globals.CurrentScreen].Draw(spriteBatch);
            if (Globals.CurrentScreen != Globals.TopScreen)
            {
                Globals.ScreenManager[Globals.TopScreen].Draw(spriteBatch);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

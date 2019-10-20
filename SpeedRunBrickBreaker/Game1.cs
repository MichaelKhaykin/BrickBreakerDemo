using Microsoft.Xna.Framework;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dictionary<ScreenStates, Screen> ScreenManager = new Dictionary<ScreenStates, Screen>();

        public static List<Color> AllColors = new List<Color>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920 / 2;
            graphics.PreferredBackBufferHeight = 920;
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
            MediaPlayer.Play(Globals.GameSong);

            Globals.CurrentScreen = ScreenStates.MainMenu;
            Globals.TopScreen = ScreenStates.MainMenu;

            Type colorType = typeof(Color);
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);

            Color color = default;

            foreach (var propInfo in propInfos)
            {
                var name = propInfo.Name;

                if (name.Contains("Black") || name.Contains("Blue") || name.Contains("Transperent")
                    || name.Contains("Transperency") || name.Contains("White")) continue;
                AllColors.Add((Color)propInfo.GetValue(null));
            }

            ScreenManager.Add(ScreenStates.MainMenu, new MainMenu(Content, GraphicsDevice));
            ScreenManager.Add(ScreenStates.Settings, new Settings(Content, GraphicsDevice));
            ScreenManager.Add(ScreenStates.Game, new GameScreen(Content, GraphicsDevice));


     

             // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Globals.MouseState = Mouse.GetState();
            Globals.KeyboardState = Keyboard.GetState();

            ScreenManager[Globals.TopScreen].Update(gameTime);

            Globals.OldMouseState = Globals.MouseState;
            Globals.OldKeyboardState = Globals.KeyboardState;

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            ScreenManager[Globals.CurrentScreen].Draw(spriteBatch);
            if (Globals.CurrentScreen != Globals.TopScreen)
            {
                ScreenManager[Globals.TopScreen].Draw(spriteBatch);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

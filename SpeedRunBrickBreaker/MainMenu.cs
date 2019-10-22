using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public class MainMenu : Screen
    {
        Button playButton;
        Button settingsButton;
       public MainMenu(ContentManager content, GraphicsDeviceManager graphics, (int width, int height) prefferedScreenSize) 
            : base(content, graphics, prefferedScreenSize)
        {
            Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
   
            playButton = new Button(Globals.TextureInfos["PlayButton"].Texture, new Vector2(center.X, center.Y + 200), Color.White, Globals.TextureInfos["PlayButton"].Scale, 0f);
            var texture = content.Load<Texture2D>("button_settings");
            var scale = Vector2.One / 2;
            settingsButton = new Button(texture, new Vector2(center.X, playButton.Position.Y + texture.Height * scale.Y), Color.White, scale, 0f);

            /*
            var font = content.Load<SpriteFont>("SpriteFont");
            var text = "Brick Breaker";
            Vector2 size = font.MeasureString(text);
            Title = new TextSprite(text, font, new Vector2(center.X, size.Y), Color.Black, Vector2.One, new Vector2(size.X / 2, size.Y / 2), 0f);
            */

            int boxWidth = 200;
            int boxHeight = 200;

            Rectangle topLeftBounds = new Rectangle(0, 0, boxWidth, boxHeight);
            Rectangle topRightBounds = new Rectangle(GraphicsDevice.Viewport.Width - boxWidth, 0, GraphicsDevice.Viewport.Width, boxHeight);
            Rectangle bottomLeftBounds = new Rectangle(0, GraphicsDevice.Viewport.Height - boxHeight, boxWidth, GraphicsDevice.Viewport.Height);  
            Rectangle bottomRightBounds = new Rectangle(GraphicsDevice.Viewport.Width - boxWidth, GraphicsDevice.Viewport.Height - boxHeight, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            Background background = new Background(content.Load<Texture2D>("BrickBreakerBackGround"), center, Color.White, Vector2.One, 0f, content.Load<Texture2D>("glowyBall"), topLeftBounds, topRightBounds, bottomLeftBounds, bottomRightBounds);

            AddToBothLists(background);
            AddToBothLists(playButton);
            AddToBothLists(settingsButton);
        }

        public override void Update(GameTime gameTime)
        {
            if(playButton.IsClicked())
            {
                Globals.ChangeState(ScreenStates.Game);
            }
            if(settingsButton.IsClicked())
            {
                Game1.graphics.PreferredBackBufferWidth = 821;
                Game1.graphics.PreferredBackBufferHeight = 650;
                Game1.graphics.ApplyChanges();

                if (!Globals.ScreenManager.ContainsKey(ScreenStates.Settings))
                {
                    Globals.ScreenManager.Add(ScreenStates.Settings, new Settings(Content, Game1.graphics, (821, 650)));
                }   
                Globals.ChangeState(ScreenStates.Settings);
            }

            base.Update(gameTime);
        }
    }
}

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

        TextSprite Title;
        public MainMenu(ContentManager content, GraphicsDevice graphics) 
            : base(content, graphics)
        {
            Vector2 center = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
   
            playButton = new Button(content.Load<Texture2D>("button_play"), new Vector2(center.X, center.Y + 200), Color.White, Vector2.One / 4, 0f);
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
            Rectangle topRightBounds = new Rectangle(graphics.Viewport.Width - boxWidth, 0, graphics.Viewport.Width, boxHeight);
            Rectangle bottomLeftBounds = new Rectangle(0, graphics.Viewport.Height - boxHeight, boxWidth, graphics.Viewport.Height);  
            Rectangle bottomRightBounds = new Rectangle(graphics.Viewport.Width - boxWidth, graphics.Viewport.Height - boxHeight, graphics.Viewport.Width, graphics.Viewport.Height);

            Background background = new Background(content.Load<Texture2D>("BrickBreakerBackGround"), center, Color.White, Vector2.One, 0f, content.Load<Texture2D>("glowyBall"), topLeftBounds, topRightBounds, bottomLeftBounds, bottomRightBounds);

            AddToBothLists(background);
            AddToBothLists(playButton);
            AddToBothLists(settingsButton);
      //      AddToBothLists(Title);
        }

        public override void Update(GameTime gameTime)
        {
            if(playButton.IsClicked())
            {
                Globals.ChangeState(ScreenStates.Game);
            }
            if(settingsButton.IsClicked())
            {
                Globals.ChangeState(ScreenStates.Settings);
            }

            base.Update(gameTime);
        }
    }
}

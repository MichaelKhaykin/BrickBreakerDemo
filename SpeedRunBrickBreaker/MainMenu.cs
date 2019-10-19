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

            Rectangle leftBounds = new Rectangle(0, 0, 200, 200);
            Rectangle rightBounds = new Rectangle(graphics.Viewport.Width - 200, 0, graphics.Viewport.Width, 200);

            Background background = new Background(content.Load<Texture2D>("BrickBreakerBackGround"), center, Color.White, Vector2.One, 0f, leftBounds, rightBounds, content.Load<Texture2D>("glowyBall"));

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

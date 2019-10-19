using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SpeedRunBrickBreaker
{
    public class Settings : Screen
    {
        DualTexturedButton musicButton;
        Button backButton;
        public Settings(ContentManager content, GraphicsDevice graphics) 
            : base(content, graphics)
        {
            var center = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);

            var firstTexture = content.Load<Texture2D>("button_music");
            var secondTexture = content.Load<Texture2D>("button_music_off");
            musicButton = new DualTexturedButton(firstTexture, secondTexture, center, Color.White, Vector2.One / 2, 0f);

            var backButtonTexture = content.Load<Texture2D>("backButton");
            var scale = Vector2.One;

            backButton = new Button(backButtonTexture, new Vector2((backButtonTexture.Width / 2) * scale.X, graphics.Viewport.Height - backButtonTexture.Height / 2 * scale.Y), Color.White, scale, 0f);             
            
            AddToBothLists(musicButton);
            AddToDrawList(backButton);
        }

        public override void Update(GameTime gameTime)
        {
            if(backButton.IsClicked())
            {
                Globals.CurrentScreen = Globals.ScreenStatesStack.Pop();
                Globals.TopScreen = Globals.CurrentScreen;
            }

            base.Update(gameTime);
        }
        
    }
}

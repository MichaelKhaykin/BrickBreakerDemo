using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpeedRunBrickBreaker
{
    public class PauseScreen : Screen
    {
        public PauseScreen(ContentManager content, GraphicsDeviceManager graphics, (int width, int height) prefferedScreenSize) 
            : base(content, graphics, prefferedScreenSize)
        {
            var center = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            var board = new Sprite(Content.Load<Texture2D>("board"), center, Color.White, Vector2.One, 0f);

            AddToDrawList(board);
        }

        public override void Update(GameTime gameTime)
        {
            if(Globals.KeyboardState.IsKeyDown(Keys.Escape) && Globals.OldKeyboardState.IsKeyUp(Keys.Escape))
            {
                Globals.CurrentScreen = Globals.ScreenStatesStack.Pop();
                Globals.TopScreen = Globals.CurrentScreen;
            }
            base.Update(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpeedRunBrickBreaker
{
    public class TextButton : Button
    {
        public TextSprite TextSprite { get; set; }

        public Keys Key { get; set; }

        public bool ShouldStartScanningForNextKey = false;
        public TextButton(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, SpriteFont font) 
            : base(texture, position, color, scale, rotation)
        {
            var origin = new Vector2(font.MeasureString("M").X / 2, font.MeasureString("M").Y / 2);
            TextSprite = new TextSprite("", font, position, Color.Black, Vector2.One * 2, origin, 0f);
            Key = Keys.None;
        }

        public void Update(GameTime gameTime, Keys bannedKey)
        {
            if (ShouldStartScanningForNextKey) 
            {
                if(Globals.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && Globals.OldMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released
                    && IsClicked() == false)
                {
                    //this means we have clicked on the mouse again
                    //which basically means you clicked on something else on the screen
                    ShouldStartScanningForNextKey = false;
                }

                if (Globals.KeyboardState.GetPressedKeys().Length < 1)
                {
                    return;
                }
                var pressedKey = Globals.KeyboardState.GetPressedKeys()[0];
                if (pressedKey == bannedKey) return;

                var letter = pressedKey.ToString()[0];
                if (letter >= 65 && letter <= 90)
                {
                    Key = pressedKey;
                    TextSprite.Text = $"{letter}";
                }
            }

            if(IsClicked())
            {
                ShouldStartScanningForNextKey = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            TextSprite.Draw(spriteBatch);
        }
    }
}

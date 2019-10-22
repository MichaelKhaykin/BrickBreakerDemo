using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public class TextButton : Button
    {
        public TextSprite TextSprite { get; set; }

        public bool ShouldStartScanningForNextKey = false;
        public TextButton(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, SpriteFont font) 
            : base(texture, position, color, scale, rotation)
        {
            var origin = new Vector2(font.MeasureString("M").X / 2, font.MeasureString("M").Y / 2);
            TextSprite = new TextSprite("", font, position, Color.Black, Vector2.One * 2, origin, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (ShouldStartScanningForNextKey) 
            {
                if (Globals.KeyboardState.GetPressedKeys().Length < 1)
                {
                    return;
                }
                var pressedKey = Globals.KeyboardState.GetPressedKeys()[0];
                var letter = pressedKey.ToString()[0];
                if (letter >= 65 && letter <= 90)
                {
                    TextSprite.Text = $"{letter}";
                    ShouldStartScanningForNextKey = false;
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

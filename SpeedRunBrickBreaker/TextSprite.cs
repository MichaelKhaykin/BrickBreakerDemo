using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public class TextSprite : GameObject
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public TextSprite(string text, SpriteFont font, Vector2 position, Color color, Vector2 scale, Vector2 origin, float rotation) 
            : base(position, color, scale, origin, rotation)
        {
            Text = text;
            Font = font;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, Effects, 0f);
            base.Draw(spriteBatch);
        }
    }
}

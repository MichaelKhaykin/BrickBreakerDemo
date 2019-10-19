using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public class Sprite : GameObject
    {
        public Texture2D Texture { get; set; }
        public int ScaledWidth => (int)(Texture.Width * Scale.X);
        public int ScaledHeight => (int)(Texture.Height * Scale.Y);
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)(Position.X - ScaledWidth / 2), (int)(Position.Y - ScaledHeight / 2), ScaledWidth, ScaledHeight);
            }
        }
        public Sprite(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation) 
            : base(position, color, scale, new Vector2(texture.Width / 2, texture.Height / 2), rotation)
        {
            Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, Effects, 0f);
        }
    }
}

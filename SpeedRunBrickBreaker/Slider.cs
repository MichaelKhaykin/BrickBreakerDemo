using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpeedRunBrickBreaker
{
    public class Slider : Sprite
    {
        private Button SliderObject;

        private Vector2 InitalStartPosition;

        private float maxX;
        public Slider(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Button sliderObject, float maxX) 
            : base(texture, position, color, scale, rotation)
        {
            SliderObject = sliderObject;
            InitalStartPosition = sliderObject.Position;
            this.maxX = maxX;
        }

        public override void Update(GameTime gameTime)
        {
            if (SliderObject.HitBox.Contains(Globals.MouseState.Position) && Globals.MouseState.LeftButton == ButtonState.Pressed)
            {
                if (Globals.MouseState.X > InitalStartPosition.X && Globals.MouseState.X < maxX)
                {
                    MediaPlayer.Volume = ((SliderObject.Position.X - InitalStartPosition.X) / (maxX - InitalStartPosition.X));
                    SliderObject.Position.X = Globals.MouseState.X;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            SliderObject.Draw(spriteBatch);
        }
    }
}

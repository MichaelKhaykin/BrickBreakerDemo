using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public class Heart : Sprite
    {
        public bool StartFadingOut = false;
        public bool ShouldBeRemoved = false;

        private Vector2 OgScale;
        private float scaleTravelPercentage = 0f;
        public Heart(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation) 
            : base(texture, position, color, scale, rotation)
        {
            OgScale = scale;
        }

        public void FadeOut()
        {
            if(scaleTravelPercentage >= 1f)
            {
                StartFadingOut = false;
                ShouldBeRemoved = true;
                return;
            }

            Scale = Vector2.Lerp(OgScale, Vector2.Zero, scaleTravelPercentage);
            Rotation += 0.3f;
            scaleTravelPercentage += 0.005f;
        }

        public override void Update(GameTime gameTime)
        {
            if(StartFadingOut)
            {
                FadeOut();
            }
            base.Update(gameTime);
        }
    }
}

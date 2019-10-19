using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SpeedRunBrickBreaker
{
    public class DualTexturedButton : Button
    {
        public Texture2D SecondaryTexture { get; }
        public Texture2D OgTexture { get; }
        public DualTexturedButton(Texture2D texture, Texture2D secondTexture, Vector2 position, Color color, Vector2 scale, float rotation) 
            : base(texture, position, color, scale, rotation)
        {
            SecondaryTexture = secondTexture;
            OgTexture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if(IsClicked())
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
                else
                {
                    MediaPlayer.Resume();
                }
                Texture = OgTexture == Texture ? SecondaryTexture : OgTexture;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedRunBrickBreaker
{
    public class ImageInfo
    {
        public int ScaledWidth { get; private set; }
        public int ScaledHeight { get; private set; }
        public Vector2 Scale { get; private set; }
        
        public Texture2D Texture { get; private set; }
    
        public ImageInfo(Texture2D texture, Vector2 scale)
        {
            Texture = texture;

            ScaledWidth = (int)(texture.Width * scale.X);
            
            ScaledHeight = (int)(texture.Height * scale.Y);
            
            Scale = scale;
        }
    }
}

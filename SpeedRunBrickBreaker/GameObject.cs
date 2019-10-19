using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpeedRunBrickBreaker
{
    public abstract class GameObject
    {
        private Vector2 position;
        public ref Vector2 Position
        {
            get
            {
                return ref position;
            }
        }
        public Color Color { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public SpriteEffects Effects { get; set; }
        public GameObject(Vector2 position, Color color, Vector2 scale, Vector2 origin, float rotation, SpriteEffects effects = SpriteEffects.None)
        {
            Position = position;
            Color = color;
            Scale = scale;
            Origin = origin;
            Rotation = rotation;
            Effects = effects;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        
        }
    }
}

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
    public class Paddle : Sprite
    {
        public Dictionary<Keys, Action<GraphicsDevice>> Movements = new Dictionary<Keys, Action<GraphicsDevice>>();
        public Paddle(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Keys left, Keys right) 
            : base(texture, position, color, scale, rotation)
        {
            float speed = 8;

            Movements.Add(left, (gd) => 
            { 
                if (Position.X - ScaledWidth / 2> 0) 
                { 
                    Position.X -= speed; 
                } 
            });

            Movements.Add(right, (gd) =>
            {
                if (Position.X + ScaledWidth / 2 < gd.Viewport.Width)
                {
                    Position.X += speed;
                }
            });
        }

        public void Update(GameTime gameTime, GraphicsDevice gd)
        {
            Keys[] currentPressedKeys = Globals.KeyboardState.GetPressedKeys();

            foreach(var key in currentPressedKeys)
            {
                if (!Movements.ContainsKey(key)) continue;
                Movements[key](gd);
            }

            base.Update(gameTime);
        }

    }
}

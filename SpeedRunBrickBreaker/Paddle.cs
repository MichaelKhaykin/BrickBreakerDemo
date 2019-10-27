using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedRunBrickBreaker
{
    public class Wrapper
    {
        public Keys key;
        public Action<GraphicsDevice> Action;

        public Wrapper(Keys key, Action<GraphicsDevice> action)
        {
            this.key = key;
            Action = action;
        }
    }

    public class Paddle : Sprite
    {
        public List<Wrapper> Actions = new List<Wrapper>();

        public Keys LeftKey { get; set; }
        public Keys RightKey { get; set; }
        public Paddle(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Keys left, Keys right)
            : base(texture, position, color, scale, rotation)
        {

            //make this a class
            LeftKey = left;
            RightKey = right;

            float speed = 8;

            Actions.Add(new Wrapper(LeftKey, (gd) =>
            {
                if (Position.X - ScaledWidth / 2 > 0)
                {
                    Position.X -= speed;
                }
            }));

            Actions.Add(new Wrapper(RightKey, (gd) =>
            {
                if (Position.X + ScaledWidth / 2 < gd.Viewport.Width)
                {
                    Position.X += speed;
                }
            }
            ));
        }

        public void Update(GameTime gameTime, GraphicsDevice gd)
        {
            for(int i = 0; i < Actions.Count; i++)
            {
                if(Actions[i].key != LeftKey && i == 0)
                {
                    Actions[i].key = LeftKey;
                }
                else if(Actions[i].key != RightKey && i == 1)
                {
                    Actions[i].key = RightKey;
                }
            }

            Keys[] currentPressedKeys = Globals.KeyboardState.GetPressedKeys();

            foreach (var key in currentPressedKeys)
            {
                for(int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].key != key) continue;
                    Actions[i].Action(gd);
                }
            }

            base.Update(gameTime);
        }

    }
}

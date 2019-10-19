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
    public class Button : Sprite
    {
        public Button(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation) 
            : base(texture, position, color, scale, rotation)
        {

        }

        public bool IsClicked()
        {
            return HitBox.Contains(Globals.MouseState.Position) && Globals.MouseState.LeftButton == ButtonState.Pressed &&
                Globals.OldMouseState.LeftButton == ButtonState.Released;
        }
    }
}

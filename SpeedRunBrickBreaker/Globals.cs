using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedRunBrickBreaker
{
    public static class Globals
    {
        public static ScreenStates CurrentScreen;

        public static ScreenStates TopScreen;

        public static MouseState MouseState;

        public static MouseState OldMouseState;

        public static KeyboardState KeyboardState;

        public static KeyboardState OldKeyboardState;

        public static Song GameSong;

        public static Stack<ScreenStates> ScreenStatesStack = new Stack<ScreenStates>();

        public static Random Random;

        public static Dictionary<ScreenStates, Screen> ScreenManager = new Dictionary<ScreenStates, Screen>();

        public static List<Color> AllColors = new List<Color>();

        public static Vector2 ScreenScale = Vector2.One;
        public static void ChangeState(ScreenStates newState)
        {
            ScreenStatesStack.Push(CurrentScreen);
            CurrentScreen = newState;
            TopScreen = newState;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpeedRunBrickBreaker
{
    public class PauseScreen : Screen
    {
        Sprite board;

        Sprite musicIcon;
        Slider slider;

        Sprite keyboardSprite;

        Sprite settingsHeader;

        Texture2D pixel;

        TextButton leftKeyBinding;
        TextButton rightKeyBinding;

        public PauseScreen(ContentManager content, GraphicsDeviceManager graphics, (int width, int height) prefferedScreenSize) 
            : base(content, graphics, prefferedScreenSize)
        {
            var center = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
         
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            var musicIconTexture = content.Load<Texture2D>("soundIcon");
            var musicIconScale = Vector2.One * 2;
            musicIcon = new Sprite(musicIconTexture, new Vector2(musicIconTexture.Width * 3 / 2 * musicIconScale.X, center.Y - musicIconTexture.Height * musicIconScale.Y), Color.White, musicIconScale, 0f);

            var keyboardSpriteTexture = content.Load<Texture2D>("miniKeyboard");
            Vector2 keyBoardScale = new Vector2(1, 1.5f);
            keyboardSprite = new Sprite(keyboardSpriteTexture, new Vector2(musicIcon.Position.X, musicIcon.Position.Y + musicIcon.ScaledHeight / 2 + keyboardSpriteTexture.Height * keyBoardScale.Y), Color.DarkKhaki, keyBoardScale, 0f);

            Vector2 sliderPosition = new Vector2(center.X, musicIcon.Position.Y);

            var sliderObjectTexture = content.Load<Texture2D>("sliderObject");
            var sliderObjectScale = Vector2.One;
            var sliderObject = new Button(sliderObjectTexture, new Vector2(sliderPosition.X - sliderObjectTexture.Width * sliderObjectScale.X, sliderPosition.Y), Color.White, sliderObjectScale, 0f);

            var sliderTexture = content.Load<Texture2D>("slider_bg");
            var sliderScale = Vector2.One * 2;
            int gapBetweenEndOfSliderAndImage = 50;
            slider = new Slider(sliderTexture, sliderPosition, Color.White, sliderScale, 0, sliderObject, center.X + sliderTexture.Width / 2 * sliderScale.X - gapBetweenEndOfSliderAndImage);

            var keyButtonTexture = content.Load<Texture2D>("keyboardButton");
            SpriteFont font = content.Load<SpriteFont>("SpriteFont");
            var buttonScale = Vector2.One / 2;
            leftKeyBinding = new TextButton(keyButtonTexture, new Vector2(slider.Position.X - slider.ScaledWidth / 4, keyboardSprite.Position.Y), Color.White, buttonScale, 0f, font, Settings.LeftPaddleKey);

            rightKeyBinding = new TextButton(keyButtonTexture, new Vector2(leftKeyBinding.Position.X + leftKeyBinding.ScaledWidth * 3, leftKeyBinding.Position.Y), Color.White, buttonScale, 0f, font, Settings.RightPaddleKey);

            board = new Sprite(Content.Load<Texture2D>("board"), center, Color.White, Vector2.One, 0f);

            var headerTexture = Content.Load<Texture2D>("header_settings");
            Vector2 headerScale = Vector2.One;
            settingsHeader = new Sprite(headerTexture, new Vector2(center.X, board.Position.Y - board.ScaledHeight / 2 + headerTexture.Height / 2 * headerScale.Y + 15), Color.White, headerScale, 0f);


            AddToDrawList(board);
            AddToDrawList(musicIcon);
            AddToDrawList(leftKeyBinding);
            AddToDrawList(rightKeyBinding);
            AddToBothLists(slider);
            AddToDrawList(keyboardSprite);
            AddToDrawList(settingsHeader);
        }

        public override void Update(GameTime gameTime)
        {
            leftKeyBinding.Update(gameTime, rightKeyBinding.Key);
            rightKeyBinding.Update(gameTime, leftKeyBinding.Key);

            Settings.LeftPaddleKey = leftKeyBinding.Key;
            Settings.RightPaddleKey = rightKeyBinding.Key;

            if (Globals.KeyboardState.IsKeyDown(Keys.Escape) && Globals.OldKeyboardState.IsKeyUp(Keys.Escape))
            {
                Globals.CurrentScreen = Globals.ScreenStatesStack.Pop();
                Globals.TopScreen = Globals.CurrentScreen;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

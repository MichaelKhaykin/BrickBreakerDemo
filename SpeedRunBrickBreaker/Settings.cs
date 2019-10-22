using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SpeedRunBrickBreaker
{
    public class Settings : Screen
    {
        Sprite board;

        DualTexturedButton musicButton;
        Button backButton;

        TextButton emptyBox;

        Sprite settingsHeader;
        public Settings(ContentManager content, GraphicsDeviceManager graphics, (int width, int height) size) 
            : base(content, graphics, size)
        {
            var center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            var firstTexture = content.Load<Texture2D>("button_music");
            var secondTexture = content.Load<Texture2D>("button_music_off");
            musicButton = new DualTexturedButton(firstTexture, secondTexture, center, Color.White, Vector2.One / 2, 0f);

            var backButtonTexture = content.Load<Texture2D>("backButtonArrow");
            var scale = Vector2.One;

            backButton = new Button(backButtonTexture, new Vector2((backButtonTexture.Width / 2) * scale.X, backButtonTexture.Height / 2 * scale.Y), Color.White, scale, 0f);

            var emptyBoxTexture = content.Load<Texture2D>("emptyBox");
            var emptyBoxScale = Vector2.One / 4;
            emptyBox = new TextButton(emptyBoxTexture, new Vector2(center.X, musicButton.Position.Y + emptyBoxTexture.Height * emptyBoxScale.Y), Color.White, emptyBoxScale, 0f, content.Load<SpriteFont>("SpriteFont"));

            board = new Sprite(Content.Load<Texture2D>("board"), center, Color.White, Vector2.One, 0f);

        //    var texture = Content.Load<Texture2D>()
       //     settingsHeader = new Sprite(Content.Load<Texture2D>("header_settings"), new Vector2(board.Position.X - board.ScaledWidth / 2 + , board.Position.Y - board.ScaledHeight / 2), Color.White, Vector2.One, 0f);

            AddToDrawList(board);
            AddToDrawList(settingsHeader);
            AddToBothLists(emptyBox);
            AddToBothLists(musicButton);
            AddToDrawList(backButton);
        }

        public override void Update(GameTime gameTime)
        {
            if(backButton.IsClicked())
            {
                Globals.CurrentScreen = Globals.ScreenStatesStack.Pop();
                Globals.TopScreen = Globals.CurrentScreen;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(spriteBatch);
        }
    }
}

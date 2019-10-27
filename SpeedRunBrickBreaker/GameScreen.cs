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
    public class GameScreen : Screen
    {
        GameObject[,] bricks;

        Paddle paddle;
        Ball ball;
        List<Heart> Lives = new List<Heart>();

        Texture2D pixel;
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics, (int width, int height) size)
            : base(content, graphics, size)
        {
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            var pinkBrickTexture = content.Load<Texture2D>("pinkBrick");
            var scale = Vector2.One;


            int xCount = GraphicsDevice.Viewport.Width / (int)(pinkBrickTexture.Width * scale.X);
            int yCount = (GraphicsDevice.Viewport.Height / 2) / (int)(pinkBrickTexture.Height * scale.Y);

            bricks = new GameObject[yCount, xCount];

            int threshhold = 100;

            for (int i = 0; i < yCount; i++)
            {
                for (int j = 0; j < xCount; j++)
                {
                    var color = Globals.AllColors[Globals.Random.Next(0, Globals.AllColors.Count)];

                    var upBrick = i - 1 >= 0 ? bricks[i - 1, j] : null;
                    var downBrick = i + 1 < yCount ? bricks[i + 1, j] : null;
                    var leftBrick = j - 1 >= 0 ? bricks[i, j - 1] : null;
                    var rightBrick = j + 1 < xCount ? bricks[i, j + 1] : null;

                    while (upBrick != null && Math.Abs(upBrick.Color.R - color.R) >= threshhold &&
                           downBrick != null && Math.Abs(downBrick.Color.G - color.G) >= threshhold &&
                           leftBrick != null && Math.Abs(leftBrick.Color.B - color.B) >= threshhold &&
                           rightBrick != null && Math.Abs(rightBrick.Color.A - color.A) >= threshhold)
                    {
                        color = Globals.AllColors[Globals.Random.Next(0, Globals.AllColors.Count)];
                    }

                    var brick = new Sprite(pinkBrickTexture, new Vector2(pinkBrickTexture.Width / 2 * scale.X + pinkBrickTexture.Width * i * scale.X, (pinkBrickTexture.Height / 2 * scale.Y) + pinkBrickTexture.Height * j * scale.Y), color, scale, 0f);
                    bricks[i, j] = brick;
                }
            }


            var paddleTexture = content.Load<Texture2D>("greenPaddle");
            var paddleScale = new Vector2(2, 4);
           
            paddle = new Paddle(paddleTexture, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - (paddleTexture.Height / 2) * paddleScale.Y), Color.White, paddleScale, 0f, Settings.LeftPaddleKey, Settings.RightPaddleKey);

            var ballTexture = content.Load<Texture2D>("glowyBall");
            var ballScale = new Vector2(1.9f);

            //the reason i put + 10000 on the height is because i actussally odn't want the ball to bounce on the bottom of the screen
            //and i know there is a check that will kill me before i reach that point
            Rectangle bounds = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height + 10000);

            ball = new Ball(ballTexture, new Vector2(paddle.Position.X, (paddle.Position.Y - paddle.ScaledHeight / 2) - (ballTexture.Height / 2 * ballScale.Y)), Color.White, ballScale, 0f, Keys.Space, bounds);

            var heartTexture = content.Load<Texture2D>("Heart");
            var heartScale = Vector2.One / 3;

            for (int i = 0; i < 3; i++)
            {
                Lives.Add(new Heart(heartTexture, new Vector2(heartTexture.Width / 2 * heartScale.X + heartTexture.Width * i * heartScale.X, heartTexture.Height / 2 * heartScale.Y), Color.Black, heartScale, 0f));

            }

            AddToDrawList(paddle);
            AddToDrawList(ball);
        }

        public override void Update(GameTime gameTime)
        {
            if(paddle.LeftKey != Settings.LeftPaddleKey)
            {
                paddle.LeftKey = Settings.LeftPaddleKey;
            }
            if(paddle.RightKey != Settings.RightPaddleKey)
            {
                paddle.RightKey = Settings.RightPaddleKey;
            }

            if(Globals.KeyboardState.IsKeyDown(Keys.Escape) && Globals.OldKeyboardState.IsKeyUp(Keys.Escape))
            {
                if (!Globals.ScreenManager.ContainsKey(ScreenStates.Pause))
                {
                    Globals.ScreenManager.Add(ScreenStates.Pause, new PauseScreen(Content, GraphicsDeviceManager, (960, 920)));
                }
                Globals.ChangeState(ScreenStates.Game, ScreenStates.Pause);
            }

            bool currentlyRemovingLife = false;
            for (int i = 0; i < Lives.Count; i++)
            {
                if(Lives[i].StartFadingOut == true)
                {
                    currentlyRemovingLife = true;
                }

                if (Lives[i].ShouldBeRemoved)
                {
                    Lives.RemoveAt(i);
                    i--;
                    continue;
                }
                Lives[i].Update(gameTime);
            }

            if (currentlyRemovingLife == false)
            {
                ball.Update(gameTime);
            }
           
            paddle.Update(gameTime, GraphicsDevice);


            if (!ball.IsActivated)
            {
                ball.Position = new Vector2(paddle.Position.X, (paddle.Position.Y - paddle.ScaledHeight / 2) - (ball.ScaledHeight / 2));
                return;
            }
         
            for (int i = 0; i < bricks.GetLength(0); i++)
            {
                for (int j = 0; j < bricks.GetLength(1); j++)
                {
                    var brick = (Sprite)bricks?[i, j];

                    if (brick == null) continue;

                    if (ball.HitBox.Intersects(brick.HitBox))
                    {
                        bricks[i, j] = null;
                        ball.Speed.Y *= -1;
                        goto outOfForLoop;
                    }
                }
            }
            
            outOfForLoop:

            if (ball.HitBox.Intersects(paddle.HitBox))
            {
                ball.Speed.Y *= -1;
                var random = Globals.Random.Next(-2, 3);
                while (ball.Speed.X + random == 0)
                {
                    random = Globals.Random.Next(-2, 3);
                }
            }


            if (ball.Position.Y - ball.ScaledHeight / 2 >= GraphicsDevice.Viewport.Height)
            {
                ball.IsActivated = false;
                if (Lives.Count - 1 < 0)
                {
                    //TODO: Implenent death screen
                    //u died
                }
                else
                {
                    Lives[Lives.Count - 1].StartFadingOut = true;
                    ball.ClearParticles();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.Black);

            for(int i = 0; i < bricks.GetLength(0); i++)
            {
                for(int j = 0; j < bricks.GetLength(1); j++)
                {
                    bricks[i, j]?.Draw(spriteBatch);
                }
            }

            for (int i = 0; i < Lives.Count; i++)
            {
                Lives[i].Draw(spriteBatch);
            }

            base.Draw(spriteBatch);

            /* debug
            spriteBatch.Draw(pixel, ball.HitBox, Color.Green);
            spriteBatch.Draw(pixel, paddle.HitBox, Color.Red);
            */
        }
    }
}

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
        List<GameObject> bricks = new List<GameObject>();
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

            for (int i = 0; i < GraphicsDevice.Viewport.Width / (int)(pinkBrickTexture.Width * scale.X); i++)
            {
                for (int j = 0; j < (GraphicsDevice.Viewport.Height / 2) / (int)(pinkBrickTexture.Height * scale.Y); j++)
                {
                    var brick = new Sprite(pinkBrickTexture, new Vector2(pinkBrickTexture.Width / 2 * scale.X + pinkBrickTexture.Width * i * scale.X, (pinkBrickTexture.Height / 2 * scale.Y) + pinkBrickTexture.Height * j * scale.Y), Color.White, scale, 0f);
                    bricks.Add(brick);
                }
            }


            var paddleTexture = content.Load<Texture2D>("greenPaddle");
            var paddleScale = new Vector2(2, 4);
            paddle = new Paddle(paddleTexture, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - (paddleTexture.Height / 2) * paddleScale.Y), Color.White, paddleScale, 0f, Keys.A, Keys.D);

            var ballTexture = content.Load<Texture2D>("glowyBall");
            var ballScale = new Vector2(2);

            //the reason i put + 10000 on the height is because i actually odn't want the ball to bounce on the bottom of the screen
            //and i know there is a check that will kill me before i reach that point
            Rectangle bounds = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height + 10000);

            ball = new Ball(ballTexture, new Vector2(paddle.Position.X, (paddle.Position.Y - paddle.ScaledHeight / 2) - (ballTexture.Height / 2 * ballScale.Y)), Color.White, ballScale, 0f, Keys.Space, bounds);

            var heartTexture = content.Load<Texture2D>("Heart");
            var heartScale = Vector2.One / 3;

            for (int i = 0; i < 3; i++)
            {
                Lives.Add(new Heart(heartTexture, new Vector2(heartTexture.Width / 2 * heartScale.X + heartTexture.Width * i * heartScale.X, heartTexture.Height / 2 * heartScale.Y), Color.White, heartScale, 0f));

            }

            AddToDrawList(paddle);
            AddToDrawList(ball);
        }

        public override void Update(GameTime gameTime)
        {
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
         
            for (int i = 0; i < bricks.Count; i++)
            {
                var brick = (Sprite)bricks[i];

                if (ball.HitBox.Intersects(brick.HitBox))
                {
                    bricks.Remove(brick);
                    i--;
                    ball.Speed.Y *= -1;
                }
            }

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

            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Draw(spriteBatch);
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

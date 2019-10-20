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
    public class Background : Sprite
    {
        List<Ball> balls = new List<Ball>();

        TimeSpan changeSpeedTimer = TimeSpan.FromSeconds(1);
        TimeSpan elapsedChangeSpeedTimer = TimeSpan.Zero;
        public Background(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Texture2D ballTexture, params Rectangle[] boundaries)
            : base(texture, position, color, scale, rotation)
        {
            var ballScale = Vector2.One * 2;

            for(int i = 0; i < boundaries.Length; i++)
            {
                var bounds = boundaries[i];

                var randomX = Globals.Random.Next((int)(bounds.X + ballTexture.Width / 2 * ballScale.X), (int)(bounds.Width - ballTexture.Width / 2 * ballScale.X));
                var randomY = Globals.Random.Next((int)(bounds.Y + ballTexture.Height / 2 * ballScale.Y), (int)(bounds.Height - ballTexture.Height / 2 * ballScale.Y));

                var ball = new Ball(ballTexture, new Vector2(randomX, randomY), Game1.AllColors[Globals.Random.Next(0, Game1.AllColors.Count)], ballScale, 0f, Keys.None, bounds)
                {
                    Speed = new Vector2(6)
                };

                balls.Add(ball);
            }
        }

        public override void Update(GameTime gameTime)
        {
            elapsedChangeSpeedTimer += gameTime.ElapsedGameTime;

            bool shouldReset = false;
            foreach(var ball in balls)
            {
                if(elapsedChangeSpeedTimer >= changeSpeedTimer)
                {
                    ball.Speed += new Vector2(Globals.Random.Next(-1, 2), Globals.Random.Next(-1, 2));
                    shouldReset = true;
                }
                ball.Update(gameTime);
            }

            if(shouldReset)
            {
                elapsedChangeSpeedTimer = TimeSpan.Zero;
            }


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach(var ball in balls)
            {
                ball.Draw(spriteBatch);
            }
        }
    }
}

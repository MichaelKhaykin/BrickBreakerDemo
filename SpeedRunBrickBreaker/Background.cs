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
        Ball leftBall;
        Ball rightBall;

        public Background(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Rectangle bounds1, Rectangle bounds2, Texture2D ballTexture)
            : base(texture, position, color, scale, rotation)
        {
            var ballScale = Vector2.One * 2;

            var leftBallX = Globals.Random.Next((int)(bounds1.X + ballTexture.Width / 2 * ballScale.X), (int)(bounds1.Width - ballTexture.Width / 2 * ballScale.X));
            var leftBallY = Globals.Random.Next((int)(bounds1.Y + ballTexture.Height / 2 * ballScale.Y), (int)(bounds1.Height - ballTexture.Height / 2 * ballScale.Y));
            leftBall = new Ball(ballTexture, new Vector2(leftBallX, leftBallY), Color.White, ballScale, 0f, Keys.None, bounds1)
            {
                Speed = new Vector2(6)
            };

            var rightBallX = Globals.Random.Next((int)(bounds2.X + ballTexture.Width / 2 * ballScale.X), (int)(bounds2.Width - ballTexture.Width / 2 * ballScale.X));
            var rightBallY = Globals.Random.Next((int)(bounds2.Y + ballTexture.Height / 2 * ballScale.Y), (int)(bounds2.Height - ballTexture.Height / 2 * ballScale.Y));
            rightBall = new Ball(ballTexture, new Vector2(rightBallX, rightBallY), Color.White, ballScale, 0f, Keys.None, bounds2)
            {
                Speed = new Vector2(-6)
            };
     
        
        }

        public override void Update(GameTime gameTime)
        {
            leftBall.Update(gameTime);
            rightBall.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            leftBall.Draw(spriteBatch);
            rightBall.Draw(spriteBatch);
        }
    }
}

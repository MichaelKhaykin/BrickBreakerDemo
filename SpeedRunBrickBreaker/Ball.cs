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
    public class Particle : Sprite
    {
        public TimeSpan TimeToLive { get; set; }

        private Color OgColor;

        public float TravelPercentage = 0f;
        public Particle(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, TimeSpan timeToLive) 
            : base(texture, position, color, scale, rotation)
        {
            OgColor = Color;
            TimeToLive = timeToLive;
        }

        public void FadeOut(GameTime gameTime)
        {
            Color *= 1 - TravelPercentage;
            TravelPercentage += (float)gameTime.ElapsedGameTime.TotalSeconds / (float)TimeToLive.TotalSeconds;
        }
    }
    public class Ball : Sprite
    {
        public bool IsActivated = false;
        private Keys activationKey;

        private Vector2 speed;
        public ref Vector2 Speed
        {
            get
            {
                return ref speed;
            }
        }
        List<Particle> Particles = new List<Particle>();
        TimeSpan timeToAddParticle = TimeSpan.FromMilliseconds(50);
        TimeSpan elapsedTimeToAddParticle = TimeSpan.Zero;

        public Rectangle Bounds { get; set; }

        public Ball(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation, Keys activationKey, Rectangle bounds)
            : base(texture, position, color, scale, rotation)
        {
            if (activationKey == Keys.None) IsActivated = true;
            this.activationKey = activationKey;
            Bounds = bounds;
        }

        public void ClearParticles()
        {
            Particles.Clear();
        }
        public override void Update(GameTime gameTime)
        {

            if (Globals.KeyboardState.IsKeyDown(activationKey) && Globals.OldKeyboardState.IsKeyUp(activationKey) &&
                IsActivated == false)
            {
                Speed = new Vector2(Globals.Random.Next(0, 2) == 0 ? 8 : -8, Globals.Random.Next(0, 2) == 0 ? -8 : -13);
                IsActivated = true;
            }

            if (!IsActivated)
            {
                if (Particles.Count != 0)
                {
                    ClearParticles();
                }
                return;
            }

            Position += Speed;
            elapsedTimeToAddParticle += gameTime.ElapsedGameTime;
            if (elapsedTimeToAddParticle >= timeToAddParticle)
            {
                elapsedTimeToAddParticle = TimeSpan.Zero;
                Particles.Add(new Particle(Texture, Position, Globals.AllColors[Globals.Random.Next(0, Globals.AllColors.Count)], Vector2.One / 2, 0f, TimeSpan.FromMilliseconds(Globals.Random.Next(500, 1000))));
            }

            if (Position.X + ScaledWidth / 2 >= Bounds.Width || Position.X - ScaledWidth / 2 <= Bounds.X)
            {
                Speed.X *= -1;
            }
            if (Position.Y - ScaledHeight / 2 <= Bounds.Y || Position.Y + ScaledHeight / 2 >= Bounds.Height)
            {
                Speed.Y *= -1;
            }


            for (int i = 0; i < Particles.Count; i++)
            {
                if (Particles[i].TravelPercentage >= 1f)
                {
                    Particles.RemoveAt(i);
                    i--;
                    continue;
                }
                Particles[i].FadeOut(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var particle in Particles)
            {
                particle.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}

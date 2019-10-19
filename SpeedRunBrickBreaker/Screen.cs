using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedRunBrickBreaker
{
    public class Screen
    {
        public ContentManager Content { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        private List<GameObject> GameObjectsToUpdate = new List<GameObject>();

        private List<GameObject> GameObjectsToDraw = new List<GameObject>();
        public Screen(ContentManager content, GraphicsDevice graphics)
        {
            Content = content;
            GraphicsDevice = graphics;
        }

        public void AddToUpdateList(GameObject a)
            => GameObjectsToUpdate.Add(a);

        public void AddToDrawList(GameObject a)
            => GameObjectsToDraw.Add(a);

        public void AddRangeToUpdateList(List<GameObject> objects)
            => GameObjectsToUpdate.AddRange(objects);

        public void AddRangeToDrawList(List<GameObject> objects)
            => GameObjectsToDraw.AddRange(objects);
        public void AddToBothLists(GameObject a)
        {
            AddToUpdateList(a);
            AddToDrawList(a);
        }

        public void AddRangeToBothLists(List<GameObject> objects)
        {
            AddRangeToUpdateList(objects);
            AddRangeToDrawList(objects);
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach(var gameObject in GameObjectsToUpdate)
            {
                gameObject.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(var gameObject in GameObjectsToDraw)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}

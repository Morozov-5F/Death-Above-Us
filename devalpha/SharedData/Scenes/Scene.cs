using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace devalpha.Scenes
{
    public abstract class Scene
    {
        protected GraphicsDeviceManager graphics;

        public Scene(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// Draw scene
        /// </summary>
        /// <param name="spriteBatch">Sprite batch to draw on.</param>
        public abstract void Draw (SpriteBatch spriteBatch);

        /// <summary>
        /// Update scene
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public abstract void Update (GameTime gameTime);

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="contentManager">Content manager.</param>
        public abstract void LoadContent(ContentManager contentManager);
   
    }
}


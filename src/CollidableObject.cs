using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace devalpha
{
    /// <summary>
    /// An abstract class providing a collidable object base
    /// </summary>
    public abstract class CollidableObject : ISprite
    {
        #region Variables 
        protected float     rotation;
        protected Vector2   position;
        protected Texture2D texture;
        protected Vector2   velocity;
        #endregion

        #region Properties
        public float      Rotation { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2    Position { get { return this.position; } set { this.position = value; } }
        public Texture2D  Texture  { get { return this.texture;  } }
        #endregion

        /// <summary>
        /// Draw on the specified Sprite.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch to draw on.</param>
        public abstract void Draw (SpriteBatch spriteBatch);

        /// <summary>
        /// Update the game with specified gameTime.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public abstract void Update (GameTime gameTime);

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="contentManager">Content manager.</param>
        public abstract void LoadContent(ContentManager contentManager);
    
        /// <summary>
        /// Checks the collision.
        /// </summary>
        /// <returns><c>true</c>, if collision was checked, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        public abstract bool CheckCollision(CollidableObject obj);
    }
}


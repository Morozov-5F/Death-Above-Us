using System;

namespace devalpha.Particles
{
    public class ParticlesEmitter : ISprite
    {
        public ParticlesEmitter()
        {
            
        }

        #region ISprite implementation

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager manager)
        {
            throw new NotImplementedException();
        }

        public float Rotation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}


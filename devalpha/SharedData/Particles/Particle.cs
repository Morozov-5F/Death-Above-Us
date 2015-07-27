using devalpha;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System;

namespace devalpha.Particles
{
    public class Particle : ISprite
    {
        public static String TexturePath;

        public Vector2 Scale;
        public Vector2 Origin;

        public Vector2 Velocity;
        public Vector2 Force;
        public Vector2 Friction;

        private Vector2 position;
        private float rotation;
        private Texture2D texture;

        public Particle(Texture2D texture)
        {
            this.texture = texture;
            Origin = new Vector2(texture.Width, texture.Height);
        }

        #region ISprite implementation
        public float Rotation { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public Texture2D Texture { get { return this.texture; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(Texture, Position, null, null, Origin, Rotation, Scale, Color.White, SpriteEffects.None, 1f);
        }

        public void Update(GameTime time)
        {
            position += Velocity;
            Velocity += Force;
            Velocity *= Friction;
        }

        public void LoadContent(ContentManager manager)
        {
            // texture is loaded by ParticlesEmitter and passed in costructor
        }
        #endregion
    }
}


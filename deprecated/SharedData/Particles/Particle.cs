using devalpha;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Diagnostics;

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

        public float rotationSpeed;

        protected Vector2 position;
        protected float rotation;
        protected Texture2D texture;

        public float lifeTime = 0;
        public float maxLifeTime;

        public Color color;
        public Color targetColor;

        private Random random;

        public Particle(Texture2D texture = null, Random random = null)
        {
            this.texture = texture;
            this.random = random;
            maxLifeTime = 1f;
            Scale = Vector2.One;
            position = Vector2.Zero;
            Friction = Vector2.One;
            if (texture != null)
                Origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            color = targetColor = Color.White;
        }

        #region ISprite implementation
        public float Rotation { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public Texture2D Texture { get { return this.texture; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(Texture, Position, null, null, Origin, Rotation, Scale, color, SpriteEffects.None, 0f);
            }
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            position += Velocity * deltaTime;
            Velocity += Force;
            Velocity *= Friction;
            lifeTime += deltaTime;
            rotation += rotationSpeed;

            float fadingMul = 1.5f - lifeTime / maxLifeTime;
            color *= fadingMul;
        }

        public void LoadContent(ContentManager manager)
        {
            // texture is loaded by ParticlesEmitter and passed in costructor
        }
        #endregion
    }
}


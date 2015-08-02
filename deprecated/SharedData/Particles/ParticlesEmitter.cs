using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace devalpha.Particles
{
    public class ParticlesEmitter : ISprite
    {
        protected const int MAX_PARTICLES_COUNT = 500;
        protected Vector2 position;
        protected float rotation;

        protected Particle[] particles;
        protected Texture2D texture;

        protected String particleType;

        protected float spawnDelayMax;
        protected float spawnDelayCurrent;
        protected int spawnCount;

        protected Random random;

        public float Scale = 1f;

        public ParticlesEmitter(String particleType, Texture2D texture = null)
        {
            this.particleType = particleType;
            particles = new Particle[MAX_PARTICLES_COUNT];
            random = new Random();
            spawnCount = 1;
            this.texture = texture;
        }

        public virtual Particle InstantiateParticle()
        {
            var particle = new Particle(texture, random);
            particle.Position = Position;
            particle.Rotation = Rotation;
            particle.Scale = new Vector2(Scale);
            return particle;
        }

        protected void AddParticle()
        {
            int index = -1; 
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] == null)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                particles[index] = InstantiateParticle();
            }
        }

        public void RemoveParticle(int index)
        {
            particles[index] = null;
        }

        #region ISprite implementation
        public float Rotation { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public Texture2D Texture { get { return this.texture; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Draw(spriteBatch);
                }
            }
        }

        public void Update(GameTime time)
        {
            float deltaTime = (float)time.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Update(time);
                    if (particles[i].lifeTime >= particles[i].maxLifeTime)
                    {
                        particles[i] = null;
                    }
                }
            }
            if (spawnDelayMax > 0)
            {
                spawnDelayCurrent -= deltaTime;
                if (spawnDelayCurrent <= 0)
                {
                    spawnDelayCurrent = spawnDelayMax;
                    for (int i = 0; i < spawnCount; i++)
                        AddParticle(); 
                }
            }
           
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("particles/" + particleType);
        }
        #endregion
    }
}


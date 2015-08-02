using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace devalpha.Particles
{
    class FireEmitter : ParticlesEmitter
    {
        private const float MAX_VELOCITY = 80f;
        private const float MAX_RANGE = 50f;
        public FireEmitter(Texture2D texture = null) : base("fire", texture)
        {
            spawnDelayMax = 0.01f;
            spawnCount = 3;
        }

        public override Particle InstantiateParticle()
        {
            var particle = base.InstantiateParticle();
            particle.Velocity = new Vector2((float)random.NextDouble() * MAX_VELOCITY - MAX_VELOCITY / 2, (float)random.NextDouble() * MAX_VELOCITY - MAX_VELOCITY/2);
            particle.targetColor = new Color(Color.White, 0);
            particle.maxLifeTime = 0.2f;
            particle.Rotation = (float)random.NextDouble() * 3f;
            particle.rotationSpeed = ((float)random.NextDouble() - 0.5f) * 0f;
            //particle.Position += new Vector2((float)random.NextDouble() * MAX_RANGE - MAX_VELOCITY / 2, (float)random.NextDouble() * MAX_RANGE - MAX_VELOCITY / 2);
            return particle;
        }
    }
}

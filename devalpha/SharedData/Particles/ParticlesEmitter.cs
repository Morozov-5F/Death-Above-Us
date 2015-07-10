using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace devalpha.Particles
{
    public class ParticlesEmitter
    {
        private Type particleType;
        private Texture2D particleTexture;

        public ParticlesEmitter(Type particleType)
        {
            if (particleType.IsAssignableFrom(typeof(Particle)))
            {
                throw new Exception("Bad particle type");
            }
            this.particleType = particleType;
        }

        public void LoadContent(ContentManager Content)
        {
            String texturePath = (String)particleType.GetField("TexturePath").GetValue(null);
            Console.WriteLine("texturePath: " + texturePath);
            //Content.Load<Texture2D>(texturePath);
        }

        public Particle instantiateParticle()
        {
            object[] args = {particleTexture};
            return (Particle)Activator.CreateInstance(particleType, args);
        }
    }
}


using devalpha;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System;

namespace devalpha.Particles
{
    class FireParticle : Particle
    {
        public FireParticle(Texture2D texture) : base(texture)
        {
            TexturePath = "Particles/fire";
        }
    }
}

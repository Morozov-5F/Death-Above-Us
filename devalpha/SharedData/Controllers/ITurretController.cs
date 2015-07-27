using System;

using devalpha.Objects;
using Microsoft.Xna.Framework;

namespace devalpha.Controllers
{
    public interface ITurretController
    {
        Turret ControllableTurret { get; set; }

        void Update(GameTime time);
    }
}


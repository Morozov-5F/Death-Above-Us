using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using devalpha.Weapons.Bullets;
using System.Diagnostics;

namespace devalpha.Weapons
{
    /// <summary>
    /// Double barelled weapon. Shots from two barrels
    /// </summary>
    public class DoubleBarelledWeapon : Weapon
    {
        private float barrelOffset;
        private float barellLength;
        private Vector2 basePosition;
        private Texture2D bulletTexture;

        public DoubleBarelledWeapon(Vector2 middlePointPos, float barellOffset, float barellLength, Bullet bulletType)
        {
            this.basePosition = middlePointPos;
            this.barrelOffset = barellOffset;
            this.barellLength = barellLength;

            this.bulletType = bulletType;

            reloadTime = 500f;
            reloadMeter = reloadTime;
        }

        public override void LoadContent(ContentManager Content)
        {
            bulletTexture = Content.Load<Texture2D>(@"bullets\bullet1");
        }

        public override List<Bullet> CreateShot()
        {
            if (reloadMeter >= reloadTime)
            {
                List<Bullet> bulletsToAdd = new List<Bullet>(2);
                var multiplier = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * barrelOffset / 2 * Camera.GameScale;
                var bullet1 = new Bullet(this.position - multiplier, this.rotation, bulletTexture);
                var bullet2 = new Bullet(this.position + multiplier, this.rotation, bulletTexture);

                bulletsToAdd.Add(bullet1);
                bulletsToAdd.Add(bullet2);

                reloadMeter = 0;
                return bulletsToAdd;
            }
            else
                return null;
        }

        public override void Draw(GameTime time)
        {
//            Do nothing
//            throw new NotImplementedException();
        }

        public override void Update(GameTime time)
        {
            this.position = basePosition + barellLength * new Vector2((float)Math.Sin(this.rotation), -(float)Math.Cos(this.rotation));
            reloadMeter += (float)time.ElapsedGameTime.TotalMilliseconds;
        }
    }
}


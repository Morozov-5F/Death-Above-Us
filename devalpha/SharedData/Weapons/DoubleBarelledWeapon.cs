using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using devalpha.Weapons.Bullets;

namespace devalpha.Weapons
{
    /// <summary>
    /// Double barelled weapon. Shots from two barrels
    /// </summary>
    public class DoubleBarelledWeapon : Weapon
    {
        private float barrelOffset;
        private Texture2D bulletTexture;

        public DoubleBarelledWeapon(Vector2 firstBarrelPositon, float offset, Bullet bulletType)
        {
            this.position = firstBarrelPositon;
            this.barrelOffset = offset;

            this.bulletType = bulletType;

            reloadTime = 100f;
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
                var bullet1 = new Bullet(this.position, rotation, bulletTexture);
                var bullet2 = new Bullet(this.position + new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * barrelOffset, rotation, bulletTexture);

                bulletsToAdd.Add(bullet1);
                bulletsToAdd.Add(bullet2);

                reloadMeter = 0;
//                До сих пор чота не работает
//                ShotMadeEventHandler handler = ShotMadeEvent;
//                if (handler != null)
//                    handler(this, eventArgs);
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
            reloadMeter += (float)time.ElapsedGameTime.TotalMilliseconds;
        }
    }
}


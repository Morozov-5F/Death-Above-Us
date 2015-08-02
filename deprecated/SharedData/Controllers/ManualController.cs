using System;
using System.Collections.Generic;

using devalpha.Objects;
using devalpha.Weapons.Bullets;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace devalpha.Controllers
{
    public class ManualController : ITurretController
    {
        private Turret turret;
        private float prevMouseX;

        public bool LeftToRight;
        public Turret ControllableTurret { get { return turret; } set { turret = value;} }
       
        private Action< List<Bullet> > onShotCallback;

        public ManualController(Action< List<Bullet> > shotCallback, bool leftToRight = false)
        {
            LeftToRight = leftToRight;
            onShotCallback = shotCallback;
        }

        public void Update(GameTime time)
        {
            // Управление жестами
            #if __MOBILE__
            TouchCollection tc = TouchPanel.GetState();
            foreach (TouchLocation tl in tc)
            {
                var positionCheck = tl.Position.X < Camera.DeviceViewport.Width / 2;

                if (positionCheck == LeftToRight)
                {
                    if (tl.State == TouchLocationState.Moved)
                    {
                        turret.Rotation += MathHelper.ToRadians((tl.Position.X - prevMouseX) / 1.3f) * (float)time.ElapsedGameTime.TotalSeconds * 25;
                    }
                    prevMouseX = tl.Position.X;
                }
                if (tl.State == TouchLocationState.Moved && positionCheck != LeftToRight)
                {
                    onShotCallback(turret.Gun.CreateShot());
                }
            }
            #endif
            // Управление мышью
            #if !__MOBILE__
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                turret.Rotation += MathHelper.ToRadians((mouseState.Position.X - prevMouseX) / 1.3f) * (float)time.ElapsedGameTime.TotalSeconds * 25;
            }
            prevMouseX = mouseState.Position.X;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                onShotCallback(turret.Gun.CreateShot());
            }
            #endif
            // Ограничение поворота
            if (turret.Rotation > Turret.MAX_ROTATION)
            {
                turret.Rotation = Turret.MAX_ROTATION;
            }
            if (turret.Rotation < -Turret.MAX_ROTATION)
            {
                turret.Rotation = -Turret.MAX_ROTATION;
            }

            Camera.Position = new Vector2(-MathHelper.ToDegrees(Math.Abs(turret.Rotation)) * Math.Sign(turret.Rotation), 0) / 2f;
        }
    }
}


using System;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using devalpha.Weapons;

using System.Diagnostics;

namespace devalpha.Objects
{
    public class Turret : ISprite
    {
        private float     turretRotation;
        private Vector2   basePosition;
        private Vector2   turretOffset, baseOffset;
        private Weapon    gun;

        private Texture2D baseTexture;
        private Texture2D turretTexture;

        public float      Rotation { get { return this.turretRotation; } set { this.turretRotation = value; } }
        public Vector2    Position { get { return this.basePosition; } set { this.basePosition = value; } }
        public Texture2D  Texture  { get { return this.baseTexture;  } }
        public Weapon     Gun      { get { return this.gun; } }

        // TODO: bottom edge for turret
        public Vector2    BottomEdge { get {return basePosition + baseOffset; } }

        public const float MAX_ROTATION = (float)Math.PI / 2.5f;
        private float prevMouseX;

        public Turret(Vector2 position)
        {
            Debug.WriteLine("Object turret is being constructed");
            basePosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(baseTexture, basePosition, null, null, baseOffset, 0, Vector2.One, Color.White, SpriteEffects.None, 0);
			spriteBatch.Draw(turretTexture, basePosition, null, null, turretOffset, turretRotation, Vector2.One, Color.White, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager contentManager)
        {
            baseTexture = contentManager.Load<Texture2D>(@"turrets\1\1");
            turretTexture = contentManager.Load<Texture2D>(@"turrets\1\2");

            basePosition -= new Vector2(0, Texture.Height);

            turretOffset = new Vector2(30f, 70f);
            baseOffset = new Vector2(52f, 0f);

            gun = new DoubleBarelledWeapon(basePosition - new Vector2(13f, 5f) * Camera.GameScale, 16f, null);
            gun.LoadContent(contentManager);
        }

        public void Update(GameTime time)
        {
            // Управление жестами
			#if __MOBILE__
            var gesture = default(GestureSample);

            while (TouchPanel.IsGestureAvailable)
                gesture = TouchPanel.ReadGesture();
            if (gesture.GestureType == GestureType.HorizontalDrag)
            {
                turretRotation += MathHelper.ToRadians(gesture.Delta.X / 1.3f);
            }
			#endif
            // Управление мышью
			#if !__MOBILE__
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                turretRotation += MathHelper.ToRadians((mouseState.Position.X - prevMouseX) / 1.3f);
            }
            prevMouseX = mouseState.Position.X;
			#endif
            // Ограничение поворота
            if (turretRotation > MAX_ROTATION)
            {
                turretRotation = MAX_ROTATION;
            }
            if (turretRotation < -MAX_ROTATION)
            {
                turretRotation = -MAX_ROTATION;
            }
                
            Gun.Rotation = this.Rotation;
            Gun.Update(time);
            Camera.Position = new Vector2(-MathHelper.ToDegrees(Math.Abs(turretRotation)) * Math.Sign(turretRotation), 0) / 2f;
        }
    }
}


using System;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;


using System.Diagnostics;

namespace devalpha.Objects
{
    public class Turret : ISprite
    {
        private float turretRotation;
        private Vector2   basePosition;
        private Vector2   turretOffset, baseOffset;

        private Texture2D baseTexture;
        private Texture2D turretTexture;

        public float      Rotation { get { return this.turretRotation; } set { this.turretRotation = value; } }
        public Vector2    Position { get { return this.basePosition; } set { this.basePosition = value; } }
        public Texture2D  Texture  { get { return this.baseTexture;  } }

        // TODO: bottom edge for turret
        public Vector2    BottomEdge { get {return basePosition + baseOffset; } }
        public Vector2    Scale;

        public const float MAX_ROTATION = (float)Math.PI / 2.5f;
        private float prevMouseX;

        public Turret(Vector2 position)
        {
            Debug.WriteLine("Object turret is being constructed");
            Scale = Vector2.One;
            basePosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(baseTexture, basePosition, null, null, baseOffset, 0, Scale, Color.White, SpriteEffects.None, 0);
            spriteBatch.Draw(turretTexture, basePosition, null, null, turretOffset, turretRotation, Scale, Color.White, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager contentManager)
        {
            baseTexture = contentManager.Load<Texture2D>(@"turrets\1\1");
            turretTexture = contentManager.Load<Texture2D>(@"turrets\1\2");

            basePosition -= new Vector2(0, Texture.Height * Scale.Y);

            turretOffset = new Vector2(121f, 270f);
            baseOffset = new Vector2(207f, 0f);
        }

        public void Update(GameTime time)
        {
            // Управление жестами
            var gesture = default(GestureSample);

            while (TouchPanel.IsGestureAvailable)
                gesture = TouchPanel.ReadGesture();

            if (gesture.GestureType == GestureType.HorizontalDrag)
            {
                turretRotation += MathHelper.ToRadians(gesture.Delta.X / 1.3f);
            }

            // Управление мышью
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                turretRotation += MathHelper.ToRadians((mouseState.Position.X - prevMouseX) / 1.3f);
            }
            prevMouseX = mouseState.Position.X;

            // Ограничение поворота
            if (turretRotation > MAX_ROTATION)
            {
                turretRotation = MAX_ROTATION;
            }
            if (turretRotation < -MAX_ROTATION)
            {
                turretRotation = -MAX_ROTATION;
            }
            Camera.Position = new Vector2(-MathHelper.ToDegrees(Math.Abs(turretRotation)) * Math.Sign(turretRotation), 0) / 2f;
        }
    }
}


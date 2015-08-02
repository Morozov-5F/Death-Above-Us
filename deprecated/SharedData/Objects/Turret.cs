using System;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using devalpha.Weapons;
using devalpha.Controllers;

using System.Diagnostics;

namespace devalpha.Objects
{
    public class Turret : ISprite
    {
        private float     turretRotation;
        private Vector2   basePosition;
        private Vector2   turretOffset, baseOffset;
        private Weapon    gun;

        private ITurretController controller;

        private Texture2D baseTexture;
        private Texture2D turretTexture;

        public float      Rotation { get { return this.turretRotation; } set { this.turretRotation = value; } }
        public Vector2    Position { get { return this.basePosition; } set { this.basePosition = value; } }
        public Texture2D  Texture  { get { return this.baseTexture;  } }
        public Weapon     Gun      { get { return this.gun; } }
        
        public Vector2 GunPosition 
        {
            get 
            {
                return Position;
            }
        }

        // TODO: bottom edge for turret
        public Vector2    BottomEdge { get {return basePosition + baseOffset; } }

        public const float MAX_ROTATION = (float)Math.PI / 2.5f;

        public Turret(Vector2 position, ITurretController controller)
        {
            Debug.WriteLine("Object turret is being constructed");
            basePosition = position;

            this.controller = controller;
            this.controller.ControllableTurret = this;
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

            gun = new DoubleBarelledWeapon(GunPosition, 14f, 70f, null);
            gun.LoadContent(contentManager);
        }

        public void Update(GameTime time)
        {
            controller.Update(time);
           
            Gun.Rotation = this.Rotation;
            Gun.Update(time);
        }
    }
}
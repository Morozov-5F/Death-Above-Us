using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using devalpha.Weapons.Bullets;

namespace devalpha.Weapons
{
    public abstract class Weapon
    {
        protected Bullet bulletType;
        protected float reloadMeter;
        /// <summary>
        /// The reload time in milliseconds.
        /// </summary>
        protected float reloadTime;
        protected Vector2 position;
        protected float rotation;

        public float Rotation { get { return this.rotation; } set { this.rotation = value;} }

        /// <summary>
        /// Loads the content for bullets
        /// </summary>
        /// <param name="Content">Content.</param>
        public abstract void LoadContent(ContentManager Content);
        /// <summary>
        /// Creates the shot.
        /// </summary>
        /// TODO: КОСТЫЛЬ КОСТЫЛЬНЫЙ!!! ПЕРЕДЕЛАТЬ ПОД ИВЕНТЫ И ЧТОБЫ НИЧЕГО НЕ ВОЗВРАЩАЛ
        public abstract List<Bullet> CreateShot();
        /// <summary>
        /// Draws special weapon effects
        /// </summary>
        /// <param name="time">Game Time.</param>
        public abstract void Draw(GameTime time);
        /// <summary>
        /// ???
        /// </summary>
        /// <param name="time">Game Time.</param>
        public abstract void Update(GameTime time);
    }

    /// <summary>
    /// Bullet data event arguments.
    /// </summary>
    public class BulletDataEventArgs : EventArgs
    {
        public List<Bullet> bulletsToAdd;
    }

    /// <summary>
    /// Shot made event handler.
    /// </summary>
    public delegate void ShotMadeEventHandler(object sender, BulletDataEventArgs args);
}
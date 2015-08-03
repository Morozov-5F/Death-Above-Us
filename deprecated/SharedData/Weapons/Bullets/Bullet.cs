using System;

using devalpha.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace devalpha.Weapons.Bullets
{
    public class Bullet : CollidableObject
    {
        private Vector2 offset;

        public Bullet(Vector2 position, float rotation, Texture2D texture)
        {
            Position = position;
            Rotation = rotation;
            this.Texture = texture;

            offset = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override bool CheckCollision(CollidableObject obj)
        {
            if (obj is Asteroid)
            {
                var target = obj as Asteroid;
                float radius = (float)Math.Sqrt(obj.Texture.Width * obj.Texture.Width / 4 + obj.Texture.Height * obj.Texture.Height / 4) * Camera.GameScale;
                var targetBS = new BoundingSphere(new Vector3(target.Position, 0), radius * target.Scale.X);
               
                Vector2 offset    = new Vector2(texture.Width / 2, texture.Height / 2) * Camera.GameScale,
                        translate = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation)),
                        minPoint  = (position - offset) * translate,
                        maxPoint  = (position + offset) * translate;

                var selfBB = new BoundingBox(new Vector3(minPoint, 0), new Vector3(maxPoint, 0));
                bool ret = selfBB.Intersects(targetBS);
                if (ret)
                {
                    target.Color = Color.Red;
                }
                return ret;
            }  
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, null, offset, Rotation, Vector2.One, Color.White, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            position += 10f * Vector2.One * new Vector2((float)Math.Sin(Rotation),
                -(float)Math.Cos(Rotation));
			
        }
    }
}


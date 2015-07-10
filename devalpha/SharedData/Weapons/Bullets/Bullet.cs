using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace devalpha.Weapons.Bullets
{
    public class Bullet : CollidableObject
    {
        public Bullet(Vector2 position, float rotation, Texture2D texture)
        {
            Position = position;
            Rotation = rotation;
            this.Texture = texture;
        }

        public override bool CheckCollision(CollidableObject obj)
        {
            throw new NotImplementedException();   
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, null, new Vector2(), Rotation, Vector2.One, Color.White, SpriteEffects.None, 0);
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


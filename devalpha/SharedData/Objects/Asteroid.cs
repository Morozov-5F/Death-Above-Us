using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace devalpha.Objects
{
	public class Asteroid : CollidableObject
	{
		private Vector2 origin;
		// По сути, это масса астероида, объединенная с, размером
		private Vector2 sizeScale;
        private float layerDepth;
		private float rotationSpeed;
        private Color color;
        private Color [] AVAILIABLE_COLORS = { Color.White, Color.RosyBrown, Color.Gray, Color.DarkSeaGreen, Color.LightGray, 
            Color.BlanchedAlmond, Color.DarkGoldenrod};

        public Asteroid (Texture2D texture, Vector2 position, Vector2 velocity, float layerDepth)
		{   
			Random generator = new Random ();

			this.texture = texture;
			origin = new Vector2 (Texture.Width / 2, Texture.Height / 2);
            this.layerDepth = layerDepth;
            color = AVAILIABLE_COLORS[generator.Next(0, AVAILIABLE_COLORS.Length)];

			rotation = 0f;
			Position = position;
			this.velocity = Vector2.Normalize(velocity);

			sizeScale = Vector2.One * generator.Next (1, 4) / 2;
            this.velocity *= new Vector2(1.0f / sizeScale.X, 1.0f / sizeScale.Y) * generator.Next(1, 4);
            int reverseRotation = generator.Next(0, 2);
            rotationSpeed = this.velocity.Length () * 0.01f * ((reverseRotation == 1) ? -1 : 1);
		}

		public override bool CheckCollision (CollidableObject obj)
		{
			return this.Bounds.Intersects (obj.Bounds);
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
            spriteBatch.Draw (Texture, Position, null, null, origin, rotation, sizeScale, color, SpriteEffects.None, layerDepth);
		}

		public override void LoadContent (ContentManager contentManager)
		{
//			// TODO: Случайный выбор текстуры
//			Random generator = new Random ();
//			int textureNumber = generator.Next (0, 2);
//
//			Texture = contentManager.Load<Texture2D> ("asteroids/" + textureNumber.ToString ());
//			Нужен ли вообще этот метод здесь?
		}

		public override void Update (GameTime gameTime)
		{
			// я не уверен, на то ли домножаю, но вроде верно
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Position += velocity;
			Rotation += rotationSpeed;
		}
	}
}


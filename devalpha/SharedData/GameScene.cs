using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using devalpha.Objects;
using Microsoft.Xna.Framework.Content;


namespace devalpha
{
	public class GameScene
	{
		SpriteBatch spriteBatch;
		GraphicsDeviceManager graphics;
        ContentManager Content;
        Turret player;

		public GameScene(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, ContentManager content)
		{
			this.spriteBatch = spriteBatch;
			this.graphics = graphics;
            this.Content = content;
            player = new Turret(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height - 28 * Camera.DrawScale) * Camera.GameScale);
            player.Scale = new Vector2(1f, 1f) * (graphics.GraphicsDevice.Viewport.Height / 2560f); 
		}

		public void Load()
		{
            player.LoadContent(Content);
		}

		public void Draw()
		{
            player.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime)
		{
            player.Update(gameTime);
		}
	}
}


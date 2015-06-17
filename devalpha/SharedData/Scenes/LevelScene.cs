using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using devalpha.Objects;

namespace devalpha.Scenes
{
    public class LevelScene : Scene
    {
        private Turret player;
        private Background background;

        public LevelScene(GraphicsDeviceManager graphics) : base(graphics)
        {
            background = new Background(1);
            player = new Turret(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height - 28 * Camera.DrawScale) * Camera.GameScale);
            player.Scale = new Vector2(1f, 1f) * (graphics.GraphicsDevice.Viewport.Height / 2560f); 
        }

        public override void LoadContent(ContentManager Content)
        {
            background.LoadContent(Content);
            player.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Рисование фона
            background.Draw(spriteBatch);
            // Рисование игрока
            player.Draw(spriteBatch);
        }
    }
}


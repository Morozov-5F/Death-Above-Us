using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using devalpha.Scenes;
using Microsoft.Xna.Framework.Input;
using devalpha;

namespace devalpha.Scenes
{
    public class MenuScene : Scene
    {
        private Background background;

        public MenuScene(GraphicsDeviceManager graphics) : base(graphics)
        {
            // Фон для теста
            background = new Background(1);
        }

        public override void LoadContent(ContentManager Content)
        {
            background.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds >= 3)
            {
                MainGame.sceneManager.LoadScene(new LevelScene(graphics));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
        }
    }
}


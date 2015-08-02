using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace devalpha.Scenes
{
    public class SceneManager
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private ContentManager Content;

        public Scene currentScene;

        public SceneManager(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, ContentManager Content)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.Content = Content;
        }

        public void UnloadCurrentScene()
        {
            if (currentScene != null)
            {
                //currentScene.Unload();
            }
            currentScene = null;
        }

        public void LoadScene(Scene scene)
        {
            UnloadCurrentScene();
            currentScene = scene;
            currentScene.LoadContent(Content);
        }

        public void Draw()
        {
            if (currentScene != null)
            {
                currentScene.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentScene != null)
            {
                currentScene.Update(gameTime);
            }
        }
    }
}


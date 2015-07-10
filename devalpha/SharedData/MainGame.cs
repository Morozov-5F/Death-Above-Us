#region Using Statements
using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using devalpha.Objects;
using devalpha.Scenes;

#endregion

namespace devalpha
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static SceneManager sceneManager;
        public static Vector2 screenSize;
	
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			//#if __IOS__ 
			//#endif
            graphics.IsFullScreen = true;	
			#if !__MOBILE__
			graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 650;
            IsMouseVisible = true;
			#endif
			TouchPanel.EnabledGestures = GestureType.HorizontalDrag | GestureType.Tap;
            Debug.WriteLine("Touch panel gestures enabled: " + TouchPanel.EnabledGestures);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Инициализация камеры
            Camera.Position = Vector2.Zero;
            Camera.Rotation = 0;
            Camera.DrawScale = graphics.GraphicsDevice.Viewport.Height / 900f;
            Camera.DeviceViewport = graphics.GraphicsDevice.Viewport;

            screenSize = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height) * Camera.GameScale;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
		{
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: Загрузка контента, который потребуется во всей игре
			
            // Создание SceneManager'а
            sceneManager = new SceneManager(spriteBatch, graphics, Content);

            // Загрузка первой сцены
            sceneManager.LoadScene(new MenuScene(graphics));
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            #if !__IOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            #endif
            // Обновление сцены
            sceneManager.Update(gameTime);			
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
			graphics.GraphicsDevice.Clear(Color.LightGray);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.TransformMatrix);
            // Отрисовка сцены
			sceneManager.Draw();
			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


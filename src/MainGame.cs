#region Using Statements
using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using devalpha.Objects;

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

        private Turret player;
        private Background bg;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	  
            graphics.IsFullScreen = true;		

            TouchPanel.EnabledGestures = GestureType.HorizontalDrag;

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
            Camera.Position = Vector2.Zero;
            Camera.Rotation = 0;
            Camera.GameScale = graphics.GraphicsDevice.Viewport.Height / 900f; 
            Camera.DeviceViewport = graphics.GraphicsDevice.Viewport;

            bg = new Background(1);
            player = new Turret(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height));
            player.Scale = new Vector2(1f, 1f) * (graphics.GraphicsDevice.Viewport.Height / 2560f); 

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
            player.LoadContent(this.Content);
            bg.LoadContent(this.Content);
            //TODO: use this.Content to load your game content here 
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
            player.Update(gameTime);
            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.WhiteSmoke);
		
            //TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Camera.TransformMatrix);
//            spriteBatch.Draw(testTexture, new Vector2(1100, player.Position.Y - 200) - Camera.Position / Camera.GameScale * 0.5f, Color.White);
//            spriteBatch.Draw(tree, new Vector2(1300, player.Position.Y - 100) - Camera.Position / Camera.GameScale * 0.3f, Color.White);
           
            bg.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


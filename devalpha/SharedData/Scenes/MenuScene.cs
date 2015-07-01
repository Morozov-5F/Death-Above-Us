using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using devalpha;
using devalpha.UI;
using devalpha.Scenes;
	
namespace devalpha.Scenes
{
    public class MenuScene : Scene
    {
        private Vector2 backgroundScale;
        private Texture2D background1;
        private Texture2D background2;
        private Vector2 skyPosition;

        private SpriteFont menuFont;
        private String[] menuButtons;
        private float menuButtonsOffset;
        private float menuButtonsSpace;

        private Texture2D logoTexture;
        private Vector2 logoScale;

        private Button testButton;
        int count;

        public MenuScene(GraphicsDeviceManager graphics) : base(graphics)
        {
            // Тестовые кнопки для красоты
            menuButtons = new String[]{@"Новая игра", @"Бесконечный режим", @"Настройки", @"Об игре"};
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i] = menuButtons[i].ToUpper();
            }

            testButton = new Button();
            testButton.Position = new Vector2(0f, 0f);
            testButton.Size = new Vector2(110f, 30f);
            testButton.setClickHandler(onClick);
            testButton.Text = "testButton: 0";
            count = 0;
        }


        public void onClick()
        {
            count++;
            testButton.Text = "testButton: " + count.ToString();
        }

        public override void LoadContent(ContentManager Content)
        {
            // Фон меню
            background1 = Content.Load<Texture2D>("menu/bg1");
            background2 = Content.Load<Texture2D>("menu/bg2");
            // Масштаб фона
            backgroundScale = new Vector2(graphics.GraphicsDevice.Viewport.Height / (float) background1.Bounds.Height * Camera.GameScale);
            // Позиция слоя с небом
            skyPosition = Vector2.Zero;

            // Логотип
            logoTexture = Content.Load<Texture2D>("menu/logo");
            logoScale = new Vector2(graphics.GraphicsDevice.Viewport.Width / (float) logoTexture.Bounds.Width * Camera.GameScale);

            // Шрифт для кнопок меню
            menuFont = Content.Load<SpriteFont>("fonts/menu_font");

            menuButtonsOffset = MainGame.screenSize.Y / 2f - ((menuFont.MeasureString(menuButtons[0]).Y + menuButtonsSpace) * menuButtons.Length - menuButtonsSpace) / 2f;
            menuButtonsSpace = 20f * Camera.GameScale;

            testButton.Font = menuFont;
        }

        public override void Update(GameTime gameTime)
        {
            testButton.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            // Движение слоя неба
            skyPosition -= new Vector2(3f * deltaTime * Camera.GameScale);

            // Переход на следующий экран по клику
			#if !__MOBILE__
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed /*|| gameTime.TotalGameTime.Seconds >= 3*/)
            {
                MainGame.sceneManager.LoadScene(new LevelScene(graphics));
            }    
			#endif
			#if __MOBILE__
			var gesture = default(GestureSample);

			while (TouchPanel.IsGestureAvailable)
				gesture = TouchPanel.ReadGesture();
			if (gesture.GestureType == GestureType.Tap)
			{
				MainGame.sceneManager.LoadScene(new LevelScene(graphics));
			}
			#endif
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Фон
            spriteBatch.Draw(background1, skyPosition, null, Color.White, 0, Vector2.Zero, backgroundScale * 2f, SpriteEffects.None, 1f);
            spriteBatch.Draw(background2, Vector2.Zero, null, Color.White, 0, Vector2.Zero, backgroundScale, SpriteEffects.None, 1f);

            // Лого
            spriteBatch.Draw(logoTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, logoScale, SpriteEffects.None, 1f);

            // Кнопки меню
            for (int i = 0; i < menuButtons.Length; i++)
            {
                var buttonSize = menuFont.MeasureString(menuButtons[i]);
                var buttonPos = new Vector2(MainGame.screenSize.X / 2f - buttonSize.X / 2f, menuButtonsOffset + (buttonSize.Y + menuButtonsSpace) * i);
				spriteBatch.DrawString(menuFont, menuButtons[i], buttonPos, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            testButton.Draw(spriteBatch);
        }
    }
}


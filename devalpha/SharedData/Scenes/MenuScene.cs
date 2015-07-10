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
        private SpriteFont menuFont;
        private Button[] menuButtons;
        private float menuButtonsOffset;
        private float menuButtonsSpace;

        private Texture2D logoTexture;
        private Vector2 logoScale;

        private MenuBackground background;

        private string[] buttonsTexts = { @"Новая игра", @"Бесконечный режим", @"Настройки", @"Об игре" };

        public MenuScene(GraphicsDeviceManager graphics) : base(graphics)
        {
            background = new MenuBackground();
        }

        public void onClick(Button button)
        {
            Debug.WriteLine("Pressed: " + button.Text);
            switch (button.Text)
            {
                case @"Новая игра": // "Новая игра"
                    MainGame.sceneManager.LoadScene(new LevelScene(graphics));
                    break;
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            // Фон
            background.LoadContent(Content);

            // Логотип
            logoTexture = Content.Load<Texture2D>("menu/logo");
            logoScale = new Vector2(graphics.GraphicsDevice.Viewport.Width / (float) logoTexture.Bounds.Width * Camera.GameScale);

            // Шрифт для кнопок меню
            menuFont = Content.Load<SpriteFont>("fonts/menu_font");

            menuButtonsOffset = MainGame.screenSize.Y / 2f - ((menuFont.MeasureString(buttonsTexts[0]).Y + menuButtonsSpace) * buttonsTexts.Length - menuButtonsSpace) / 2f;
            menuButtonsSpace = 20f * Camera.GameScale;
            menuButtons = new Button[buttonsTexts.Length];
            for (int i = 0; i < buttonsTexts.Length; i++)
            {
                menuButtons[i] = new Button(buttonsTexts[i], menuFont);
                var buttonSize = menuFont.MeasureString(buttonsTexts[i]);
                menuButtons[i].Position = new Vector2(MainGame.screenSize.X / 2f - buttonSize.X / 2f, menuButtonsOffset + (buttonSize.Y + menuButtonsSpace) * i);
                menuButtons[i].Size = buttonSize;
                menuButtons[i].setClickHandler(onClick);
                menuButtons[i].Text = buttonsTexts[i];
            }
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            background.Update(deltaTime);

            // Кнопки меню
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Фон
            background.Draw(spriteBatch);

            // Лого
            spriteBatch.Draw(logoTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, logoScale, SpriteEffects.None, 0f);

            // Кнопки меню
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].Draw(spriteBatch);
            }
        }
    }
}


using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using devalpha.Scenes;
using Microsoft.Xna.Framework.Input;
using devalpha;
using System.Diagnostics;


namespace devalpha.UI
{
    public class Button : ISprite
    {
        protected Action    clickHandler;
        protected Rectangle boundsRectangle;
        protected float     rotation;
        protected Texture2D backgroundTexture;

        protected String    state;

        public float      Rotation { get { return this.rotation; } set { this.rotation = value; } }
        public Vector2    Position { get { return new Vector2(boundsRectangle.X, boundsRectangle.Y); } set { boundsRectangle.X = (int) value.X; boundsRectangle.Y = (int) value.Y; } }
        public Texture2D  Texture  { get { return this.backgroundTexture;  } }
        public Vector2    Size     { get { return new Vector2(boundsRectangle.Width, boundsRectangle.Height); } set { boundsRectangle.Width = (int) value.X; boundsRectangle.Height = (int) value.Y; } }
        public String     Text;
        public SpriteFont Font;

        public Button()
        {
            state = "normal";
        }

        public void setClickHandler(Action handlerFunction)
        {
            clickHandler = handlerFunction;
        }

        public void LoadContent(ContentManager contentManager)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            switch (state)
            {
                case "hover":
                    color = new Color(255, 0, 0);
                    break;
                   
                case "click":
                    color = new Color(0, 255, 0);
                    break;
            }
            spriteBatch.DrawString(Font, Text, Position, color, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            // TODO: Проверка клика, а не зажатия кнопки
            if (boundsRectangle.Contains(mouseState.Position))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && clickHandler != null && state != "click")
                {
                    clickHandler();
                    state = "click";
                }
                else if (mouseState.LeftButton != ButtonState.Pressed)
                {
                    state = "hover";
                }
            }   
            else
            {
                state = "normal";
            }
        }
            
    }
}

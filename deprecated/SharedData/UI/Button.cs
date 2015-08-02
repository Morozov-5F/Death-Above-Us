using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using devalpha.Scenes;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using devalpha;
using System.Diagnostics;


namespace devalpha.UI
{
    public class Button : ISprite
    {
        protected Action<Button>    clickHandler;
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

        public Color NormalColor;
        public Color HoverColor;
        public Color ClickColor;

        public Button(String text, SpriteFont font)
        {
            state = "normal";
            Text = text;
            Font = font;

            NormalColor = new Color(240, 240, 240);
            HoverColor = new Color(255, 240, 0);
            ClickColor = new Color(255, 255, 255);
        }

        public void setClickHandler(Action<Button> handlerFunction)
        {
            clickHandler = handlerFunction;
        }

        public void LoadContent(ContentManager contentManager)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {           
            var color = NormalColor;
            switch (state)
            {
                case "hover":
                    color = HoverColor;
                    break;

                case "click":
                    color = ClickColor;
                    break;
            }
            spriteBatch.DrawString(Font, Text, Position, color, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }


        public void Update(GameTime gameTime)
        {
            // Клик или там
            Point cursorPosition = Point.Zero;
            bool isDown = false;

            #if !__MOBILE__
            var mouseState = Mouse.GetState();
            cursorPosition = new Point((int)(mouseState.Position.X * Camera.GameScale), (int) (mouseState.Position.Y * Camera.GameScale));
            isDown = mouseState.LeftButton == ButtonState.Pressed; 
            #endif

            #if __MOBILE__
            var gesture = default(GestureSample);
            while (TouchPanel.IsGestureAvailable)
            gesture = TouchPanel.ReadGesture();
            if (gesture.GestureType == GestureType.Tap)
            {
                cursorPosition = new Point((int)(gesture.Position.X * Camera.GameScale), (int) (gesture.Position.Y * Camera.GameScale));
                isDown = true;
            }
            #endif

            if (boundsRectangle.Contains(cursorPosition))
            {
                if (isDown && clickHandler != null && state != "click")
                {
                    clickHandler(this);
                    state = "click";
                }
                else if (!isDown)
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

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace devalpha
{
    public class Background
    {
        private Texture2D[] textures;
        private Vector2 texturePos;
        private float[]     depths;
        private Int16 levelNo;
        private Vector2 scale;

        public Int16 Level { get { return this.levelNo; } }

        public Background(Int16 Level)
        {
            this.levelNo = Level;
//            this.texturePos = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager content)
        {
//          TODO: reading from file
            int n = 6;

            this.textures = new Texture2D[n];
            this.depths   = new float[n];

            for (int i = 0; i < n; ++i)
            {
                textures[i] = content.Load<Texture2D>(@"Backgrounds\" + levelNo.ToString() + @"\bg" + i.ToString());
                // Временно
                depths[i]   = 1.0f - i * 0.1f;
            }
            depths[n - 1] = 0f;
            this.texturePos = new Vector2(Camera.DeviceViewport.Width - textures[0].Width, 0) * Camera.GameScale / 2f;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < textures.Length; ++i)
            {
                spriteBatch.Draw(textures[i], texturePos - Camera.Position / Camera.GameScale * depths[i],
                    null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, depths[i]); 
            }
        }
    }
}


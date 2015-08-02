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
                string bgPath = @"levels\" + levelNo.ToString() + @"\background\bg" + i.ToString();
                textures[i] = content.Load<Texture2D>(bgPath);
                // Временно
                depths[i]   = (1f - i * 0.1f);
            }
//            depths[0] = 1;
            depths[n - 1] = 0f;
            this.texturePos = new Vector2(Camera.DeviceViewport.Width * Camera.GameScale / 2f, 0) ;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < textures.Length; ++i)
            {
                spriteBatch.Draw(textures[i], texturePos - Camera.Position * Camera.GameScale * depths[i],
                    null, null, new Vector2(1350f, 0), 0, null, Color.White, SpriteEffects.None, depths[i]); 
            }
        }
    }
}


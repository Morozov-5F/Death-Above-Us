using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace devalpha
{
    public class MenuBackground
    {
        // Speed
        private const float BACKGROUND_SCROLLING_SPEED    = 15f;
        private const float SKY_MOVEMENT_SPEED            = 0.2f;

        // Background layers
        private const int   BACKGROUND_LAYERS_COUNT = 4;
        private Texture2D[] bgTextures;
        // От дальнего слоя к ближнему
        private float[]     bgDepths = {0.4f, 0.6f, 0.7f, 1f};
        private Vector2[]   bgPositions;

        // Sky
        private Texture2D[] skyTextures;
        // Позиция звезд
        private Vector2     skyPosition;

        // Projectiles
        struct Projectile
        {
            public Vector2 position;
            public float scale;
            public float rotation;
            public float speed;
            public bool isActive;
        }
        // Максимальное количество снарядов на экране
        private const int       MAX_PROJECTILES_COUNT = 5;
        private Texture2D       projectileTexture;
        private Projectile[]    projectiles;
        // Задержка до создания следующего снаряда
        private const float     MAX_PROJECTILE_DELAY = 2f;
        private float           projectileDelay;

        // Random generator
        private Random random;

        public MenuBackground()
        {
            random = new Random(1);
        }

        public void LoadContent(ContentManager content)
        {
            // Sky
            skyPosition = Vector2.Zero;
            skyTextures = new Texture2D[2];
            skyTextures[0] = content.Load<Texture2D>(@"menu\sky1");
            skyTextures[1] = content.Load<Texture2D>(@"menu\sky2");

            // Background layers
            bgTextures = new Texture2D[BACKGROUND_LAYERS_COUNT];
            bgPositions = new Vector2[BACKGROUND_LAYERS_COUNT];

            for (int i = 0; i < BACKGROUND_LAYERS_COUNT; ++i)
            {
                string texturePath = @"menu\bg" + (i + 1).ToString();
                bgTextures[i] = content.Load<Texture2D>(texturePath);
                bgPositions[i] = Vector2.Zero;
            }

            // Projectile
            projectileTexture = content.Load<Texture2D>(@"menu\projectile");
            projectiles = new Projectile[MAX_PROJECTILES_COUNT];
            for (int i = 0; i < MAX_PROJECTILES_COUNT; i++)
            {
                projectiles[i].position = Vector2.Zero;
                projectiles[i].rotation = -(float) Math.PI / 4f;
                projectiles[i].scale = 1f;
                projectiles[i].isActive = false;
                projectiles[i].speed = 0f;
            }

            projectileDelay = 1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Sky
            spriteBatch.Draw(skyTextures[1], Vector2.Zero, null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, 0f); 
            spriteBatch.Draw(skyTextures[0], skyPosition, null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, 0f); 
            spriteBatch.Draw(skyTextures[0], skyPosition + new Vector2(0f, skyTextures[0].Height), null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, 0f); 

            // Projectiles            
            for (int i = 0; i < MAX_PROJECTILES_COUNT; i++)
            {
                if (projectiles[i].isActive)
                {
                    spriteBatch.Draw(projectileTexture, projectiles[i].position, null, Color.White, projectiles[i].rotation, 
                        Vector2.Zero, new Vector2(projectiles[i].scale), SpriteEffects.None, 0f);
                }
            }

            // Background layers
            for (int i = 0; i < BACKGROUND_LAYERS_COUNT; i++)
            {
                Vector2 layerPosition = bgPositions[i] - Camera.Position * Camera.GameScale;
                spriteBatch.Draw(bgTextures[i], layerPosition,
                    null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, 0f); 

                layerPosition += new Vector2(bgTextures[i].Width, 0f);
                spriteBatch.Draw(bgTextures[i], layerPosition,
                    null, null, Vector2.Zero, 0, null, Color.White, SpriteEffects.None, 0f); 
            }
        }

        public void Update(float deltaTime)
        {
            // Sky
            skyPosition -= new Vector2(0f, SKY_MOVEMENT_SPEED);
            if (skyPosition.Y < -skyTextures[0].Height)
            {
                skyPosition += new Vector2(0f, skyTextures[0].Height);
            }

            // Background layers
            for (int i = 0; i < BACKGROUND_LAYERS_COUNT; i++)
            {
                bgPositions[i] -= new Vector2(deltaTime * bgDepths[i] * BACKGROUND_SCROLLING_SPEED, 0f);
                if (bgPositions[i].X < -bgTextures[i].Width)
                {
                    bgPositions[i] += new Vector2(bgTextures[i].Width, 0f);
                }
            }

            // Projectiles
            int newIndex = -1;
            for (int i = 0; i < MAX_PROJECTILES_COUNT; i++)
            {
                if (projectiles[i].isActive)
                {
                    Vector2 velocity = new Vector2((float)Math.Cos(projectiles[i].rotation), (float)Math.Sin(projectiles[i].rotation)) * projectiles[i].speed;
                    projectiles[i].position += velocity;
                    if (projectiles[i].position.Y < 0)
                    {
                        projectiles[i].isActive = false;
                        newIndex = i;
                    }
                }
                else
                {
                    newIndex = i;
                }
            }

            projectileDelay -= deltaTime;
            if (projectileDelay < 0 && newIndex >= 0)
            {
                projectileDelay = MAX_PROJECTILE_DELAY * (float)random.NextDouble() + 0.01f;

                projectiles[newIndex].isActive = true;
                float xOffsetRandom = ((float)random.NextDouble() - 0.2f) * bgTextures[0].Width * 0.5f;
                projectiles[newIndex].position = new Vector2(xOffsetRandom, bgTextures[0].Height + projectileTexture.Height * 2f);
                float rotationRandom = ((float)random.NextDouble() - 0.5f) * (float)Math.PI / 5f;
                projectiles[newIndex].rotation = -(float)Math.PI / 4f + rotationRandom;
                projectiles[newIndex].speed = 20f;

                projectiles[newIndex].scale = (float)random.NextDouble() * 0.5f + 0.8f;
                projectiles[newIndex].speed *= projectiles[newIndex].scale;
            }

        }
    }
}


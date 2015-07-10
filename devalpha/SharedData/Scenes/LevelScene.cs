using System;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using devalpha.Objects;
using devalpha.Weapons;
using devalpha.Weapons.Bullets;

namespace devalpha.Scenes
{
    public class LevelScene : Scene
    {
		private const int MAX_ASTEROIDS_AT_SPAWN = 10;
		private const int MIN_ASTEROIDS_AT_SPAWN = 1;
		private const int ASTEROIDS_SPAWN_POINTS = 3;

		private Turret player;
		private Background background;

        private List<Bullet> bullets;

		private List<Asteroid> asteroids;
		private Texture2D[] asteroidTextures;

        public LevelScene(GraphicsDeviceManager graphics) : base(graphics)
        {
            background = new Background(1);
            player = new Turret(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height - 28 * Camera.DrawScale) * Camera.GameScale);
            //player.Scale = new Vector2(1f, 1f) * (graphics.GraphicsDevice.Viewport.Height / 2560f); 
        	
            // TODO: конфигурационный файл с количеством астероидов? 
			asteroidTextures = new Texture2D[2];
			
            asteroids = new List<Asteroid>();
            bullets = new List<Bullet>();
		}

        public override void LoadContent(ContentManager Content)
        {
            background.LoadContent(Content);
            player.LoadContent(Content);
//            Чето не работает
//            player.Gun.ShotMadeEvent += OnWeaponShot;
			for (int i = 0; i < 2; ++i)
			{
				asteroidTextures[i] = Content.Load<Texture2D>(@"asteroids\" + (i + 1).ToString()); 
			}
			SpawnAsteroids();
        }

        public override void Update(GameTime gameTime)
        {
			// DEBUG: спавн астероидов по клику мыши
			#if !__MOBILE__
			var mouseState = Mouse.GetState();
            if (mouseState.RightButton == ButtonState.Pressed)
			{
//				SpawnAsteroids();
               
			}
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                List<Bullet> bulletsToAdd = player.Gun.CreateShot();
                if (bulletsToAdd != null)
                {
                    foreach (var cb in bulletsToAdd)
                    {
                        bullets.Add(cb);
                    }
                }
            }
			#endif

            player.Update(gameTime);
            foreach (var currentBullet   in bullets)
            {
                currentBullet.Update(gameTime);
            }
            foreach (var currentAsteroid in asteroids)
			{
				currentAsteroid.Update(gameTime);
			}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Рисование фона
            background.Draw(spriteBatch);
//             Рисование игрока
            foreach (var currentBullet in bullets)
            {
                currentBullet.Draw(spriteBatch);
            }
            foreach (var currentAsteroid in asteroids)
			{
				currentAsteroid.Draw(spriteBatch);
			}
			player.Draw(spriteBatch);
        }

        public void OnWeaponShot(object sender, BulletDataEventArgs args)
        {
            Debug.WriteLine("Получено пуль: " + args.bulletsToAdd.Count);
            foreach (var cb in args.bulletsToAdd)
            {
                bullets.Add(cb);
            }
        }

		public void SpawnAsteroids()
		{
			Random generator = new Random ();
			int asteroidCount = generator.Next (MIN_ASTEROIDS_AT_SPAWN, MAX_ASTEROIDS_AT_SPAWN + 1);
			// Выбираем область спавна и размер точки спавна
			int spawnArea      = generator.Next(0, ASTEROIDS_SPAWN_POINTS + 1), 
				spawnAreaSize  = (int)(Camera.DeviceViewport.Width * Camera.GameScale / ASTEROIDS_SPAWN_POINTS);
			Debug.WriteLine("Количество астероидов для спавна: " + asteroidCount.ToString());
			Debug.WriteLine("Астероиды спавнятся в регионе номер " + spawnArea.ToString());
			for (int i = 0; i < asteroidCount; ++i) 
			{
				int asteroidTextureNo = generator.Next(0, 2);
				// Устанавливаем позицию где-то за экраном, в одной из областей возможного спавна
				float posX = generator.Next((int)((spawnArea - 0.1f) * spawnAreaSize), 
					             (int)((spawnArea + 0.1f) * spawnAreaSize)) * Camera.GameScale,
				posY = generator.Next((int)(-200 * Camera.GameScale), (int)(-100 * Camera.GameScale));
				Vector2 position = new Vector2(posX, posY);
				// Устанавливаем скорость к земле
				float velocityDirectionX = generator.Next(Camera.DeviceViewport.Width / 7, Camera.DeviceViewport.Width - Camera.DeviceViewport.Width / 7);
				Vector2 velocity = new Vector2(velocityDirectionX, 
					Camera.DeviceViewport.Height) * Camera.GameScale - position;
				// Создаем астероид
				var asteroidToAdd = new Asteroid(asteroidTextures[asteroidTextureNo], position, 
					velocity, 0.3f);
				asteroids.Add(asteroidToAdd);
			}
		}
    }
}


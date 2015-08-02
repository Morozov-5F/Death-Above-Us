using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace devalpha
{
    public interface ISprite
    {
        float      Rotation { get; set; }
        Vector2    Position { get; set; }
        Texture2D  Texture  { get; }

        void Draw       (SpriteBatch spriteBatch);
        void Update     (GameTime time);
        void LoadContent(ContentManager manager);
    }
}


using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace devalpha
{
    public static class Camera
    {
        public static Vector2  Position { get; set;  }
        public static float    Rotation { get; set; }
        public static float    GameScale { get; set; }
        public static Viewport DeviceViewport { get; set; }

        public static Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(GameScale) *
                    Matrix.CreateTranslation(Position.X, Position.Y, 0);
            }
        }
    }
}


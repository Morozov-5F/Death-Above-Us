using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace devalpha
{
    public static class Camera
    {
        private static float drawScale;

        public static Vector2  Position { get; set;  }
        public static float    Rotation { get; set; }
        public static float    GameScale { get; private set; }
        public static Viewport DeviceViewport { get; set;}
        public static float    DrawScale 
        {   
            get             
            {
                return drawScale;
            }
            set 
            { 
                drawScale = value;
                GameScale = 1 / value;
            } 
        }

        public static Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(DrawScale) *
                    Matrix.CreateTranslation(Position.X, Position.Y, 0);
            }
        }
    }
}


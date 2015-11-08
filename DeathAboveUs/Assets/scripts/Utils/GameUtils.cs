using UnityEngine;
using System.Collections;

public static class GameUtils
{
    // Вычисляется в GameInitializationController.Start()
    public static float cameraWidth;
    public static float cameraHeight;

    public const int PIXELS_IN_UNITS = 64;

    public static float WrapAngle(float angle)
    {
        while (angle <= -180)
            angle += 360f;
        while (angle > 180f)
            angle -= 360f;
        return angle;
    }
}

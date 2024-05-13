using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    // Checkpoint info
    public static List<Vector3> points;
    public static int curPoint;
    public static int nextPoint;
    public static float distance;

    // Status check
    public static bool errCheck = false;
    public static bool starting = false;
    public static bool isGaming = false;
    public static bool isMoving = false;
    public static bool crashed = false;
    public static bool reachCheck = false;

    // Right hand gestures
    public static bool rotateLeft = false;
    public static bool rotateRight = false;
    public static bool rotateUp = false;
    public static bool rotateDown = false;

    // Gaming data
    public static Vector3 flightDir;
}

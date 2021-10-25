using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainGame
{
    public static bool isGameStarted = false;
    public static bool isRubikShuffled = false;
    public static bool lockedRotation = false;
    public static Vector3 cubeSelected;
    public static int rubikWidth = 3;
    public static int rubikHeight = 3;
    public static int rubikDepth = 3;
    public static bool IsAlmostEqual(float f1, float f2)
    {
        float epsilon = 0.001f;
        return Math.Abs(f1-f2) <= epsilon;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
public static class Functions
{
    public static float Cube(float x)
    {
        return x * x * x;
    }

    public static float Square(float x)
    {
        return x * x;
    }
    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    public static float MultWave(float x, float t)
    {
        float y = Sin(PI * (x + t));
        y += 0.5f * Sin(2f * PI * (x + 0.5f * t));
        y *= (2f/3f);
        return y;
    }

    public static float Ripple(float x, float t)
    {
        float d = Abs(x);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}

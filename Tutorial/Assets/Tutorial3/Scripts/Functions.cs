using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
public static class Functions
{

    public delegate Vector3 Function(float x, float z, float t);
    public enum FunctionName { Square, Wave, MultWave, Cubic, Ripple, Sphere, Torus };

    static Function[] functions = { Square, Wave, MultiWave, Cube, Ripple, Sphere, Torus };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }
    public static Vector3 Cube(float u, float v, float t)
    {
        Vector3 output;
        output.x = u;
        output.y = u * u * u + v * v * v;
        output.z = v;
        return output;
    }

    public static Vector3 Square(float u, float v, float t)
    {
        Vector3 output;
        output.x = u;
        output.y = u * u + v * v;
        output.z = v;
        return output;
    }
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 output;
        output.x = u;
        output.y = Sin(PI * (u + v + t));
        output.z = v;
        return output;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 output;
        output.x = u;
        output.y = Sin(PI * (u + 0.5f * t));
        output.y += 0.5f * Sin(2f * PI * (v + t));
        output.y += Sin(PI * (u + v + 0.25f * t));
        output.y *= 1f / 2.5f;
        output.z = v;
        return output;
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        Vector3 output;
        output.x = u;
        output.y = Sin(PI * (4f * d - t));
        output.y /= 1f + 10f * d;
        output.z = v;
        return output;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 output;
        output.x = s * Sin(PI * u + t);
        output.y = r * Sin(PI * 0.5f * v);
        output.z = s * Cos(PI * u + t);
        return output;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 output;
        output.x = s * Sin(PI * u);
        output.y = r2 * Sin(PI * v);
        output.z = s * Cos(PI * u);
        return output;
    }

    public static FunctionName GetNextFunction(FunctionName name)
    {
        return (int)name < functions.Length - 1 ? name + 1 : 0;
    }

    public static Vector3 Morph(float u, float v, float t, Function from, Function to, float progress)
    {
        return Vector3.LerpUnclamped(from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress));
    }
}

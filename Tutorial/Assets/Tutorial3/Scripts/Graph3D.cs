using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph3D : MonoBehaviour
{
    public enum GraphType { Quadratic, Wave, MultWave, Cubic , Ripple};

    [SerializeField]
    GraphType type = GraphType.Wave;

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    Transform[] points;
    public void Awake()
    {
        points = new Transform[resolution];

        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.one * 2f / resolution;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);

            point.SetParent(transform, false);
            pos.x = (i + 0.5f) * 2f / resolution - 1f;
            pos.y = Evaluate(pos.x, 0);
            point.localPosition = pos;
            point.localScale = scale;

            points[i] = point;
        }
    }

    private void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Evaluate(position.x, time);
            point.localPosition = position;
        }
    }

    public float Evaluate(float x, float t)
    {
        float y = 0;
        switch(type)
        {
            case GraphType.Cubic:
                {
                    y = Functions.Cube(x);
                    break;
                }
            case GraphType.Quadratic:
                {
                    y = Functions.Square(x);
                    break;
                }
            case GraphType.Wave:
                {
                    y = Functions.Wave(x, t);
                    break;
                }
            case GraphType.MultWave:
                {
                    y = Functions.MultWave(x, t);
                    break;
                }
            case GraphType.Ripple:
                {
                    y = Functions.Ripple(x, t);
                    break;
                }
            default:
                {
                    break;
                }
        }
        return y;
    }

    public void ClearGraph()
    {

        List<Transform> points = new List<Transform>();

        foreach(Transform child in transform)
        {
            points.Add(child);
        }

        if (points.Count > 0)
        {
            foreach (Transform child in points)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }
    }
 
}

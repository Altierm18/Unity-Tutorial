using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Graph : MonoBehaviour
{

    public enum GraphType { Quadratic, Sine, Cosine, Linear, Cubic };

    [SerializeField]
    GraphType type = GraphType.Quadratic;

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 500)]
    int resolution = 10;

    Transform[] points;
    public void MakeGraph()
    {
        points = new Transform[resolution];

        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.one * 2f / resolution;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            points[i] = point;

            point.SetParent(transform, false);
            pos.x = (i + 0.5f) * 2f / resolution - 1f;

            pos.y = Evaluate(pos.x);

            point.localPosition = pos;
            point.localScale = scale;
        }
    }

    private float Evaluate(float input)
    {
        float output;
        switch (type)
        {
            case GraphType.Quadratic:
                {
                    output = input * input;
                    break;
                }
            case GraphType.Sine:
                {
                    output = Mathf.Sin(input * Mathf.PI);
                    break;
                }
            case GraphType.Cosine:
                {
                    output = Mathf.Cos(input * Mathf.PI);
                    break;
                }
            case GraphType.Cubic:
                {
                    output = input * input * input;
                    break;
                }
            default:
                {
                    output = input;
                    break;
                }

        }
        return output;
    }
    /* //Very Buggy
    private void Update()
    {
        float time = Time.time;
        for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Evaluate(position.x + time);
            point.localPosition = position;
        }
    }*/ 
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

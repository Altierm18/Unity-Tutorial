using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph3D : MonoBehaviour
{

    [SerializeField]
    Functions.FunctionName function;

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    Transform[] points;
    public void Awake()
    {
        points = new Transform[resolution * resolution];

        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.one * 2f / resolution;
        for (int i = 0; i < points.Length; i++)
        {

            Transform point = Instantiate(pointPrefab);

            point.SetParent(transform, false);

            point.localPosition = pos;
            point.localScale = scale;

            points[i] = point;
        }
    }

    private void Update()
    {
        Functions.Function evaluate = Functions.GetFunction(function);

        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = evaluate(u, v, time);
        }
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

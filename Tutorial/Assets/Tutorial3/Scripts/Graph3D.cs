using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph3D : MonoBehaviour
{

    [SerializeField]
    Functions.FunctionName function = default;

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    Transform[] points;

    float duration;

    bool transitioning;
    Functions.FunctionName transitionFunction;

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
        duration += Time.deltaTime;
        if (transitioning)
        {
            if (duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if (duration >= functionDuration)
        {
            duration -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            function = Functions.GetNextFunction(function);
            
        }

        if (transitioning)
        {
            UpdateFunctionTransition();
        }
        else
        {
            UpdateFunction();
        }
    }

    
    void UpdateFunctionTransition()
    {
        Functions.Function from = Functions.GetFunction(transitionFunction);
        Functions.Function to = Functions.GetFunction(function);
        float progress = duration / transitionDuration;

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
            points[i].localPosition = Functions.Morph(u, v, time, from, to, progress);
        }
    }

    void UpdateFunction()
    {
        Functions.Function eval = Functions.GetFunction(function);
        float progress = duration / transitionDuration;

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
            points[i].localPosition = eval(u, v, time);
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

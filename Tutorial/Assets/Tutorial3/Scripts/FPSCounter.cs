using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField, Range(0.1f, 2f)]
    float durationLength;

    int frames;
    float duration;

    private void Update()
    {
        float frameLength = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameLength;
        if (duration > durationLength)
        {
            display.SetText("FPS\n{0:0}\n000\n000", frames / duration);
            frames = 0;
            duration = 0;
        }
    }
}

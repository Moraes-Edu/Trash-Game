using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] Text text;
    float fps;
    void Start()
    {
        InvokeRepeating(nameof(GetFps), 1, 1);
    }

    void GetFps()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        text.text = fps + " fps";
    }
}

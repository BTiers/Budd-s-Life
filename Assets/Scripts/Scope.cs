using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [Tooltip("Value inside this array are defined in terms of camera FoV.")]
    public float[] zoom;
    public int price;
    public Sprite image { get; }

    private long activeZoom = 0;
    private float getZoom()
    {
        float curZoom = zoom[activeZoom];
        activeZoom = (activeZoom + 1) % zoom.Length;

        return curZoom;
    }

}

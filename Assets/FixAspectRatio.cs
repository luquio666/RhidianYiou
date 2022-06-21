using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAspectRatio : MonoBehaviour
{
    private const int _pixelWidth = 160;
    private const int _pixelHeight = 144;

    private const int _pixelMultiplier = 4;

    void Start()
    {
        Screen.SetResolution(_pixelWidth * _pixelMultiplier, _pixelHeight * _pixelMultiplier, false);
    }
}

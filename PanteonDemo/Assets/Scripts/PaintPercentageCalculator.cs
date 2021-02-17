using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PaintPercentageCalculator : MonoBehaviour
{
    [HideInInspector]
    public float Percentage;

    private InkCanvas _inkCanvas;
    private Texture _texture;
    private RenderTexture _paintedTexture;
    private Texture2D _texture2D;
    private Texture2D _paintedTexture2D;

    private int _totalPixelCount;

    private void Start()
    {
        _inkCanvas = gameObject.GetComponent<InkCanvas>();
        _paintedTexture = _inkCanvas.GetPaintMainTexture(gameObject.name);
        _paintedTexture2D = new Texture2D(512, 512, TextureFormat.RGB24, false);
        
    }

    private void LateUpdate()
    {
        if (_texture == null)
            SetTexture();

        SetPaintedTexture();

        Percentage = ((100 * GetRedPixelCount()) / _totalPixelCount);
        UIController.Instance.PercentageText.text = String.Format("{0}%", Percentage);
    }

    public int GetRedPixelCount()
    {
        var pixels = _paintedTexture2D.GetPixels();
        var redPixelCount = 0;

        foreach (var color in pixels)
        {
            if (color.r > 0.7f && color.g < 0.2f && color.b < 0.2f)
                redPixelCount++;
            else
                continue;
        }

        return redPixelCount;
    }

    private void SetTexture()
    {
        _texture = _inkCanvas.GetMainTexture(gameObject.name);
        _texture2D = (Texture2D)_texture;
        _totalPixelCount = _texture2D.width * _texture2D.height;
    }

    private void SetPaintedTexture()
    {
        if (_paintedTexture != null)
            _paintedTexture2D = ConvertToTexture2D(_paintedTexture);
    }

    private Texture2D ConvertToTexture2D(RenderTexture rTex)
    {
        RenderTexture.active = rTex;
        _paintedTexture2D.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        _paintedTexture2D.Apply();
        return _paintedTexture2D;
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;

public class FogOfWar : MonoBehaviour
{
    [Tooltip("Size of the Fog of War texture. Should be a power of 2.")]
    [SerializeField]
    private int textureSize = 1024;

    [SerializeField]
    private Color fogOfWarColor = Color.black;

    private Texture2D texture;
    private Color[] texturePixels;
    private int pixelsPerUnit;
    private Vector2 centerPixel;

    private List<FogOfWarRevealer> fogOfWarRevealers;

    private void Awake()
    {
        fogOfWarRevealers = new List<FogOfWarRevealer>();

        texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texturePixels = texture.GetPixels();
        GetComponent<Renderer>().material.mainTexture = texture;

        pixelsPerUnit = Mathf.RoundToInt(textureSize / transform.lossyScale.x);
        centerPixel = new Vector2(textureSize * 0.5f, textureSize * 0.5f);

        ClearFogOrWar();
    }

    private void Update()
    {
        foreach (FogOfWarRevealer revealer in fogOfWarRevealers)
        {
            Vector2 positionOnFogOfWar = GetPositionOnFogOfWar(revealer);

            DrawFogOfWarView(positionOnFogOfWar,
                             revealer.Radius,
                             Color.clear);
        }

        DrawFogOfWar();
    }

    public void RegisterRevealer(FogOfWarRevealer revealer)
    {
        fogOfWarRevealers.Add(revealer);
    }

    public void UnregisterRevealer(FogOfWarRevealer revealer)
    {
        fogOfWarRevealers.Remove(revealer);
    }

    private void ClearFogOrWar()
    {
        for (var i = 0; i < texturePixels.Length; i++)
        {
            texturePixels[i] = fogOfWarColor;
        }
    }

    private void DrawFogOfWarView(Vector2 position, int radius, Color color)
    {
        for (int x = 0; x <= radius; x++)
        {
            int distanceFromCenter = (int) Mathf.Ceil(Mathf.Sqrt(radius * radius - x * x));
            for (int y = 0; y <= distanceFromCenter; y++)
            {
                int px = (int) position.x + x;
                int nx = (int) position.x - x;
                int py = (int) position.y + y;
                int ny = (int) position.y - y;

                texturePixels[py * texture.width + px] = color;
                texturePixels[py * texture.width + nx] = color;
                texturePixels[ny * texture.width + px] = color;
                texturePixels[ny * texture.width + nx] = color;
            }
        }
    }

    private void DrawFogOfWar()
    {
        texture.SetPixels(texturePixels);
        texture.Apply(false);
    }

    private Vector2 GetPositionOnFogOfWar(FogOfWarRevealer revealer)
    {
        Vector3 positionOnFogOfWar = revealer.transform.position - transform.position;
        return new Vector2(positionOnFogOfWar.x * pixelsPerUnit + centerPixel.x,
                           positionOnFogOfWar.y * pixelsPerUnit + centerPixel.y);
    }
}
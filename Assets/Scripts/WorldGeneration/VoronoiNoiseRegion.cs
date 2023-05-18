using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiNoiseRegion : MonoBehaviour
{
    public int size;
    public int regionAmount;
    public int regionColorAmount;
    public List<Color> myRegionColors = new List<Color>();
    public float scale = 12;
    public int octaves = 4;
    public float persistance = 0.1f;
    public float lacunarity = 2.2f;

    public static class PerlinNoise
    {
        public static float GenerateBiomeNoise(int x, int y, float scale, int octaves, float persistance, float lacunarity)
        {
            float perlinValue = new float();
            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 1;

            for (int i = 0; i < octaves; i++)
            {
                float posX = x / scale * frequency;
                float posY = y / scale * frequency;

                perlinValue = Mathf.PerlinNoise(posX, posY) * 2 - 1;
                noiseHeight += perlinValue * amplitude;
                amplitude *= persistance;
                frequency *= lacunarity;
            }

            perlinValue = noiseHeight;
            perlinValue = Mathf.InverseLerp(-0.5f, 2f, perlinValue);

            return perlinValue;
        }
    }

    private void Start()
    {
        CreateVoronoiDiagram();
    }

    public void CreateVoronoiDiagram()
    {
        Vector2[] points = new Vector2[regionAmount];
        Color[] regionColors = new Color[regionColorAmount];

        for (int i = 0; i < regionAmount; i++)
        {
            points[i] = new Vector2(Random.Range(0, size), Random.Range(0, size));
        }

        for (int i = 0; i < regionColorAmount; i++)
        {
            regionColors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }

        for (int i = 0; i < myRegionColors.Count; i++)
        {
            regionColors[i] = myRegionColors[i];
        }

        Color[] pixelColors = new Color[size * size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = float.MaxValue;
                int value = 0;
                float perlinValue = PerlinNoise.GenerateBiomeNoise(x, y, scale, octaves, persistance, lacunarity);

                for (int i = 0; i < regionAmount; i++)
                {
                    if (Vector2.Distance(new Vector2(x, y), points[i]) < distance)
                    {
                        distance = Vector2.Distance(new Vector2(x, y), points[i]);
                        value = i;
                    }
                }

                int closestRegionIndex = 0;
                float distanceRegion = float.MaxValue;
                for (int i = 0; i < regionAmount; i++)
                {
                    if (i != value)
                    {
                        if (Vector2.Distance(new Vector2(x, y), points[i]) < distanceRegion)
                        {
                            distanceRegion = Vector2.Distance(new Vector2(x, y), points[i]);
                            closestRegionIndex = i;
                        }
                    }
                }

                if (distanceRegion - distance < value)
                {
                    if (perlinValue < 0.5f)
                    {
                        pixelColors[x + y * size] = regionColors[value % regionColorAmount];
                    }
                    else
                    {
                        pixelColors[x + y * size] = regionColors[closestRegionIndex % regionColorAmount];
                    }
                }
                else
                {
                    pixelColors[x + y * size] = regionColors[value % regionColorAmount];
                }
            }
        }

        for (int i = 0; i < regionAmount / 2; i++)
        {
            for (int amount = 1; amount <= 2; amount++)
            {
                for (int y = amount; y < size - amount; y++)
                {
                    for (int x = amount; x < size - amount; x++)
                    {
                        if (pixelColors[(x + amount) + y * size] != pixelColors[x + y * size] && pixelColors[(x - amount) + y * size] != pixelColors[x + y * size] ||
                        pixelColors[x + (y + amount) * size] != pixelColors[x + y * size] && pixelColors[x + (y - amount) * size] != pixelColors[x + y * size])
                        {
                            pixelColors[x + y * size] = pixelColors[(x + amount) + y * size];
                        }
                    }
                }
            }
        }

        Texture2D myTexture = new Texture2D(size, size);
        myTexture.filterMode = FilterMode.Point;
        myTexture.SetPixels(pixelColors);
        myTexture.Apply();

        GetComponent<Renderer>().material.mainTexture = myTexture;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("World Settings")]
    public int size;
    public int regionAmount;
    public int regionColorAmount;
    public float scale = 12;
    public int octaves = 4;
    public float persistance = 0.1f;
    public float lacunarity = 2.2f;
    public BiomeSettings biomeSettings;
    
    private BiomeData[] biomeData;

    private void Start()
    {
        biomeData = biomeSettings.Biomes;
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

        Color[] pixelColors = new Color[size * size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = float.MaxValue;
                int value = 0;

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

                float perlinValue = Perlin.GenerateNoiseMap(x, y, 1283, biomeData[value % biomeData.Length].perlinParams);
                //float perlinValue = Perlin.GenerateNoiseMap(x, y, 123, new PerlinParams(scale, octaves, persistance, lacunarity));

                bool useBiomeData = false;
                for (int j = 0; j < biomeData[value % biomeData.Length].colorLayers.Length; j++)
                {
                    if (perlinValue >= biomeData[value % biomeData.Length].colorLayers[j % biomeData[value % biomeData.Length].colorLayers.Length].range.x &&
    perlinValue <= biomeData[value % biomeData.Length].colorLayers[j % biomeData[value % biomeData.Length].colorLayers.Length].range.y)
                    {
                        pixelColors[x + y * size] = biomeData[value % biomeData.Length].colorLayers[j].layerColor;
                        useBiomeData = true;
                        break; // Exit the loop after assigning the color
                    }
                }

                if (!useBiomeData)
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
            }
        }

        Texture2D myTexture = new Texture2D(size, size);
        myTexture.filterMode = FilterMode.Point;
        myTexture.SetPixels(pixelColors);
        myTexture.Apply();

        GetComponent<Renderer>().material.mainTexture = myTexture;
    }
}
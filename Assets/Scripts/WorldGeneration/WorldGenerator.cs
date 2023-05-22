using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int worldSize = 1024;
    [SerializeField] private int regionAmount = 10;
    [SerializeField] private BiomeSettings biomeSettings;
    private BiomeData[] biomeData;
    private Vector2[] points;
    private Color[] pixelColors;
    private Texture2D myTexture;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        biomeData = biomeSettings.Biomes;
        points = GenerateRandomPoints();
        pixelColors = new Color[worldSize * worldSize];
        myTexture = new Texture2D(worldSize, worldSize);
        myTexture.filterMode = FilterMode.Point;
        CreateVoronoiDiagram();
        ApplyTextureToMaterial();
    }

    private void CreateVoronoiDiagram()
    {
        int biomeDataLength = biomeData.Length;

        for (int y = 0; y < worldSize; y++)
        {
            for (int x = 0; x < worldSize; x++)
            {
                int value = FindClosestPointIndex(new Vector2(x, y));
                int closestRegionIndex = FindClosestRegionIndex(new Vector2(x, y), value);
                float perlinValue = Perlin.GenerateNoiseMap(x, y, 100, biomeData[value % biomeData.Length].perlinParams);
                bool useBiomeData = AssignPixelColorWithBiomeData(value, perlinValue, x, y, biomeDataLength);

                if (!useBiomeData)
                    pixelColors[x + y * worldSize] = GetPixelColorBasedOnPerlinValue(perlinValue, value, closestRegionIndex);
            }
        }

        myTexture.SetPixels(pixelColors);
        myTexture.Apply();
    }

    private Vector2[] GenerateRandomPoints()
    {
        Vector2[] points = new Vector2[regionAmount];
        int textureSizeMinusOne = worldSize - 1;

        for (int i = 0; i < regionAmount; i++)
        {
            points[i] = new Vector2(Random.Range(0, worldSize), Random.Range(0, worldSize));
        }

        return points;
    }

    private int FindClosestPointIndex(Vector2 position)
    {
        int value = 0;
        float distance = float.MaxValue;

        for (int i = 0; i < regionAmount; i++)
        {
            float dist = Vector2.SqrMagnitude(position - points[i]);
            if (dist < distance)
            {
                distance = dist;
                value = i;
            }
        }

        return value;
    }

    private int FindClosestRegionIndex(Vector2 position, int excludeIndex)
    {
        int closestRegionIndex = 0;
        float distanceRegion = float.MaxValue;

        for (int i = 0; i < regionAmount; i++)
        {
            if (i != excludeIndex)
            {
                float dist = Vector2.SqrMagnitude(position - points[i]);
                if (dist < distanceRegion)
                {
                    distanceRegion = dist;
                    closestRegionIndex = i;
                }
            }
        }

        return closestRegionIndex;
    }

    private bool AssignPixelColorWithBiomeData(int value, float perlinValue, int x, int y, int biomeDataLength)
    {
        var biome = biomeData[value % biomeDataLength];

        float perlinNoise = VoronoiNoiseRegion.PerlinNoise.GenerateBiomeNoise(x, y, biome.perlinParams.scale, biome.perlinParams.octaves, biome.perlinParams.persistence, biome.perlinParams.lacunarity);

        foreach (var colorLayer in biome.colorLayers)
        {
            if (perlinNoise >= colorLayer.range.x && perlinNoise <= colorLayer.range.y)
            {
                pixelColors[x + y * worldSize] = colorLayer.layerColor;
                return true;
            }
        }

        return false;
    }

    private Color GetPixelColorBasedOnPerlinValue(float perlinValue, int value, int closestRegionIndex)
    {
        return perlinValue < 0.5f ? Color.white : Color.black;
    }

    private void ApplyTextureToMaterial()
    {
        material.mainTexture = myTexture;
    }
}
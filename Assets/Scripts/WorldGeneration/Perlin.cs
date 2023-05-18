using UnityEngine;

[System.Serializable]
public struct PerlinParams
{
    [SerializeField] private float _scale;
    [SerializeField] private int _octaves;
    [SerializeField] private float _persistence;
    [SerializeField] private float _lacunarity;
    [SerializeField] private Vector2 _offset;

    public PerlinParams(float scale = 20, int octaves = 4, float persistence = 0.1f, float lacunarity = 2.2f, Vector2 offset = default)
    {
        this._scale = scale;
        this._octaves = octaves;
        this._persistence = persistence;
        this._lacunarity = lacunarity;
        this._offset = offset;
    }

    public float scale => _scale;
    public int octaves => _octaves;
    public float persistence => _persistence;
    public float lacunarity => _lacunarity;
    public Vector2 offset => _offset;
}
public static class Perlin
{
    public static float GenerateNoiseMap(int x, int y, int seed, PerlinParams param)
    {
            float perlinValue = new float();
            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 1;

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[param.octaves];

        for (int i = 0; i < param.octaves; i++)
        {
            float X = prng.Next(-100000, 100000) + param.offset.x;
            float Y = prng.Next(-100000, 100000) + param.offset.y;
            octaveOffsets[i] = new Vector2(X, Y);
        }

        for (int i = 0; i < param.octaves; i++)
        {
            float sampleX = (x + octaveOffsets[i].x) / param.scale * frequency;
            float sampleY = (y + octaveOffsets[i].y) / param.scale * frequency;

            perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
            noiseHeight += perlinValue * amplitude;
            amplitude *= param.persistence;
            frequency *= param.lacunarity;
        }

        perlinValue = noiseHeight;
        perlinValue = Mathf.InverseLerp(-0.5f, 2f, perlinValue);

        return perlinValue;
    }
}

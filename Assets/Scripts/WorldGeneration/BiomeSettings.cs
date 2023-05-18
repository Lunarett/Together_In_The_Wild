using UnityEngine;
using GD.MinMaxSlider;

[System.Serializable]
public struct HeightColorLayer
{
    [SerializeField] private string _layerName;
    [SerializeField] private Color _layerColor;
    [SerializeField] [MinMaxSlider(-2.0f, 2.0f)] private Vector2 _range;

    public string Name { get => _layerName; }
    public Color layerColor { get => _layerColor; } // the color of the layer
    public Vector2 range { get => _range; } // holds height ranges for where the color should be applied
}

[System.Serializable]
public struct BiomeData
{
    [SerializeField] private string _biomeName;

    [Header("Terrain Settings")]
    [SerializeField] PerlinParams _perlinParams;
    [SerializeField] HeightColorLayer[] _colorLayers;

    public PerlinParams perlinParams { get => _perlinParams; }
    public HeightColorLayer[] colorLayers { get => _colorLayers; }
}

[CreateAssetMenu(fileName = "BiomeSettings", menuName = "World Settings/Biome Settings", order = 1)]
public class BiomeSettings : ScriptableObject
{
    [Header("General Biome Properties")]
    [SerializeField] private int _biomeSize = 40;
    [SerializeField] private PerlinParams _biomeShape; 
    [Space(20)]
    [SerializeField] private BiomeData[] _biomeData;
    
    public BiomeData[] Biomes { get => _biomeData; }
    public int BiomeSize { get => _biomeSize; }
    public int BiomeCount { get => _biomeData.Length + 1; }
    public PerlinParams PerlinParams { get => _biomeShape; }
}

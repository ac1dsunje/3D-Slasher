using UnityEngine;

namespace _Game.Scripts.Biomes
{
public class BiomeManager: MonoBehaviour
{
    [SerializeField] private Biome[] _biomes;
    [SerializeField] private float _noiseScale = 15f;
    [SerializeField] private int _seed;
    public void Awake()
    {
        if (_seed != 0) return;
        _seed = Random.Range(0, 99999999);
    }

    public Biome GetBiomeAt(Vector2Int chunkGridPos)
    {
        if (_biomes.Length == 1) return _biomes[0];

        var x = chunkGridPos.x + _seed * 0.13f;
        var y = chunkGridPos.y + _seed * 0.27f;

        var noise = Mathf.PerlinNoise(x / _noiseScale, y / _noiseScale);

        var index = Mathf.FloorToInt(noise * _biomes.Length);
        index = Mathf.Clamp(index, 0, _biomes.Length - 1);
        return _biomes[index];
    }    
}
}
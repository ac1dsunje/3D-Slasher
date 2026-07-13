using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Biomes
{
[CreateAssetMenu(fileName = "BiomeSelector", menuName = "Game/World/BiomeSelector")]
public class BiomeSelector : ScriptableObject
{
    [field: SerializeField] public Biome[] Biomes { get; private set; } 
    [field: SerializeField] public float NoiseScale { get; private set; } = 50f;
    [field: SerializeField] public int Seed { get; private set; }

    public void Awake()
    {
        if (Seed != 0) return;
        Seed = Random.Range(0, 99999999);
    }

    public Biome GetBiomeAt(Vector2Int chunkGridPos)
    {
        if (Biomes.Length == 1) return Biomes[0];

        var x = chunkGridPos.x + Seed * 0.13f;
        var y = chunkGridPos.y + Seed * 0.27f;

        var noise = Mathf.PerlinNoise(x / NoiseScale, y / NoiseScale);

        var index = Mathf.FloorToInt(noise * Biomes.Length);
        index = Mathf.Clamp(index, 0, Biomes.Length - 1);
        return Biomes[index];
    }
}
}
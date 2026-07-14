using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes
{
[CreateAssetMenu(fileName = "NewBiome", menuName = "Game/World/Biome")]
public class Biome : ScriptableObject
{
    [field: SerializeField] public string BiomeName { get; private set; }
    [field: SerializeField] public BiomeContent[] Contents { get; private set; }
    [field: SerializeField] public int SpecialChance { get; private set; } = 30;
    [field: SerializeField] public  int EnvironmentChance { get; private set; } = 50;
    [field: SerializeField] public int BuildingChance { get; private set; } = 10;
}
}
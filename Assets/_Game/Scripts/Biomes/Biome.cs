using UnityEngine;

namespace _Game.Scripts.Biomes
{
[CreateAssetMenu(fileName = "NewBiome", menuName = "Game/World/Biome")]
public class Biome : ScriptableObject
{
    [field: SerializeField] public string BiomeName { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; } = Color.white;
}
}
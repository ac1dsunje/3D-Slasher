using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes
{
[CreateAssetMenu(fileName = "NewBiome", menuName = "Game/World/Biome")]
public class Biome : ScriptableObject
{
    [field: SerializeField] public string BiomeName { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; } = Color.white;

    [field: SerializeField] public GameObject[] Buildings { get; private set; }
    [field: SerializeField] public GameObject[] Environments { get; private set; }
    [field: SerializeField] public  int EnvChance { get; private set; } = 50;
    [field: SerializeField] public GameObject[] SpecialObjects { get; private set; }
    [field: SerializeField] public GameObject[] Enemies { get; private set; }
    [field: SerializeField] public AudioClip[] Music { get; private set; }
}
}
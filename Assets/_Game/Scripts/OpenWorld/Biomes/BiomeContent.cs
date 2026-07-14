using _Game.Scripts.OpenWorld.Biomes.Structures;
using UnityEngine;
using _Game.Scripts.OpenWorld.Sun;

namespace _Game.Scripts.OpenWorld.Biomes
{
[CreateAssetMenu(fileName = "New Biome Content", menuName = "Game/World/Biome Content")]
public class BiomeContent: ScriptableObject
{
    [field: SerializeField] public SunFaces Type { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; } = Color.white;
    [field: SerializeField] public StructureController[] Buildings { get; private set; }
    [field: SerializeField] public StructureController[] Environments { get; private set; }
    [field: SerializeField] public StructureController[] SpecialObjects { get; private set; }
    [field: SerializeField] public GameObject[] Enemies { get; private set; }
    [field: SerializeField] public AudioClip[] Music { get; private set; }
}
}
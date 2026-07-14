using System;
using _Game.Scripts.OpenWorld.Sun;
using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes.Structures
{
[Serializable]
public class StructureData
{
    [field: SerializeField] public SunFaces Type { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
}
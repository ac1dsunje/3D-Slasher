using UnityEngine;

namespace _Game.Scripts.OpenWorld.Sun
{
[CreateAssetMenu(fileName = "New Sun Face", menuName = "Game/World/Sun Face")]
public class SunFace: ScriptableObject
{
    [field: SerializeField] public SunFaces Type { get; private set; }
    [field: SerializeField] public string FaceName { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; } = Color.white;
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int MinDuration { get; private set; } = 1;
    [field: SerializeField] public int MaxDuration { get; private set; } = 5;
}
}
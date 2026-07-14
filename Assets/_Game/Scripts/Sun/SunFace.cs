using UnityEngine;

namespace _Game.Scripts.Sun
{
[CreateAssetMenu(fileName = "NewSunFace", menuName = "Game/World/Sun Face")]
public class SunFace : ScriptableObject
{
    [field: SerializeField] public string FaceName { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; } = Color.white;
    [field: SerializeField] public float MinIntensity { get; private set; } = 1.0f;
    [field: SerializeField] public float MaxIntensity { get; private set; } = 1.0f;
}
}
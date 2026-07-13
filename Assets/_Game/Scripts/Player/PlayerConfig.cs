using UnityEngine;

namespace _Game.Scripts.Player
{
[CreateAssetMenu(menuName = "Game/Player/Config")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
}
}
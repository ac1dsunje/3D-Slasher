using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private void Awake()
    {
        _player.transform.position = new Vector3(0, 1, 0);
    }
}
}

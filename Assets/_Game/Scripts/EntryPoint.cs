using _Game.Scripts.Biomes;
using _Game.Scripts.Biomes.Chunks;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ChunkManager _chunkManager;
    [SerializeField] private BiomeManager _biomeManager;
    [SerializeField] private CameraController _cameraController;

    private void Awake()
    {
        _cameraController.Construct(_player.transform);
        _chunkManager.Construct(_player.transform, _biomeManager);
        _player.transform.position = new Vector3(0, 1, 0);
    }
}
}

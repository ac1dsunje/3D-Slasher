using _Game.Scripts.Biomes;
using _Game.Scripts.Biomes.Chunks;
using _Game.Scripts.Player;
using _Game.Scripts.Sun;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ChunkManager _chunkManager;
    [SerializeField] private BiomeManager _biomeManager;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private LightController _lightController;
    [SerializeField] private SunsManager _sunsManager;

    private void Awake()
    {
        _cameraController.Construct(_player.transform);
        _chunkManager.Construct(_player.transform, _biomeManager);
        _player.transform.position = new Vector3(0, 1, 0);
        _sunsManager.Construct(_lightController);
    }
}
}

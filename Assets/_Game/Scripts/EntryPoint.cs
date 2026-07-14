using _Game.Scripts.OpenWorld.Biomes;
using _Game.Scripts.OpenWorld.Biomes.Chunks;
using _Game.Scripts.OpenWorld.Sun;
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
    [SerializeField] private LightController _lightController;
    [SerializeField] private SunsManager _sunsManager;
    [SerializeField] private TimeController _timeController;

    private void Awake()
    {
        _cameraController.Construct(_player.transform);
        _chunkManager.Construct(_player.transform, _biomeManager);
        _lightController.Construct(_timeController);
        _sunsManager.Construct(_lightController, _timeController);
    }
}
}

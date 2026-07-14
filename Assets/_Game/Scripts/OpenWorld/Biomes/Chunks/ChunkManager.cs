using System.Collections.Generic;
using _Game.Scripts.OpenWorld.Sun;
using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes.Chunks
{
public class ChunkManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ChunkController _chunkPrefab;

    [Header("Settings")]
    [SerializeField] private float _chunkSize = 10f;
    [SerializeField] private int _viewRadius;

    private readonly Dictionary<Vector2Int, ChunkController> _loadedChunks = new();
    private readonly Dictionary<Vector2Int, ChunkController> _unloadedChunks = new();
    
    private readonly List<Vector2Int> _chunksToUnload = new();

    private Transform _player;
    private BiomeManager _biomeManager;
    private SunsManager _sunsManager;
    private SunFaces _type;

    public void Construct(Transform player, BiomeManager biomeManager, SunsManager sunsManager)
    {
        _player = player;
        _biomeManager = biomeManager;
        _sunsManager = sunsManager;

        _sunsManager.OnSunFaceChanged += UpdateSunFace;
    }

    private void Update()
    {
        UpdateChunksAroundPlayer();
    }

    private void UpdateSunFace(SunFace sunFace)
    {
        _type = sunFace.Type;
        foreach (var chunk in _loadedChunks)
        {
            chunk.Value.UpdateSunFace(_type);
        }
    }

    private void UpdateChunksAroundPlayer()
    {
        var playerChunk = WorldToChunkGrid(_player.position);

        for (var x = -_viewRadius; x <= _viewRadius; x++)
        {
            for (var y = -_viewRadius; y <= _viewRadius; y++)
            {
                var gridPos = new Vector2Int(playerChunk.x + x, playerChunk.y + y);

                if (!_loadedChunks.ContainsKey(gridPos))
                {
                    SpawnChunk(gridPos);
                }
            }
        }

        _chunksToUnload.Clear();

        foreach (var loadedChunk in _loadedChunks)
        {
            if (Mathf.Abs(loadedChunk.Key.x - playerChunk.x) > _viewRadius ||
                Mathf.Abs(loadedChunk.Key.y - playerChunk.y) > _viewRadius)
            {
                _chunksToUnload.Add(loadedChunk.Key);
            }
        }

        foreach (var key in _chunksToUnload)
        {
            UnloadChunk(key);
        }
    }

    private void SpawnChunk(Vector2Int gridPos)
    {
        if (_unloadedChunks.Remove(gridPos, out var chunk))
        {
            chunk.gameObject.SetActive(true);
            chunk.UpdateSunFace(_type);
        }
        else
        {
            var biome = _biomeManager.GetBiomeAt(gridPos);
            chunk = Instantiate(_chunkPrefab, transform);
            chunk.Initialize(gridPos, biome, _chunkSize);
            chunk.UpdateSunFace(_type);
        }

        _loadedChunks[gridPos] = chunk;
    }

    private void UnloadChunk(Vector2Int gridPos)
    {
        if (!_loadedChunks.Remove(gridPos, out var chunk)) return;
        
        _unloadedChunks[gridPos] = chunk;
        chunk.gameObject.SetActive(false);
    }

    private Vector2Int WorldToChunkGrid(Vector3 worldPos)
    {
        var x = Mathf.FloorToInt(worldPos.x / _chunkSize);
        var y = Mathf.FloorToInt(worldPos.z / _chunkSize);
        return new Vector2Int(x, y);
    }

    private void OnDestroy()
    {
        _sunsManager.OnSunFaceChanged -= UpdateSunFace;
    }
}
}
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Biomes.Chunks
{
public class ChunkManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ChunkController _chunkPrefab;
    [SerializeField] private BiomeSelector _biomeSelector;

    [Header("Settings")]
    [SerializeField] private float _chunkSize = 10f;
    [SerializeField] private int _viewRadius = 4;

    private readonly Dictionary<Vector2Int, ChunkController> _loadedChunks = new();
    private Transform _player;

    public void Construct(Transform player)
    {
        _player = player;
    }

    private void Update()
    {
        UpdateChunksAroundPlayer();
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
    }

    private void SpawnChunk(Vector2Int gridPos)
    {
        var biome = _biomeSelector.GetBiomeAt(gridPos);

        var chunk = Instantiate(_chunkPrefab, transform);
        chunk.Initialize(gridPos, biome, _chunkSize);

        _loadedChunks[gridPos] = chunk;
    }

    private Vector2Int WorldToChunkGrid(Vector3 worldPos)
    {
        var x = Mathf.FloorToInt(worldPos.x / _chunkSize);
        var y = Mathf.FloorToInt(worldPos.z / _chunkSize);
        return new Vector2Int(x, y);
    }
}
}
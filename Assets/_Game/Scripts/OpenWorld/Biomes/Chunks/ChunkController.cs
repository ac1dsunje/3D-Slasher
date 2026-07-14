using System.Collections.Generic;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes.Chunks
{
public class ChunkController: MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _centralPoint;
    private Biome _biome;
    private Renderer _renderer;
    private readonly List<Transform> _freePoints = new();

    private const float ChunkOffset = 0.5f;

    private void Awake()
    {
        _renderer= GetComponent<Renderer>();
    }

    public void Initialize(Vector2Int gridPos, Biome assignedBiome, float chunkSize)
    {
        _biome = assignedBiome;

        transform.position = new Vector3(
            gridPos.x * chunkSize + chunkSize * ChunkOffset,
            0,
            gridPos.y * chunkSize + chunkSize * ChunkOffset
        );
        GetRandomRotation();
        ApplyBiome();
    }

    private void FillSpawnPossibilities()
    {
        foreach (var point in _points)
        {
            _freePoints.Add(point);
        }
        _freePoints.Add(_centralPoint);
    }

    private void GetRandomRotation()
    {
        var angle = Random.Range(0, 4) * 90;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void ApplyBiome()
    {
        SetBiomeColor();
        
        FillSpawnPossibilities();
        
        CreateEnvironment();
    }

    private void SetBiomeColor()
    {
        var mat = new Material(_renderer.sharedMaterial)
        {
            color = _biome.BaseColor
        };
        
        _renderer.material = mat;
    }

    private void CreateEnvironment()
    {
        if (_biome.Environments.Length == 0) return;
        foreach (var point in _freePoints)
        {
            TrySetRandomEnvironment(point);
        }
    }

    private void TrySetRandomEnvironment(Transform position)
    {
        if (!GetSpawnPossibility(_biome.EnvChance)) return;
        var rand = Random.Range(0, _biome.Environments.Length);
        CreateObject(_biome.Environments[rand], position);
    }

    private bool GetSpawnPossibility(int chance)
    {
        var spawnChance = Random.Range(0, 100);
        return spawnChance >= chance;
    }

    private void CreateObject(GameObject obj, Transform parent)
    {
        Instantiate(obj, parent.position, Quaternion.identity, parent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        TryUpdatePlayerChunk(collision);
    }

    private void TryUpdatePlayerChunk(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        player.TryUpdateBiome(_biome);
    }
}
}
using System.Collections.Generic;
using _Game.Scripts.OpenWorld.Biomes.Structures;
using _Game.Scripts.OpenWorld.Sun;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes.Chunks
{
public class ChunkController: MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _centralPoint;
    [SerializeField] private SunFaces _testType = SunFaces.Clear;
    private Biome _biome;
    private BiomeContent _biomeContent;
    private Renderer _renderer;
    private readonly List<Transform> _freePoints = new();

    private StructureController _building;
    private readonly List<StructureController> _environments = new();
    private readonly List<StructureController> _specialObjects = new();

    private const float ChunkOffset = 0.5f;

    private void Awake()
    {
        _renderer= GetComponent<Renderer>();
    }

    public void Initialize(Vector2Int gridPos, Biome assignedBiome, float chunkSize)
    {
        _biome = assignedBiome;
        _biomeContent = _biome.Contents[0];

        transform.position = new Vector3(
            gridPos.x * chunkSize + chunkSize * ChunkOffset,
            0,
            gridPos.y * chunkSize + chunkSize * ChunkOffset
        );
        GetRandomRotation();
        FillSpawnPossibilities();
        ApplyBiome();
    }

    public void UpdateSunFace(SunFaces type)
    {
        if (type == _biomeContent.Type) return;
        foreach (var content in _biome.Contents)
        {
            if (content.Type != type) continue;
            
            _biomeContent = content;
            UpdateBiomeContent();
            return;
        }
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

        CreateBuilding();
        CreateSpecialObject();
        CreateEnvironment();
    }

    private void UpdateBiomeContent()
    {
        SetBiomeColor();

        _building?.UpdateType(_biomeContent.Type);

        foreach (var specialObject in _specialObjects)
        {
            specialObject.UpdateType(_biomeContent.Type);
        }

        foreach (var environment in _environments)
        {
            environment.UpdateType(_biomeContent.Type);
        }
    }

    private void SetBiomeColor()
    {
        var mat = new Material(_renderer.sharedMaterial)
        {
            color = _biomeContent.BaseColor
        };
        
        _renderer.material = mat;
    }

    private void CreateBuilding()
    {
        if (_biomeContent.Buildings.Length == 0) return;
        if (!GetSpawnPossibility(_biome.BuildingChance)) return;
        var rand = Random.Range(0, _biomeContent.Buildings.Length);
        _building = CreateBiomeStructure(_biomeContent.Buildings[rand], _centralPoint);
        _freePoints.Clear();
    }

    private void CreateSpecialObject()
    {
        if (_biomeContent.SpecialObjects.Length == 0) return;
        if (!GetSpawnPossibility(_biome.SpecialChance)) return;
        var rand = Random.Range(0, _biomeContent.SpecialObjects.Length);
        var randPoint = Random.Range(0, _freePoints.Count);
        var special = CreateBiomeStructure(_biomeContent.SpecialObjects[rand], _points[randPoint]);
        _freePoints.Remove(_points[randPoint]);
        _specialObjects.Add(special);
    }

    private void CreateEnvironment()
    {
        if (_biomeContent.Environments.Length == 0) return;
        foreach (var point in _freePoints)
        {
            TrySetRandomEnvironment(point);
        }
    }

    private void TrySetRandomEnvironment(Transform position)
    {
        if (!GetSpawnPossibility(_biome.EnvironmentChance)) return;
        var rand = Random.Range(0, _biomeContent.Environments.Length);
        var environment = CreateBiomeStructure(_biomeContent.Environments[rand], position);
        _environments.Add(environment);
    }

    private bool GetSpawnPossibility(int chance)
    {
        var spawnChance = Random.Range(0, 100);
        return spawnChance >= chance;
    }

    private StructureController CreateBiomeStructure(StructureController obj, Transform parent)
    {
        var structure = Instantiate(obj, parent.position, Quaternion.identity, parent);
        structure.UpdateType(_biomeContent.Type);
        return structure;
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
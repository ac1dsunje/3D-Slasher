using UnityEngine;

namespace _Game.Scripts.Biomes.Chunks
{
public class ChunkController: MonoBehaviour
{
    [SerializeField] private Biome _biome;
    private Renderer _renderer;

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

        ApplyBiome();
    }

    private void ApplyBiome()
    {
        var mat = new Material(_renderer.sharedMaterial)
        {
            color = _biome.BaseColor
        };
        
        _renderer.material = mat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        TryUpdatePlayerChunk(collision);
    }

    private void TryUpdatePlayerChunk(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player._currentBiome == _biome.BiomeName) return;
        
        Debug.Log($"Player Stepped in a {_biome.BiomeName}!");
        player._currentBiome = _biome.BiomeName;
    }
}
}
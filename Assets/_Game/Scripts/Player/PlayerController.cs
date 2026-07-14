using _Game.Scripts.OpenWorld.Biomes;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;
    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _jumpRequested;
    private Biome _currentBiome;

    public void TryUpdateBiome(Biome newBiome)
    {
        if (Equals(_currentBiome, newBiome)) return;
        
        _currentBiome = newBiome;
        Debug.Log($"Player Stepped in a {_currentBiome.BiomeName} biome!");
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput  = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector3(
            _horizontalInput * _config.MoveSpeed,
            0, 
            _verticalInput * _config.MoveSpeed
        );
    }

    private void Jump()
    {
        if (!_jumpRequested) return;
        _rb.AddForce(Vector3.up * _config.JumpForce, ForceMode.Impulse);
        _jumpRequested = false;
    }
}
}

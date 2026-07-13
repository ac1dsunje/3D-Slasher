using UnityEngine;

namespace _Game.Scripts
{
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;
    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _jumpRequested;
    
    public string _currentBiome;
    
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
        _rb.linearVelocity = new Vector3(
            _horizontalInput * _config.MoveSpeed,
            0, 
            _verticalInput * _config.MoveSpeed
            );

        if (!_jumpRequested) return;
        _rb.AddForce(Vector3.up * _config.JumpForce, ForceMode.Impulse);
        _jumpRequested = false;
    }
}
}

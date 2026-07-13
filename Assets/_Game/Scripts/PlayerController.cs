using UnityEngine;

namespace _Game.Scripts
{
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;
    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput  = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3((_horizontalInput * _config.MoveSpeed), 0, (_verticalInput * _config.MoveSpeed)) ;
    }
}
}

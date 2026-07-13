using UnityEngine;

namespace _Game.Scripts
{
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    
    public void Construct(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        var cam = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var target = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, _target.position.z + _offset.z);
        transform.position = Vector3.Lerp(cam, target, _speed * Time.deltaTime);
    }
}
}

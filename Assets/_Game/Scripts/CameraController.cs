using UnityEngine;

namespace _Game.Scripts
{
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Transform _target;
    
    public void Construct(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        var cam = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var target = new Vector3(_target.position.x, _target.position.y + 4f, _target.position.z -10f);
        transform.position = Vector3.Lerp(cam, target, _speed * Time.deltaTime);
    }
}
}

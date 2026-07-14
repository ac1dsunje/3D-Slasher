using UnityEngine;

namespace _Game.Scripts.OpenWorld.Sun
{
public class LightController : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationOffset = new(-90f, 0f, 0f);

    private Light _light;
    private TimeController _worldTime;
    private SunFace _currentSunFace;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    public void Construct(TimeController timeController)
    {
        _worldTime = timeController;
    }

    private void Update()
    {
        if (!_currentSunFace || !_worldTime)
            return;

        UpdateRotation();
    }

    public void SetSunFace(SunFace sunFace)
    {
        _currentSunFace = sunFace;

        _light.color = sunFace.BaseColor;

        Debug.Log($"{sunFace.FaceName} Sun is watching this world.");
    }

    private void UpdateRotation()
    {
        var angle = _worldTime.NormalizedTime * 360f;

        transform.rotation = Quaternion.Euler(
            angle + _rotationOffset.x,
            _rotationOffset.y,
            _rotationOffset.z);
    }
}
}
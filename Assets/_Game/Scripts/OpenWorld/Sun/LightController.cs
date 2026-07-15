using UnityEngine;

namespace _Game.Scripts.OpenWorld.Sun
{
public class LightController : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationOffset = new(-90f, 0f, 0f);

    private Light _light;
    
    private TimeController _worldTime;
    private SunsManager _sunsManager;
    private SunFace _currentSunFace;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    public void Construct(TimeController timeController, SunsManager sunsManager)
    {
        _worldTime = timeController;
        _sunsManager = sunsManager;
        
        _sunsManager.OnSunFaceChanged += SetSunFace;
    }

    private void Update()
    {
        if (_currentSunFace == null || !_worldTime)
            return;

        UpdateRotation();
    }

    private void SetSunFace(SunFace sunFace)
    {
        _currentSunFace = sunFace;

        _light.color = sunFace.BaseColor;
    }

    private void UpdateRotation()
    {
        var angle = _worldTime.NormalizedTime * 360f;

        transform.rotation = Quaternion.Euler(
            angle + _rotationOffset.x,
            _rotationOffset.y,
            _rotationOffset.z);
    }

    private void OnDestroy()
    {
        _sunsManager.OnSunFaceChanged -= SetSunFace;
    }
}
}
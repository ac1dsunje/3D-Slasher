using UnityEngine;

namespace _Game.Scripts.OpenWorld.Sun
{
public class SunsManager: MonoBehaviour
{
    [SerializeField] private SunFace[] _sunFaces;
    
    private LightController _lightController;
    private TimeController _timeController;
    private int _faceIndex;
    private int _currentFaceDuration;

    public void Construct(LightController lightController, TimeController timeController)
    {
        _timeController = timeController;
        _timeController.OnDayChanged += OnDayChanged;
        
        _lightController = lightController;
        SetNewSunFace(_sunFaces[0]);
    }

    private void OnDestroy()
    {
        _timeController.OnDayChanged -= OnDayChanged;
    }

    private void OnDayChanged(int count)
    {
        _currentFaceDuration--;
        if (_currentFaceDuration > 0) return;
        
        _faceIndex++;
        if (_faceIndex >= _sunFaces.Length)
        {
            _faceIndex = 0;
        }
        SetNewSunFace(_sunFaces[_faceIndex]);
    }

    private void SetNewSunFace(SunFace sunFace)
    {
        _currentFaceDuration = Random.Range(sunFace.MinDuration, sunFace.MaxDuration + 1);
        _lightController.SetSunFace(sunFace);
    }
}
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.OpenWorld.Sun
{
public enum SunFaces
{
    Clear,
    Blood, 
    Acid
}

public class SunsManager: MonoBehaviour
{
    [SerializeField] private SunFace[] _sunFaces;
    
    private LightController _lightController;
    private TimeController _timeController;
    private int _faceIndex;
    private int _currentFaceDuration;

    public event Action<SunFace> OnSunFaceChanged;

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
        OnSunFaceChanged?.Invoke(sunFace);
    }
}
}
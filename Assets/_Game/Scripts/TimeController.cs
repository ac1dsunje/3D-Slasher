using System;
using UnityEngine;

namespace _Game.Scripts
{
public enum TimeState
{
    Midnight,
    Dawn,
    Morning,
    Day,
    Evening,
    Sunset,
    Night
}

public class TimeController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float _dayLengthInSeconds = 600f;
    
    [Header("Current Info")]
    [SerializeField] private int _currentDay = 1;
    [SerializeField] private float _timeOfDay;
    [SerializeField] private TimeState _currentState;
    public float NormalizedTime => _timeOfDay / 24f;

    public event Action<int> OnDayChanged;

    private void Update()
    {
        _timeOfDay += (24f / _dayLengthInSeconds) * Time.deltaTime;
        _currentState = GetCurrentTimeState;

        if (_timeOfDay < 24f) return;
        UpdateDayCount();
    }
    
    private void UpdateDayCount()
    {
        _timeOfDay -= 24f;
        _currentDay++;

        OnDayChanged?.Invoke(_currentDay);
    }
    
    private TimeState GetCurrentTimeState
    {
        get
        {
            return _timeOfDay switch
            {
                >= 0f and < 4f => TimeState.Midnight,
                >= 4f and < 7f => TimeState.Dawn,
                >= 7f and < 12f => TimeState.Morning,
                >= 12f and < 17f => TimeState.Day,
                >= 17f and < 19f => TimeState.Evening,
                >= 19f and < 21f => TimeState.Sunset,
                _ => TimeState.Night
            };
        }
    }
}
}
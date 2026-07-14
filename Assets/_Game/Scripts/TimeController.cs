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
    
    private TimeState _currentState;
    public float NormalizedTime => _timeOfDay / 24f;

    public event Action<int> OnDayChanged;
    public event Action<TimeState> OnTimeStateChanged;

    private void Update()
    {
        _timeOfDay += (24f / _dayLengthInSeconds) * Time.deltaTime;
        _currentState = GetTimeState();

        if (_timeOfDay < 24f) return;
        UpdateDayCount();
    }
    
    private void UpdateDayCount()
    {
        _timeOfDay -= 24f;
        _currentDay++;

        OnDayChanged?.Invoke(_currentDay);
    }

    private TimeState GetTimeState()
    {
        TimeState state;
        switch (_timeOfDay)
        {
            case >= 0f and < 4f: 
                state = TimeState.Midnight;
                break;
            case >= 4f and < 7f: 
                state = TimeState.Dawn;
                break;
            case >= 7f and < 12f:
                state = TimeState.Morning;
                break;
            case >= 12f and < 17f:
                state = TimeState.Day;
                break;
            case >= 17f and < 19f:
                state = TimeState.Evening;
                break;
            case >= 19f and < 21f:
                state = TimeState.Sunset;
                break;
            default:
                state = TimeState.Night;
                break;
        };
        if (state != _currentState)
        {
            OnTimeStateChanged?.Invoke(state);
        }
        return state;
    }
}
}
using UnityEngine;

namespace _Game.Scripts.Sun
{
public class LightController: MonoBehaviour
{
    private Light _light;
    private SunFace _currentSunFace;

    public void SetSunFace(SunFace sunFace)
    {
        _currentSunFace = sunFace;
        _light.color = _currentSunFace.BaseColor;
        Debug.Log($"{sunFace.FaceName} Sun is watching for this world");
    }

    private void Awake()
    {
        _light = GetComponent<Light>();
    }
}
}
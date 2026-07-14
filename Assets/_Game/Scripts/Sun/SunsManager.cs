using UnityEngine;

namespace _Game.Scripts.Sun
{
public class SunsManager: MonoBehaviour
{
    [SerializeField] private SunFace[] _sunFaces;
    
    private LightController _lightController;

    public void Construct(LightController lightController)
    {
        _lightController = lightController;
        _lightController.SetSunFace(_sunFaces[0]);
    }
}
}
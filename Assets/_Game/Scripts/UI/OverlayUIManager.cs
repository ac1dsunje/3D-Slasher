using _Game.Scripts.OpenWorld.Sun;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayUIManager: ScreenManager
{
    [SerializeField] private TextMeshProUGUI _timeStateText;
    [SerializeField] private TextMeshProUGUI _sunFaceText;

    private TimeController _timeController;
    private SunsManager _sunsManager;
    
    public void Construct(TimeController timeController, SunsManager sunsManager)
    {
        _timeController = timeController;
        _sunsManager = sunsManager;

        _timeController.OnTimeStateChanged += SetTimeStateText;
        _sunsManager.OnSunFaceChanged += SetSunFaceText;
    }
    
    private void SetTimeStateText(TimeState timeState)
    {
        _timeStateText.text = timeState.ToString();
    }
    
    private void SetSunFaceText(SunFace sunFace)
    {
        _sunFaceText.text = sunFace.FaceName;
    }

    private void OnDestroy()
    {
        _sunsManager.OnSunFaceChanged -= SetSunFaceText;
        _timeController.OnTimeStateChanged -= SetTimeStateText;
    }
}
}
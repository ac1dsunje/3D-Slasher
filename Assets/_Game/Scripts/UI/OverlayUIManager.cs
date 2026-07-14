using _Game.Scripts.OpenWorld.Sun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
public class OverlayUIManager: ScreenManager
{
    [SerializeField] private TextMeshProUGUI _timeStateText;
    [SerializeField] private Image _sunFaceImage;

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
        _sunFaceImage.sprite = sunFace.Image;
    }

    private void OnDestroy()
    {
        _sunsManager.OnSunFaceChanged -= SetSunFaceText;
        _timeController.OnTimeStateChanged -= SetTimeStateText;
    }
}
}
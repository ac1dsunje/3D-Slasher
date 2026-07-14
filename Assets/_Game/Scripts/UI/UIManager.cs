namespace _Game.Scripts.UI
{
public class UIManager
{
    private ScreenManager _overlay;
    
    public UIManager(GameStateManager gsm, OverlayUIManager overlay)
    {
        _overlay = overlay;
    }
}
}
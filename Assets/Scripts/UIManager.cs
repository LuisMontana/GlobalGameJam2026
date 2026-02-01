using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject maskButtons;
    [SerializeField] private Image stressBar;
    [SerializeField] private Image timerBar;

    public void UpdateTimer(float value)
    {
        timerBar.fillAmount = value;
    }
    
    public void UpdateStressBar(float newValue)
    {
        stressBar.fillAmount = newValue;
    }
    
    public void ToggleMaskButtons(bool newState)
    {
        Button[] buttons = maskButtons.GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.interactable = newState;
        }
    }
}

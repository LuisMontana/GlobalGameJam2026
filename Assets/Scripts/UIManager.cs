using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject maskButtons;
    [SerializeField] private Image stressBar;
    [SerializeField] private Image timerBar;
    [SerializeField] private Transform paperHolder;
    
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
    
    public void FlipPages()
    {
        Transform first = paperHolder.GetChild(0);
        Transform second = paperHolder.GetChild(1);

        first.SetSiblingIndex(1);
        second.SetSiblingIndex(0);
    }

    public void DisablePages()
    {
        paperHolder.gameObject.SetActive(false);
    }
}

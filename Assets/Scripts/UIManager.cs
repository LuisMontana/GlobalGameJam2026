using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject maskButtons;
    [SerializeField] private Image stressBar;

    public void UpdateStressBar(float newValue)
    {
        Debug.Log(stressBar.name + ": " + stressBar.fillAmount);
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

using UnityEngine;
using UnityEngine.UI;

public class stressBarHandler : MonoBehaviour
{
    public Image stressBar;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stressBar.fillAmount = stressBar.fillAmount + Time.deltaTime;
    }
}

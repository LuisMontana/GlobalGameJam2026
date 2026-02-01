using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameOverLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] LevelManager levelManagerScript;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] string sceneName;
    [SerializeField] GameObject restartBtn;

    private bool _gameLost = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (levelManagerScript.getStressValue() >= 1 && _gameLost == false)
        {
            EndGameCore();
        }
       
    }

    void EndGameCore()
    {
        _gameLost = true;
        gameOverScreen.SetActive(true);
    }

   public void restart()
    {
        _gameLost = false;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

}

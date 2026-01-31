using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("External Managers")]
    [SerializeField] private UIManager uiManager;

    [Header("Client Setup")]
    [SerializeField] private GameObject[] clients;
    [SerializeField] private Vector3 startPosition = new(4.5f, 0.5f, -7f);
    [SerializeField] private Vector3 midPosition = new(0f, 0.5f, -7f);
    [SerializeField] private Vector3 endPosition = new(-4.5f, 0.5f, -7f);
    [SerializeField] private float clientMoveSpeed = 3f;
    [Header("Stress Bar Values")]
    [SerializeField] private float singleIncreaseValue = 0.1f;
    [SerializeField] private float singleDecreaseValue = 0.05f;
    [SerializeField] private float multiIncreaseValue = 0.2f;
    [SerializeField] private float multiDecreaseValue = 0.15f;
    
    private float _currentStress = 0f;
    private const float MaxStress = 1f;
    private int negativeAnswersInRow = 0;
    private int positiveAnswersInRow = 0;
    
    
    private int currentClientIndex = 0;
    private string _currentClientDisposition;
    private GameObject _currentClient;
    

    private bool _clientStoppedMoving;
    
    void Start()
    {
        _currentClientDisposition = clients[0].GetComponent<ClientInfo>().GetClientDisposition();
        
        _currentClient = Instantiate(clients[currentClientIndex], startPosition, Quaternion.identity);
        StartCoroutine(MoveObject(midPosition));
    }

    private IEnumerator MoveObject(Vector3 target, bool isExit = false)
    {
        while (Vector3.Distance(_currentClient.transform.position, target) > 0.01f)
        {
            _currentClient.transform.position = Vector3.MoveTowards(
                _currentClient.transform.position,
            target,
            clientMoveSpeed * Time.deltaTime
            );

            yield return null;
        }
       
        _currentClient.transform.position = target;

        OnArrived(isExit);
    }

    private void OnArrived(bool isExit = false)
    {
        if (_clientStoppedMoving) return;
        _clientStoppedMoving = true;

        if (isExit)
        {
            currentClientIndex++;
            if (currentClientIndex >= clients.Length)
            {
                Debug.Log("End of game!");
            }
            else
            {
                Destroy(_currentClient);
                _currentClient = Instantiate(clients[currentClientIndex], startPosition, Quaternion.identity);
                _clientStoppedMoving = false;
                StartCoroutine(MoveObject(midPosition));
            }
        }
        else
        {
            uiManager.ToggleMaskButtons(true);
        }
    }
    

    public void CheckCompatibility(string userDisposition)
    {
        _clientStoppedMoving = false;
        if (userDisposition == _currentClientDisposition)
        {
            if (negativeAnswersInRow > 0) negativeAnswersInRow = 0;
            float stressReduction = positiveAnswersInRow > 2 ? multiDecreaseValue : singleDecreaseValue;
            _currentStress -= stressReduction;
            if(_currentStress < 0 ) _currentStress = 0;
        }
        else
        {
            if (positiveAnswersInRow > 0) positiveAnswersInRow = 0;
            float stressIncrease = negativeAnswersInRow > 2 ? multiIncreaseValue : singleIncreaseValue;
            _currentStress += stressIncrease;
        }
        
        uiManager.UpdateStressBar(_currentStress);
        uiManager.ToggleMaskButtons(false);
        StartCoroutine(MoveObject(endPosition, true));
    }

    
}

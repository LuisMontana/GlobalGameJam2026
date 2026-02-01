using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("External Managers")]
    [SerializeField] private UIManager uiManager;

    [Header("Client Setup")]
    [SerializeField] private GameObject[] clients;
    public Vector3 startPosition = new(4.5f, 0.5f, -7f);
    public Vector3 midPosition = new(0f, 0.5f, -7f);
    public Vector3 endPosition = new(-4.5f, 0.5f, -7f);
    public float clientMoveSpeed = 3f;
    [Header("Stress Bar Values")]
    [SerializeField] private float singleIncreaseValue = 0.1f;
    [SerializeField] private float singleDecreaseValue = 0.05f;
    [SerializeField] private float multiIncreaseValue = 0.2f;
    [SerializeField] private float multiDecreaseValue = 0.15f;

    [Header("Timer Values")]
    [SerializeField] private CameraShaker camShaker;
    [SerializeField] private float maxTimeToSelect = 5.0f;
    [SerializeField] private float shakeThreshold = 4.0f;
    [SerializeField] private float currentTimeToSelect = 0.0f;
    private bool _isShaking = false;
    private bool _isSelectingMask = false;

    private float _currentStress = 0f;
    private const float MaxStress = 1f;
    private int negativeAnswersInRow = 0;
    private int positiveAnswersInRow = 0;


    private int currentClientIndex = 0;
    private string _currentClientDisposition;
    private GameObject _currentClient;


    public bool _clientStoppedMoving;

    void Start()
    {
        _currentClientDisposition = clients[0].GetComponent<ClientInfo>().clientDisposition;

        _currentClient = Instantiate(clients[currentClientIndex], startPosition, Quaternion.identity);
        GenerateRandomUserStats();
        StartCoroutine(MoveObject(midPosition));
    }

    string GetRandomOption(string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }

    void Update()
    {
        if (_isSelectingMask)
        {
            currentTimeToSelect += Time.deltaTime;
            if (currentTimeToSelect > shakeThreshold && !_isShaking)
            {
                _isShaking = true;
                camShaker.StartContinuousShake();
            }
            if (currentTimeToSelect > maxTimeToSelect)
            {
                string[] dispositions = { "Happy", "Neutral", "Angry" };
                string picked = GetRandomOption(dispositions);
                CheckCompatibility(picked);
            }
            uiManager.UpdateTimer(1f - (currentTimeToSelect / maxTimeToSelect));
        }
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
    
    private void GenerateRandomUserStats()
    {
        string[] dispositions = { "happy", "neutral", "angry" };
        
        string[] femaleNames =
        {
            "Alice", "Sofia", "Emma", "Luna", "Isabella",
            "Mia", "Olivia", "Ava", "Camila", "Valentina",
            "Zoe", "Chloe", "Natalia", "Lucia", "Elena",
            "Aria", "Nora", "Julia", "Clara", "Victoria"
        };

        string[] maleNames =
        {
            "Liam", "Noah", "Lucas", "Mateo", "Ethan",
            "Oliver", "Leo", "Daniel", "Sebastian", "Gabriel",
            "David", "Samuel", "Adrian", "Alex", "Julian",
            "Benjamin", "Aaron", "Diego", "Oscar", "Max"
        };
        
        string picked = GetRandomOption(dispositions);
        ClientInfo cf = _currentClient.GetComponent<ClientInfo>();
        cf.clientDisposition = picked;
        
        cf.expenses = Random.Range(1, 100);
        cf.income = Random.Range(1, 100);
        cf.percentageSuccesfulLoans = Random.Range(1, 100);
        cf.charName = cf.sex == "F"
            ? femaleNames[Random.Range(0, femaleNames.Length)]
            : maleNames[Random.Range(0, maleNames.Length)];
        cf.gameObject.name = cf.charName;
        
        _currentClientDisposition = picked;
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
                GenerateRandomUserStats();
                _clientStoppedMoving = false;
                StartCoroutine(MoveObject(midPosition));
            }
        }
        else
        {
            _isSelectingMask = true;
            currentTimeToSelect = 0.0f;
            uiManager.UpdateTimer(1f);
            uiManager.ToggleMaskButtons(true);
        }
    }

    public void updateStressValue(float value)//La variable value debe ser un n�mero real entre [-1,1]
    {
        if (value > -1 && value < 1)
        {
            _currentStress += value;
        }

    }
    public float getStressValue()
    {
        return _currentStress;
    } //Getter de la variable _currentStress

    public void CheckCompatibility(string userDisposition)
    {
        _clientStoppedMoving = false;
        if (userDisposition == _currentClientDisposition)
        {
            if (negativeAnswersInRow > 0) negativeAnswersInRow = 0;
            float stressReduction = positiveAnswersInRow > 2 ? multiDecreaseValue : singleDecreaseValue;
            _currentStress -= stressReduction;
            if (_currentStress < 0) _currentStress = 0;
        }
        else
        {
            if (positiveAnswersInRow > 0) positiveAnswersInRow = 0;
            float stressIncrease = negativeAnswersInRow > 2 ? multiIncreaseValue : singleIncreaseValue;
            _currentStress += stressIncrease;
        }

        uiManager.UpdateStressBar(_currentStress);
        uiManager.ToggleMaskButtons(false);
        _isSelectingMask = false;
        _isShaking = false;
        camShaker.StopShake();
        StartCoroutine(MoveObject(endPosition, true));
    }
}

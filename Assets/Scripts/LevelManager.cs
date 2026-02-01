using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private bool _areMasksDisabled = true;

    private bool _isDescriptionDisabled = true;
    private string _currentClientDisposition;
    private int _currentStress = 0;
    private int _maxStress = 0;
    private GameObject _currentClient;
    private int currentClientIndex = 0;
    [SerializeField] private GameObject maskButtons;
    
    [SerializeField] private GameObject[] clients;
    
    [SerializeField] private Vector3 startPosition = new(4.5f, 0.5f, -7f);
    [SerializeField] private Vector3 midPosition = new(0f, 0.5f, -7f);
    [SerializeField] private Vector3 endPosition = new(-4.5f, 0.5f, -7f);
    [SerializeField] private float moveSpeed = 3f;

    private bool hasArrived;
    
    void Start()
    {
        // _currentClientDisposition = clients[0].GetComponent<ClientInfo>().GetClientDisposition();
        
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
            moveSpeed * Time.deltaTime
            );

            yield return null;
        }
       
        _currentClient.transform.position = target;

        OnArrived(isExit);
    }

    private void OnArrived(bool isExit = false)
    {
        if (hasArrived) return;
        hasArrived = true;

        _isDescriptionDisabled = true;
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
                hasArrived = false;
                StartCoroutine(MoveObject(midPosition));
            }
        }
        else
        {
            SetButtonsEnabled(true);
        }
    }
    
    void SetButtonsEnabled(bool newValue)
    {
        Button[] buttons = maskButtons.GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.interactable = newValue;
        }
    }

    public void CheckCompatibility(string userDisposition)
    {
        hasArrived = false;
        if (userDisposition == _currentClientDisposition)
        {
            Debug.Log("Bien!");
        }
        else
        {
            Debug.Log("Mal!");
        }
        SetButtonsEnabled(false);
        StartCoroutine(MoveObject(endPosition, true));
    }

    
}

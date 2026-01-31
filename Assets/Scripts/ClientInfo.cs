using UnityEngine;

public class ClientInfo : MonoBehaviour
{
    [SerializeField] private string clientDisposition;
    
    public string GetClientDisposition()
    {
        return clientDisposition;
    }
}

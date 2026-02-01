using UnityEngine;

public class ClientInfo : MonoBehaviour
{
    public string clientDisposition;
    public string sex;
    public int percentageSuccesfulLoans;
    public int income;
    public int expenses;
    public string charName;
    
    public string GetClientDisposition()
    {
        return clientDisposition;
    }
}

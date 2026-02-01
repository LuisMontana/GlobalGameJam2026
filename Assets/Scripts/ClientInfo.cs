using UnityEngine;

public enum ClientDisposition
{
    Calm,
    Angry,
    Nervous,
    Happy
}

[CreateAssetMenu(menuName = "Clientes/ClientInfo")]
public class ClientInfo : ScriptableObject
{
    [Header("Datos del cliente")]
    public string nombre;
    public string dni;

    [TextArea(3, 6)]
    public string descripcion;

    [Header("Personalidad / Disposición")]
    public ClientDisposition clientDisposition;

    public string GetClientDisposition()
    {
        return clientDisposition.ToString();
    }
}

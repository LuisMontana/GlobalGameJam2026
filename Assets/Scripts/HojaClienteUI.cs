using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HojaClienteUI : MonoBehaviour
{
    public ClientInfo clientData;

    [Header("Panel")]
    public GameObject panelHoja;

    [Header("Textos")]
    public TextMeshProUGUI nombreTexto;
    public TextMeshProUGUI dniTexto;
    public TextMeshProUGUI descripcionTexto;

    public Image profilePic;


    public void Start()
    {
        //if(clientData != null) AssignClientIinfo(clientData);
    }


    public void MostrarHoja(string nombre, string dni, string descripcion)
    {
        panelHoja.SetActive(true);

        nombreTexto.text = nombre;
        dniTexto.text = "DNI: " + dni;
        descripcionTexto.text = descripcion;
    }

    public void MostrarHoja(ClientInfo ci)
    {
        panelHoja.SetActive(true);

        nombreTexto.text = ci.nombre;
        dniTexto.text = "DNI: " + ci.dni;
        descripcionTexto.text = ci.descripcion;
    }

    public void CerrarHoja()
    {
        panelHoja.SetActive(false);
    }

    public void AssignClientIinfo(ClientInfo ci)
    {
        clientData = ci;
        MostrarHoja(ci);
    }
}

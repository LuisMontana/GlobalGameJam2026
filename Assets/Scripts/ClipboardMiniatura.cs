using UnityEngine;

public class ClipboardMiniatura : MonoBehaviour
{
    public HojaClienteUI hojaUI;

    [Header("Datos del cliente")]
    public string nombre;
    public string dni;
    [TextArea(3, 6)]
    public string descripcion;

    public void AbrirHoja()
    {
        hojaUI.MostrarHoja(nombre, dni, descripcion);
    }
}

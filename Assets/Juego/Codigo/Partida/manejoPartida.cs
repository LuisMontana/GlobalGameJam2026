using UnityEngine;
using UnityEngine.InputSystem;


public class manejoPartida : MonoBehaviour
{
    [Header("Composicion")]
    public Transform[] PoscnsClient;

    [Header("Referencias")]
    public LevelManager _ManejoNivel;
    public manejoPersonaje _ManejoPersonaje;


    void Awake()
    {
        _ManejoNivel.enabled = false;

        // Posicion de Inicio
        _ManejoNivel.startPosition = PoscnsClient[0].position;
        // Posicion de Atencion
        _ManejoNivel.midPosition = PoscnsClient[1].position;
        // Posicion de Salida
        _ManejoNivel.endPosition = PoscnsClient[2].position;

        // Velocidad de Cliente
        _ManejoNivel.clientMoveSpeed = 11;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Quitar_Juego();
        }
    }
    void FixedUpdate()
    {
        _ManejoNivel.enabled = _ManejoPersonaje.enPuestAsesr;
        _ManejoPersonaje.enMascaras = _ManejoNivel._clientStoppedMoving;
    }
    
    

    public void Quitar_Juego()
    {
        Application.Quit();
    }
}

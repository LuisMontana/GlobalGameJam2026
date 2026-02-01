using UnityEngine;

public class manejoPersonaje : MonoBehaviour
{
    [Header("Composicion")]
    public Transform VistJugdr;
    public Transform PuestoAsesor;
    public VistasJugador[] VistsJugdr;

    [Header("Variantes")]
    public float Velcd_Orientcn;
    public float Velcd_Camnr;

    [Header("Estados")]
    public float distncPuestAsesr;
    public int actualCamn;
    public string actlVistJugdr;
    public bool enPuestAsesr;
    public bool enMascaras;
    public bool enAltoEstres;


    [System.Serializable]
    public class VistasJugador
    {
        public string idVist;
        public Vector3 orientcnVist;
        public Vector3 poscnVist;
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        IniciandoJornada();
        Manejo_VistasJugador();
    }


    void IniciandoJornada()
    {
        // Camino hacia puesto de trabajo
        Transform avatar = transform.GetChild(0);
        Transform orientcn = transform.GetChild(1);
        Transform camnPuestAsesr = PuestoAsesor.GetChild(0);
        Transform actualCamnPuestAsesr = camnPuestAsesr.GetChild(actualCamn);
        //
        distncPuestAsesr = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(actualCamnPuestAsesr.position.x, 0, actualCamnPuestAsesr.position.z));
        if (Vector3.Distance(transform.position, PuestoAsesor.position) > .2f)
            actualCamn = (distncPuestAsesr <=
                (!enPuestAsesr ? 2f : 0)) ? actualCamn + 1 : actualCamn;
        // Direccion
        orientcn.LookAt(actualCamnPuestAsesr.position);
        Quaternion orientcnCamnr = avatar.rotation;
        orientcnCamnr.eulerAngles = new Vector3(0, orientcn.eulerAngles.y, 0);
        avatar.rotation = Quaternion.Lerp(avatar.rotation, (!enPuestAsesr) ? orientcnCamnr : actualCamnPuestAsesr.rotation,
            (Velcd_Orientcn * 11) * Time.deltaTime);
        // Movimiento
        Vector3 dirccnCamnr = avatar.forward;
        if (!enPuestAsesr)
            transform.Translate(dirccnCamnr * (Velcd_Camnr * .8f) * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, actualCamnPuestAsesr.position,
                (Velcd_Camnr * .4f) * Time.deltaTime);

        // Estado
        enPuestAsesr = actualCamn == camnPuestAsesr.childCount - 1;
    }
    void Manejo_VistasJugador()
    {
        int actlVist = 0;

        actlVistJugdr = (enMascaras) ? "Mascaras" :// Viendo que Mascara usar
            (enAltoEstres) ? "Estresado" :// Tentado a usar el Arma
            "";
        switch (actlVistJugdr)
        {
            case "Mascaras":
                actlVist = 1;
                break;
            case "Estresado":
                actlVist = 2;
                break;
        }

        VistJugdr.localEulerAngles = Vector3.Lerp(VistJugdr.localEulerAngles,
            VistsJugdr[actlVist].orientcnVist, (Velcd_Orientcn * 5) * Time.deltaTime);
        PuestoAsesor.localPosition = VistsJugdr[actlVist].poscnVist;
    }
}

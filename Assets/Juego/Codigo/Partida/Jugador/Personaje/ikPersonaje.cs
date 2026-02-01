using UnityEngine;

public class ikPersonaje : MonoBehaviour
{
    [Header("Composicion")]
    public Animator Animador;
    public IKs[] IKs_Esqlt;

    [Header("ObjetosInteraccion")]
    public Transform[] Mascaras;

    [Header("Estados")]
    public int actlMascr;

    [System.Serializable]
    public class IKs
    {
        public string Id_Hueso;
        public Transform Ik_Hueso;
        public float Peso_Hueso;
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }


    void OnAnimatorIK(int layerIndex)
    {
        for (int i = 0; i < IKs_Esqlt.Length; i++)
        {
            IKs ik = IKs_Esqlt[i];

            switch (ik.Id_Hueso)
            {
                case "ManoDerecha":
                    Animador.SetIKRotation(AvatarIKGoal.RightHand, ik.Ik_Hueso.rotation);
                    Animador.SetIKPosition(AvatarIKGoal.RightHand, ik.Ik_Hueso.position);
                    Animador.SetIKPositionWeight(AvatarIKGoal.RightHand, ik.Peso_Hueso);
                    Animador.SetIKRotationWeight(AvatarIKGoal.RightHand, ik.Peso_Hueso);
                    break;
                case "ManoIzquierda":
                    Animador.SetIKRotation(AvatarIKGoal.LeftHand, ik.Ik_Hueso.rotation);
                    Animador.SetIKPosition(AvatarIKGoal.LeftHand, ik.Ik_Hueso.position);
                    Animador.SetIKPositionWeight(AvatarIKGoal.LeftHand, ik.Peso_Hueso);
                    Animador.SetIKRotationWeight(AvatarIKGoal.LeftHand, ik.Peso_Hueso);
                    break;
            }
        }
    }
}

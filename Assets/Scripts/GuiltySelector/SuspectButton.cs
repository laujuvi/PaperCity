using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectButton : MonoBehaviour
{
    // Variable para almacenar el sospechoso asociado al botón
    [SerializeField] private Suspect associatedSuspect;

    // Propiedad para acceder al sospechoso asociado desde otros scripts
    public Suspect AssociatedSuspect
    {
        get { return associatedSuspect; }
        set { associatedSuspect = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tipoTonal
{
    fuerte = 1,
    debil = 2
}

public enum tipoGrado
{
    tonica = 1,
    subdominante = 2,
    dominante = 3
}
public class UnidadPiano : MonoBehaviour
{
    

    public tipoTonal funcionTonal;
    public tipoGrado grado;
    public int nota;

    public UnidadPiano(tipoTonal tipoTonalParam)
    {
        funcionTonal = tipoTonalParam;
    }


    
}

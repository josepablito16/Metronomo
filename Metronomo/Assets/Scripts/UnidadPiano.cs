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
    public List<int> acorde;

    public UnidadPiano(tipoTonal tipoTonalParam, tipoGrado tipoGradoParam, List<int> acordeParam)
    {
        funcionTonal = tipoTonalParam;
        grado = tipoGradoParam;
        acorde = acordeParam;
    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFormas : MonoBehaviour
{
    string[] letras = { "A", "B", "C", "D", "E", "F" };
    // Start is called before the first frame update
    void Start()
    {
        List<string> secciones = new List<string>();
        secciones = generarSecciones();
        Debug.Log(string.Join(", ", secciones));

    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<string> generarSecciones()
    {
        int cantidadCompases = getCantidadCompases();
        //Debug.Log(cantidadCompases);
        return getSecciones(cantidadCompases);
    }


    int getCantidadCompases()
    {
        /*
        Modular 8
        Minimo 16
        Maximo X
        */
        int[] posibles = { 16, 24, 32, 40 };


        return posibles[Random.Range(0, posibles.Length)];
    }

    List<string> getSecciones(int cantidadCompases)
    {
        /*
        Minimo 2
        maximo la cantidad de compases
        los extremos de este rango seran poco probable
        */
        int cantidadSecciones = cantidadCompases / 8;
        List<string> secciones = new List<string>();

        if (cantidadSecciones == 2)
        {
            secciones.Add(letras[0]);
            secciones.Add(letras[1]);

        }
        else
        {
            int idSeccion = 0;
            secciones.Add(letras[idSeccion++]);

            for (int i = 1; i < cantidadSecciones; i++)
            {
                int random = Random.Range(0, 15);

                if (random == 0)
                {
                    secciones.Add(secciones[secciones.Count - 1]);
                }
                else if (random > 1 && random < 10)
                {
                    secciones.Add(letras[idSeccion++]);
                }
                else
                {
                    secciones.Add(letras[Random.Range(0, idSeccion + 1)]);
                }

            }

        }
        //Debug.Log("Secciones = " + cantidadCompases / 8);

        return secciones;
    }







}

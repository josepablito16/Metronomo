using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFormas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int cantidadCompases = getCantidadCompases();
        Debug.Log(cantidadCompases);
        getSecciones(cantidadCompases);

    }

    // Update is called once per frame
    void Update()
    {


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

    List<int> getSecciones(int cantidadCompases)
    {
        /*
        Minimo 2
        maximo la cantidad de compases
        los extremos de este rango seran poco probable
        */
        int cantidadSecciones = cantidadCompases / 8;
        List<int> secciones = new List<int>();

        if (cantidadSecciones == 2)
        {
            secciones.Add(1);
            secciones.Add(2);
        }
        else
        {
            int idSeccion = 0;
            secciones.Add(idSeccion++);

            for (int i = 1; i < cantidadSecciones; i++)
            {
                int random = Random.Range(0, 5);

                if (random > 1)
                {
                    secciones.Add(idSeccion++);
                }
                else
                {
                    secciones.Add(secciones[secciones.Count - 1]);
                }

            }

        }
        Debug.Log("Secciones = " + cantidadCompases / 8);
        Debug.Log(string.Join(", ", secciones));
        return secciones;
    }







}

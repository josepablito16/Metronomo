using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeRitmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        generarRitmo();        
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }


    void generarRitmo(int seed = 0)
    {
        if (seed != 0)
        {
            Random.seed = seed;
        }
        else
        {
            //Debug.Log("Seed"+Random.seed);
        }
        
        //ej 3/4
        //cantidad_subdivision = 3 
        //subdivision_base = 4
        // solo vamos a trabajar con 3/4 y 4/4
        int[] cantidadSubdivisionArray = {3,4};
        int[] subdivisionBaseArray = {4};


        int cantidadSubdivision = cantidadSubdivisionArray[Random.Range(0,cantidadSubdivisionArray.Length)];
        int subdivisionBase =subdivisionBaseArray[Random.Range(0,subdivisionBaseArray.Length)];

        Debug.Log("cantidad de subdivisiones " +cantidadSubdivision);
        Debug.Log("subdivision base " +subdivisionBase);

        crearClave(cantidadSubdivision, subdivisionBase);

    }

    void getGrupoClave(int limite)
    {
        
        Debug.Log("GenerarClave Random limite = "+limite);
    }

    void crearClave(int cantidadSubdivision, int subdivisionBase)
    {
        int[] posibleSubdivision = {1,2,4};

        int subdivisionRandom = posibleSubdivision[Random.Range(0,posibleSubdivision.Length)];

        int subdivisionClave = subdivisionRandom * subdivisionBase;



        Debug.Log("Subdivision Clave es 1/"+ subdivisionClave);

        getGrupoClave(subdivisionRandom * cantidadSubdivision);




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Piano : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int cantSubdivisiones;


        // 8 compases de 3/4
        //cantSubdivisiones = 3;
        //List<float> resultado = getRitmoArmonico(24,cantSubdivisiones);

        // 8 compases de 4/4
        cantSubdivisiones = 4;
        List<float> resultado = getRitmoArmonico(32,cantSubdivisiones);


        Debug.Log("Clave final = "+string.Join(", ", resultado));

        calcularFuncionesTonales(resultado, cantSubdivisiones);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getSum(List<float> ritmoArmonico)
    {
        /*
        Funcion para obtener la suma de todas las posiciones de una lista

        Parametros:
        - ritmoArmonico: lista tipo float, cada posicion tiene el valor de una figura.

        Return:
        - valor <int> de la suma de todas las posiciones
        */
        int suma = 0;
        if (ritmoArmonico.Count == 0)
            return 0;

        return (int) ritmoArmonico.Aggregate((x, y) => x + y);
    }

    List<float> getRitmoArmonico(int limite, int cantSubdivisiones)
    {
        /*
        Funcion para generar aleatoriamente los valores de figura del ritmo
        armonico.

        Parametros:
        - limite: valor del cual no se puede pasar el ritmo armonico. Ej:
                    * para 8 compases de 4/4 seria 4*8 = 32
                    * para 8 compases de 3/4 seria 3*8 = 24
        - cantSubdivisiones: cantidad de subdivisiones que queremos usar. Ej:
                    * 4/4 seria 4
                    * 3/4 seria 3
        */
        float[] posible = {
            4f * cantSubdivisiones,  //redonda
            2f * cantSubdivisiones,   //blanca
            1f * cantSubdivisiones,   //negra
            0.5f * cantSubdivisiones,   //corchea
            //0.25f * cantSubdivisiones    //semicorchea
        };

        bool condicionSalida = false;

        List<float> ritmoArmonicoFinal = new List<float>();
        
        while (true)
        {

            while (getSum(ritmoArmonicoFinal) <= limite)
            {
                if (getSum(ritmoArmonicoFinal) == limite)
                {
                    condicionSalida = true;
                    break;
                }
                else
                {
                    ritmoArmonicoFinal.Add(posible[Random.Range(0,posible.Length)]);
                }
            }

            if (condicionSalida)
            {
                break;
            }
            else{
                ritmoArmonicoFinal.Clear();  
            }
        }
        return ritmoArmonicoFinal;
    }

    List<UnidadPiano> agregarUnidades(int cantidad, tipoTonal funcionTonal, List<UnidadPiano> funciones)
    {
        /*
        Funcion para agregar <cantidad> elementos de tipo <UnidadPiano> con funcion
        tonal <funcionTonal>.

        Parametros:
        - cantidad: cantidad de elementos a agregar a la lista
        - funcionTonal: funcion tonal de los elementos a agregar
        - funciones: lista donde se quiere agregar los elementos

        Retorno:
        - lista con los elementos agregados
        */

        for (int i = 0; i < cantidad; i++)
        {
            UnidadPiano temp = new UnidadPiano(funcionTonal);
            funciones.Add(temp);
        }

        return funciones;

    }
    void calcularFuncionesTonales(List<float> ritmoArmonico, int cantSubdivisiones)
    {
        float unidadMinima =  0.25f * cantSubdivisiones;
        float residuo = 0;
        Debug.Log("unidad Minima = "+ unidadMinima);
        List<UnidadPiano> funciones = new List<UnidadPiano>();

        foreach (float item in ritmoArmonico)
        {
            if (item % 2 == 0)
            {
                float mitad = item / 2;

                // agregarUnidades mitad, Fuerte
                funciones = agregarUnidades((int) mitad, tipoTonal.fuerte, funciones);

                // agregarUnidades mitad, Debil
                funciones = agregarUnidades((int) mitad, tipoTonal.debil, funciones);
            }
            else if (item == 3f)
            {
                funciones = agregarUnidades(1, tipoTonal.fuerte, funciones);
                funciones = agregarUnidades(2, tipoTonal.debil, funciones);
            }
            else if (item == 1.5f)
            {
                funciones = agregarUnidades(1, tipoTonal.fuerte, funciones);

                if (residuo == 0.5f)
                {
                    funciones = agregarUnidades(1, tipoTonal.debil, funciones);
                    residuo = 0;
                }
                else
                {
                    residuo = 0.5f;
                }

            }
        }

        for (int i = 0; i < funciones.Count; i++)
        {
            Debug.Log(i + " " +funciones[i].funcionTonal);

        }


    }
}


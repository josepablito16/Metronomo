using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Piano : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 8 compases de 3/4
        //List<float> resultado = getRitmoArmonico(24,3);

        // 8 compases de 4/4
        List<float> resultado = getRitmoArmonico(32,4);
        Debug.Log("Clave final = "+string.Join(", ", resultado));
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
            0.25f * cantSubdivisiones    //semicorchea
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
}

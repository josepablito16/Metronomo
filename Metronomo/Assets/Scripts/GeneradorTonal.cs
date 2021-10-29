using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorTonal : MonoBehaviour
{
    List<int> formulaEscalaMayor = new List<int> { 2, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2, 2, 2, 1 };


    List<int> getEcala(int notaInicial)
    {
        List<int> escala = new List<int>();
        int notaActual = notaInicial;
        escala.Add(notaActual);
        foreach (int salto in formulaEscalaMayor)
        {
            notaActual += salto;
            escala.Add(notaActual);
        }
        return escala;
    }

    List<int> getAcorde(int notaRaiz, List<int> escala)
    {
        return new List<int> {
                escala[notaRaiz] % 12,
                escala[notaRaiz + 2] % 12,
                escala[notaRaiz + 4] % 12
            };
    }


    public List<int> calcularAcorde(int gradoIndex, int notaIndex)
    {
        /*
        Funcion que devuelve los semitonos de diferencia para contruir un acorde respecto
        a un grado y una nota.

        Parametros:
        - gradoIndex: int que representa el grado del acorde
                        Ej:
                        0. Primer Grado
                        1. Segundo Grado
                        2. Tercer Grado
                        3. Cuarto Grado
                        4. Quinto Grado
                        5. Sexto Grado
                        6. Septimo Grado
        - notaIndex: int que representa la nota de la escala.
                        Ej:
                        0. Do
                        1. DO#
                        2. RE
                        3. RE#
                        4. MI
                        5. FA
                        6. FA#
                        7. SOL
                        8. SOL#
                        9. LA
                        10. LA#
                        11. SI

        */

        int maxAcordes = 8;

        int contadorAcordes = 0;

        List<int> escala = getEcala(notaIndex);

        List<int> acorde = getAcorde(gradoIndex, escala);

        //Debug.Log("Acorde = " + string.Join(", ", acorde));
        return acorde;
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}

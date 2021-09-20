using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorTonal : MonoBehaviour
{
    List<int> formulaEscalaMayor = new List<int> { 2, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2, 2, 2, 1 };

    List<string> nombreNotas = new List<string> {"Do", "DO#", "RE", "RE#", "MI",
                "FA", "FA#", "SOL", "SOL#", "LA", "LA#", "SI"};

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

    List<string> getAcorde(int notaRaiz, List<int> escala)
    {
        return new List<string> {
                nombreNotas[escala[notaRaiz] % 12],
                nombreNotas[escala[notaRaiz + 2] % 12],
                nombreNotas[escala[notaRaiz + 4] % 12]
            };
    }




    // Start is called before the first frame update
    void Start()
    {
        // Escriba el numero de grado:
        int gradoIndex = 1;

        // "Escriba el numero de nota: 
        int notaIndex = 1;

        int maxAcordes = 8;

        int contadorAcordes = 0;

        List<int> escala = getEcala(notaIndex);

        List<string> acorde = getAcorde(gradoIndex, escala);

        Debug.Log("Acorde = " + string.Join(", ", acorde));

    }

    // Update is called once per frame
    void Update()
    {

    }
}

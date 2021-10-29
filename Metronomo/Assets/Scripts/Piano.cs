using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Piano : MonoBehaviour
{
    int notaIndex;
    List<UnidadPiano> funciones = new List<UnidadPiano>();
    List<UnidadPiano> seccionTemp = new List<UnidadPiano>();
    int tiempo = 0;

    List<string> nombreNotas = new List<string> {"Do", "DO#", "RE", "RE#", "MI",
                "FA", "FA#", "SOL", "SOL#", "LA", "LA#", "SI"};
    public List<string> GenerarRitmoPiano(int cantSubdivisiones, int notaBase)
    {
        tiempo = 0;
        notaIndex = notaBase;
        List<float> resultado = new List<float>();
        if (cantSubdivisiones == 4)
        {
            // 8 compases de 4/4
            cantSubdivisiones = 4;
            resultado = getRitmoArmonico(32, cantSubdivisiones);
        }
        else
        {
            // 8 compases de 3/4
            cantSubdivisiones = 3;
            resultado = getRitmoArmonico(24, cantSubdivisiones);
        }



        //Debug.Log("Duración de acordes = " + string.Join(", ", resultado));
        Text info = GameObject.Find("Info").GetComponent<Text>();


        List<string> respuesta = new List<string>();

        respuesta.Add(string.Join(", ", resultado));

        respuesta.Add(nombreNotas[notaIndex]);
        seccionTemp = calcularFuncionesTonales(resultado, cantSubdivisiones);

        Debug.Log("respuesta = " + string.Join(", ", respuesta));

        return respuesta;
    }

    public string getNotaBase(int notaIndex)
    {
        return nombreNotas[notaIndex];
    }

    public List<UnidadPiano> getSeccionTemp()
    {
        return seccionTemp;
    }

    public void agregarSeccion(List<UnidadPiano> seccion)
    {
        funciones = funciones.Concat(seccion).ToList();
    }

    public void cleanFunciones()
    {
        funciones.Clear();
    }

    public void PlayPiano()
    {
        PianoPlayer a = new PianoPlayer();
        try
        {
            List<int> prevAcorde = funciones[(tiempo - 1) % funciones.Count].acorde;
            List<int> acordeActual = funciones[tiempo % funciones.Count].acorde;
            if (!prevAcorde.SequenceEqual(acordeActual))
                a.playAcorde(funciones[tiempo % funciones.Count].acorde);
        }
        catch (System.Exception)
        {

            a.playAcorde(funciones[tiempo % funciones.Count].acorde);
        }

        tiempo += 1;
    }


    // Start is called before the first frame update
    void Start()
    {
        //GenerarRitmoPiano(4);
        //PlayPiano();
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

        return (int)ritmoArmonico.Aggregate((x, y) => x + y);
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
                    ritmoArmonicoFinal.Add(posible[Random.Range(0, posible.Length)]);
                }
            }

            if (condicionSalida)
            {
                break;
            }
            else
            {
                ritmoArmonicoFinal.Clear();
            }
        }
        return ritmoArmonicoFinal;
    }

    List<UnidadPiano> agregarUnidades(int cantidad, tipoTonal funcionTonal, List<UnidadPiano> funciones, bool esPrimero, bool esUltimo)
    {
        /*
        Funcion para agregar <cantidad> elementos de tipo <UnidadPiano> con funcion
        tonal <funcionTonal>.

        Parametros:
        - cantidad: cantidad de elementos a agregar a la lista
        - funcionTonal: funcion tonal de los elementos a agregar
        - funciones: lista donde se quiere agregar los elementos
        - esPrimero: verdadero si es la primera unidad
        - esUltimo: verdadero si es la ultima unidad

        Retorno:
        - lista con los elementos agregados
        */

        int[] posiblesTonicas = { 1, 3, 6 };
        int[] posiblesSubDominantes = { 2, 4 };
        GeneradorTonal generadorTonal = new GeneradorTonal();


        tipoGrado grado = getTipoGrado(funcionTonal, esPrimero, esUltimo);
        List<int> acorde = new List<int>();
        if (tipoGrado.tonica == grado)
        {
            acorde = generadorTonal.calcularAcorde(posiblesTonicas[Random.Range(0, posiblesTonicas.Length)], notaIndex);
        }
        else if (tipoGrado.subdominante == grado)
        {
            acorde = generadorTonal.calcularAcorde(posiblesSubDominantes[Random.Range(0, posiblesSubDominantes.Length)], notaIndex);
        }
        else
        {
            // dominantes 5
            acorde = generadorTonal.calcularAcorde(5, notaIndex);
        }

        for (int i = 0; i < cantidad; i++)
        {
            UnidadPiano temp = new UnidadPiano(funcionTonal, grado, acorde);
            funciones.Add(temp);
        }

        return funciones;

    }

    tipoGrado getTipoGrado(tipoTonal funcionTonal, bool esPrimero, bool esUltimo)
    {
        /*
        Funcion para obtener de forma random el tipo de grado

        Parametros:
        - funcionTonal: funcion tonal que tomaremos como base (fuerte, debil)
        - esPrimero: sera verdadero si es el primer segmento a analizar
        - esUltimo: sera verdadero si es el ultimo segmento a analizar

        Return:
        - tipoGrado generado random
        */
        List<tipoGrado> posibles = new List<tipoGrado>();
        if (funcionTonal == tipoTonal.fuerte)
        {
            // si es fuerte y es el primero, simpre sera tonica
            if (esPrimero)
                return tipoGrado.tonica;

            // se agregan las opciones a seleccionar si es fuerte y no es el primero
            posibles.Add(tipoGrado.tonica);
            posibles.Add(tipoGrado.subdominante);
        }
        else
        {
            // si es debil y es el ultimo, siempre sera dominante
            if (esUltimo)
                return tipoGrado.dominante;

            // se agregan las opciones a seleccionar si es debil y no es el primero
            posibles.Add(tipoGrado.subdominante);
            posibles.Add(tipoGrado.dominante);
        }

        /*
        vamos a seleccionar random con mayor probabilidad
        de seleccionar la primera posicion de lista "posibles"
        */
        if (Random.Range(0f, 1f) < 0.8)
            return posibles[0];
        else
            return posibles[1];


    }
    List<UnidadPiano> calcularFuncionesTonales(List<float> ritmoArmonico, int cantSubdivisiones)
    {
        float residuo = 0;
        float acumulado = 0;
        int max = cantSubdivisiones * 8;
        bool esPrimero = true;
        bool esUltimo = false;

        List<UnidadPiano> funciones = new List<UnidadPiano>();

        foreach (float item in ritmoArmonico)
        {

            if (item % 2 == 0)
            {
                if ((acumulado + item) == max)
                    esUltimo = true;

                float mitad = item / 2;

                // agregarUnidades mitad, Fuerte
                funciones = agregarUnidades((int)mitad, tipoTonal.fuerte, funciones, esPrimero, esUltimo);

                // agregarUnidades mitad, Debil
                funciones = agregarUnidades((int)mitad, tipoTonal.debil, funciones, esPrimero, esUltimo);
            }
            else if (item == 3f)
            {
                if ((acumulado + item) == max)
                    esUltimo = true;
                funciones = agregarUnidades(1, tipoTonal.fuerte, funciones, esPrimero, esUltimo);
                funciones = agregarUnidades(2, tipoTonal.debil, funciones, esPrimero, esUltimo);
            }
            else if (item == 1.5f)
            {
                if ((acumulado + item) == max)
                    esUltimo = true;
                funciones = agregarUnidades(1, tipoTonal.fuerte, funciones, esPrimero, esUltimo);

                if (residuo == 0.5f)
                {
                    funciones = agregarUnidades(1, tipoTonal.debil, funciones, esPrimero, esUltimo);
                    residuo = 0;
                }
                else
                {
                    residuo = 0.5f;
                }

            }

            if (esPrimero)
                esPrimero = false;

            acumulado += item;
        }

        return funciones;

    }
}


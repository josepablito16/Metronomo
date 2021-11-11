using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;



public class GeneradorDeRitmo : MonoBehaviour
{
    [Header("HitHat")]
    public AudioSource[] HitHat;

    [Header("Kick")]
    public AudioSource[] Kick;

    [Header("Snare")]
    public AudioSource[] Snare;

    private double Interval; //The interval between the ticks

    private double SubInterval;

    private double BPM = 120;

    private int Counter;

    private int setTheTime = 1000;

    private int NumberOfBeatsInBar = 2; //cant_subdivision

    private int contadorControl = 0;

    private IEnumerator IEtickRoutine;

    private List<int> rellenoFormas = new List<int>();

    private int rellenoDim = 0;

    Piano miPiano = new Piano();

    List<string> pianoInfo = new List<string>();



    // Start is called before the first frame update
    void Start()
    {
        generarRitmo();
        Interval = 60.0f / BPM;
        SubInterval = Interval / 2;
        IEtickRoutine = tickRoutine();
        StartCoroutine(IEtickRoutine);

    }

    public void HandlenputField(string text)
    {

        BPM = int.Parse(text);
    }

    IEnumerator tickRoutine()
    {
        //While the time is less than 1000( You can use any number for this, but it seems to multiply it by 2 what ever you pick) this is the amount of time the metronome runs for
        while (Time.time < setTheTime) //decided to use a while statement because it seemed like the bst option for a continuingly playing beat
        {

            // Subintervalo
            int HitHatIndex = Random.Range(0, HitHat.Length);
            HitHat[HitHatIndex].Play();

            miPiano.PlayPiano();


            //Intervalo normal
            contadorControl++;
            if (contadorControl % 2 == 0)
            {
                if (rellenoFormas[Counter % rellenoDim] == 0)             //on the first beat I want to play a different sound, then repeat that pattern every 4 beats 1(Accent), 2, 3, 4, 1(Accent), 2, 3, 4.... etc.  In this case I have used a modulas operator. 
                {
                    // Suena clave
                    Kick[0].Play();
                }
                else if (rellenoFormas[Counter % rellenoDim] == 1)
                {
                    // suena relleno
                    if (Counter % rellenoDim == rellenoDim - 1)
                        Snare[0].Play();
                    else
                        Snare[1].Play();


                }
                Counter++;

            }

            Interval = 60.0f / BPM;
            SubInterval = Interval;
            yield return new WaitForSecondsRealtime((float)SubInterval); // Because I decided to use doubles for the ticks and BPM to be more acurate with time, we have to explicitly imply it as a float for the WaitForSecondsRealtime method to recognose it.
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void generarRitmo(int seed = 0)
    {
        rellenoFormas.Clear();
        miPiano.cleanFunciones();
        Counter = 0;
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
        int[] cantidadSubdivisionArray = { 3, 4 };
        int[] subdivisionBaseArray = { 4 };


        int cantidadSubdivision = cantidadSubdivisionArray[Random.Range(0, cantidadSubdivisionArray.Length)];


        int notaBase = Random.Range(0, 11);



        NumberOfBeatsInBar = cantidadSubdivision;

        int subdivisionBase = subdivisionBaseArray[Random.Range(0, subdivisionBaseArray.Length)];

        //Debug.Log("cantidad de subdivisiones " +cantidadSubdivision);
        //Debug.Log("subdivision base " +subdivisionBase);


        /*
        GENERACION DE SECCIONES
        */
        Dictionary<string, List<int>> seccionesRelleno = new Dictionary<string, List<int>>();
        Dictionary<string, List<UnidadPiano>> seccionesAcorde = new Dictionary<string, List<UnidadPiano>>();
        Dictionary<string, List<int>> seccionesMelodia = new Dictionary<string, List<int>>();

        GeneradorFormas GF = new GeneradorFormas();
        List<string> secciones = GF.generarSecciones();
        //Debug.Log("Secciones = " + string.Join(", ", secciones));
        Text info = GameObject.Find("Info").GetComponent<Text>();
        info.text = string.Format("Metrica: {0}/4\nNota base: {1}\nSecciones: {2}", cantidadSubdivision, miPiano.getNotaBase(notaBase), string.Join(", ", secciones));

        //--------------------------------
        //pianoInfo = miPiano.GenerarRitmoPiano(cantidadSubdivision, notaBase);
        //miPiano.agregarSeccion(miPiano.getSeccionTemp());
        //--------------------------------
        foreach (string i in secciones)
        {
            try
            {
                seccionesRelleno[i] = seccionesRelleno[i];
            }
            catch (System.Exception)
            {
                // Bateria
                seccionesRelleno[i] = crearClave(cantidadSubdivision, subdivisionBase);

                // Piano
                pianoInfo = miPiano.GenerarRitmoPiano(cantidadSubdivision, notaBase);
                seccionesAcorde[i] = miPiano.getSeccionTemp();
                seccionesMelodia[i] = miPiano.GenerarMelodiaPiano(cantidadSubdivision, seccionesAcorde[i]);
                Debug.Log(i);
                Debug.Log(seccionesAcorde[i].Count);
            }
        }


        foreach (string i in secciones)
        {
            rellenoFormas = rellenoFormas.Concat(seccionesRelleno[i]).ToList();
            miPiano.agregarSeccion(seccionesAcorde[i]);
            miPiano.agregarSeccionMelodia(seccionesMelodia[i]);
        }



        //rellenoFormas = crearClave(cantidadSubdivision, subdivisionBase);
        rellenoDim = rellenoFormas.Count;

    }

    int getSum(List<int> claveFinal)
    {
        int suma = 0;
        if (claveFinal.Count == 0)
            return 0;

        return claveFinal.Aggregate((x, y) => x + y);


    }

    List<int> getGrupoClave(int limite)
    {
        int[] posible = { 2, 3 };
        //posibleSubdivision[Random.Range(0,posibleSubdivision.Length)];

        //Debug.Log("--- Generar clave ---");
        //Debug.Log("limite = "+limite);

        bool condicionSalida = false;

        List<int> claveFinal = new List<int>();





        while (true)
        {

            while (getSum(claveFinal) <= limite)
            {
                if (getSum(claveFinal) == limite)
                {
                    condicionSalida = true;
                    break;
                }
                else
                {
                    claveFinal.Add(posible[Random.Range(0, posible.Length)]);
                }
            }

            if (condicionSalida)
            {
                break;
            }
            else
            {
                claveFinal.Clear();
            }

        }


        return claveFinal;

    }

    List<int> crearRelleno(List<int> clave, int dimension)
    {
        List<int> relleno = new List<int>();

        for (int i = 0; i < dimension; i++)
        {
            relleno.Add(0);
        }

        int contador = -1;
        foreach (int i in clave)
        {
            contador += i;
            relleno[contador] = 1;
        }

        return relleno;
    }



    List<int> crearClave(int cantidadSubdivision, int subdivisionBase)
    {
        List<int> rellenoFinal = new List<int>();
        int[] posibleSubdivision = { 1, 2, 4 };

        int subdivisionRandom = posibleSubdivision[Random.Range(0, posibleSubdivision.Length)];

        int subdivisionClave = subdivisionRandom * subdivisionBase;



        //Debug.Log("Subdivision Clave es 1/"+ subdivisionClave);

        List<int> claveFinal = getGrupoClave(subdivisionRandom * cantidadSubdivision);
        //Debug.Log("Clave final = "+string.Join(", ", claveFinal));


        rellenoFinal = crearRelleno(claveFinal, subdivisionRandom * cantidadSubdivision);

        /*
            Se crea una bateria simple
        */
        if (Random.Range(0, 3) == 0)
        {
            for (int i = 0; i < rellenoFinal.Count; i++)
            {
                if (rellenoFinal[i] == 1)
                {
                    rellenoFinal[i] = 5;
                }
            }
        }

        //Debug.Log("Relleno final = " + string.Join(", ", rellenoFinal));


        return rellenoFinal;

    }
}

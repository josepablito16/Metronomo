using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class GeneradorDeRitmo : MonoBehaviour
{   
    [Header("Samples")]
    public AudioSource[] MetroSound;

    private double Interval; //The interval between the ticks

    private double SubInterval;

    private double BPM = 120;

    private int Counter;

    private int setTheTime = 1000;

    private int NumberOfBeatsInBar = 2; //cant_subdivision

    private int contadorControl = 0;

    private IEnumerator IEtickRoutine;

    private List<int> rellenoFinal = new List<int>();

    private int rellenoDim = 0;


    // Start is called before the first frame update
    void Start()
    {
        generarRitmo();
        Interval = 60.0f / BPM;
        SubInterval = Interval/2;
        IEtickRoutine = tickRoutine();
        StartCoroutine(IEtickRoutine);

    }

    IEnumerator tickRoutine()
    {
        //While the time is less than 1000( You can use any number for this, but it seems to multiply it by 2 what ever you pick) this is the amount of time the metronome runs for
        while (Time.time < setTheTime) //decided to use a while statement because it seemed like the bst option for a continuingly playing beat
        {

            // Subintervalo
            MetroSound[2].volume = Random.Range(0.7f, 1f);
            MetroSound[2].Play();



            //Intervalo normal
            contadorControl++;
            if (contadorControl % 2 == 0)
            {
                if (rellenoFinal[Counter % rellenoDim] == 0)             //on the first beat I want to play a different sound, then repeat that pattern every 4 beats 1(Accent), 2, 3, 4, 1(Accent), 2, 3, 4.... etc.  In this case I have used a modulas operator. 
                {
                    // Suena clave
                    MetroSound[0].Play();
                }
                else if (rellenoFinal[Counter % rellenoDim] == 1)
                {
                    // suena relleno
                    MetroSound[1].Play();
                }
                Counter++;

            }

            Interval = 60.0f / BPM;
            SubInterval = Interval/2;
            yield return new WaitForSecondsRealtime((float)SubInterval); // Because I decided to use doubles for the ticks and BPM to be more acurate with time, we have to explicitly imply it as a float for the WaitForSecondsRealtime method to recognose it.
        }
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

        NumberOfBeatsInBar = cantidadSubdivision;

        int subdivisionBase =subdivisionBaseArray[Random.Range(0,subdivisionBaseArray.Length)];

        Debug.Log("cantidad de subdivisiones " +cantidadSubdivision);
        Debug.Log("subdivision base " +subdivisionBase);

        crearClave(cantidadSubdivision, subdivisionBase);

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
        int[] posible = {2,3};
        //posibleSubdivision[Random.Range(0,posibleSubdivision.Length)];

        Debug.Log("--- Generar clave ---");
        Debug.Log("limite = "+limite);

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
                    claveFinal.Add(posible[Random.Range(0,posible.Length)]);
                }
            }

            if (condicionSalida)
            {
                break;
            }
            else{
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
            contador+=i;
            relleno[contador] = 1;
        }
        
        return relleno;
    }



    void crearClave(int cantidadSubdivision, int subdivisionBase)
    {
        int[] posibleSubdivision = {1,2,4};

        int subdivisionRandom = posibleSubdivision[Random.Range(0,posibleSubdivision.Length)];

        int subdivisionClave = subdivisionRandom * subdivisionBase;



        Debug.Log("Subdivision Clave es 1/"+ subdivisionClave);

        List<int> claveFinal = getGrupoClave(subdivisionRandom * cantidadSubdivision);
        Debug.Log("Clave final = "+string.Join(", ", claveFinal));


        rellenoFinal = crearRelleno(claveFinal,subdivisionRandom * cantidadSubdivision);
        rellenoDim = rellenoFinal.Count;
        Debug.Log("Relleno final = "+string.Join(", ", rellenoFinal));


    }
}

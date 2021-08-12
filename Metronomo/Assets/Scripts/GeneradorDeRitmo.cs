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


    // Start is called before the first frame update
    void Start()
    {
        Interval = 60.0f / BPM;
        SubInterval = Interval/2;
        IEtickRoutine = tickRoutine();
        StartCoroutine(IEtickRoutine);

        generarRitmo();

    }

    IEnumerator tickRoutine()
    {
        //While the time is less than 1000( You can use any number for this, but it seems to multiply it by 2 what ever you pick) this is the amount of time the metronome runs for
        while (Time.time < setTheTime) //decided to use a while statement because it seemed like the bst option for a continuingly playing beat
        {

            // Subintervalo
            MetroSound[2].volume = 1;
            MetroSound[2].Play();



            //Intervalo normal
            contadorControl++;
            if (contadorControl % 2 == 0)
            {
                Counter++;
                if (Counter % NumberOfBeatsInBar == 1)             //on the first beat I want to play a different sound, then repeat that pattern every 4 beats 1(Accent), 2, 3, 4, 1(Accent), 2, 3, 4.... etc.  In this case I have used a modulas operator. 
                {
                    MetroSound[0].Play();
                }
                else
                {
                    MetroSound[1].Play();
                }

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

    int[] crearRelleno(List<int> clave, int dimension)
    {
        int[] relleno = new int[dimension];
        
        int contador = -1;
        foreach (int i in clave)
        {
            contador+=i;
            relleno[contador] = 1;
        }
        
        return relleno;
    }

    int[] getClaveArray(int[] relleno)
    {
        int[] claveArray = new int[relleno.Length];

        int contador = 0;
        foreach (int i in relleno)
        {
            if(i==0)
                claveArray[contador] = 1;
            else
                claveArray[contador] = 0;
            
            contador += 1;
        }

        return claveArray;
    }

    void crearClave(int cantidadSubdivision, int subdivisionBase)
    {
        int[] posibleSubdivision = {1,2,4};

        int subdivisionRandom = posibleSubdivision[Random.Range(0,posibleSubdivision.Length)];

        int subdivisionClave = subdivisionRandom * subdivisionBase;



        Debug.Log("Subdivision Clave es 1/"+ subdivisionClave);

        List<int> claveFinal = getGrupoClave(subdivisionRandom * cantidadSubdivision);
        Debug.Log("Clave final = "+string.Join(", ", claveFinal));


        int[] relleno = crearRelleno(claveFinal,subdivisionRandom * cantidadSubdivision);
        Debug.Log("Relleno final = "+string.Join(", ", relleno));

        int[] claveArray = getClaveArray(relleno);
        Debug.Log("Clave array = "+string.Join(", ", claveArray));
    }
}

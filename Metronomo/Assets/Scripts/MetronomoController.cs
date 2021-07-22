using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomoController : MonoBehaviour
{
    [Header("Metronome Sound")]
    public AudioSource[] MetroSound;

    [Header("Beat Interval")]
    public double Interval; //The interval between the ticks

    [Header("Beat SubInterval")]
    public double SubInterval; 

    [Header("is subInterval?")]
    public bool isSubInterval; 

    private bool lastIsSubInterval;

    private IEnumerator IEtickRoutine;
    private IEnumerator IEsemiTickRoutine;
    


    [Header("Set the Tempo")]
    public double BPM = 120; //The number set in the inspector to set the desired Tempo

    [Header("Count How Many Beats Go By")]
    public int Counter; // Set in the inspector how much time is ellapsed.

    [Header("Set Your Duration Needed")]
    public int setTheTime = 1000;

    //[Header("Place Your GameObject Here")]
    //public GameObject Cube; //Place the game object you wish to change colour here

    [Header("This will change the number of beats in the bar. For Example ~ 4/4 , 5/4 , 6/4")]
    public int NumberOfBeatsInBar = 4;




    public void Awake()
    {
        //We set ticks to divide the Tempo by 60(which represents a minute, to get the number between the beats). We do this in the awake function so this information is set before the first frame of the game.
        Interval = 60.0f / BPM;
        SubInterval = Interval/2;
        
    }





    // Start is called before the first frame update
    void Start()
    {
        StartTickRoutines(false);
        
        
    }

    void StartTickRoutines(bool sub)
    {
        try
        {
            StopCoroutine(IEtickRoutine);
        }
        catch
        {}

        try
        {
            StopCoroutine(IEsemiTickRoutine);
        }
        catch
        {}
            
        
        

        IEtickRoutine = tickRoutine();
        StartCoroutine(IEtickRoutine);

        if(sub)
        {
            IEsemiTickRoutine = semiTickRoutine();
            StartCoroutine(IEsemiTickRoutine);

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isSubInterval)
        {
            if (isSubInterval != lastIsSubInterval)
            {
                StartTickRoutines(true);
                lastIsSubInterval = isSubInterval;
            } 
        }
        else
        {
            if (isSubInterval != lastIsSubInterval)
            {
                StartTickRoutines(false);
                lastIsSubInterval = isSubInterval;
            }

        }
        
        /*
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(tickRoutine()); //Starting our coroutine by pressing spacebar
        }

        if (Input.GetKeyDown(KeyCode.Space))
        */
        
    }


    IEnumerator tickRoutine()
    {
        //While the time is less than 1000( You can use any number for this, but it seems to multiply it by 2 what ever you pick) this is the amount of time the metronome runs for
        while (Time.time < setTheTime) //decided to use a while statement because it seemed like the bst option for a continuingly playing beat
        {
            Counter++;
            if (Counter % NumberOfBeatsInBar == 1)             //on the first beat I want to play a different sound, then repeat that pattern every 4 beats 1(Accent), 2, 3, 4, 1(Accent), 2, 3, 4.... etc.  In this case I have used a modulas operator. 
            {
                MetroSound[0].Play();
                //Cube.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                //Debug.Log("Up Beat");
            }
            
            
            else
            {
                MetroSound[1].Play();
                //Cube.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                //Debug.Log("Down Beat");
            }

            


            yield return new WaitForSecondsRealtime((float)Interval); // Because I decided to use doubles for the ticks and BPM to be more acurate with time, we have to explicitly imply it as a float for the WaitForSecondsRealtime method to recognose it.
        }
    }


     IEnumerator semiTickRoutine()
    {
        //While the time is less than 1000( You can use any number for this, but it seems to multiply it by 2 what ever you pick) this is the amount of time the metronome runs for
        while (Time.time < setTheTime) //decided to use a while statement because it seemed like the bst option for a continuingly playing beat
        {

            MetroSound[2].Play();

            yield return new WaitForSecondsRealtime((float)SubInterval); // Because I decided to use doubles for the ticks and BPM to be more acurate with time, we have to explicitly imply it as a float for the WaitForSecondsRealtime method to recognose it.
        }
    }
    

}

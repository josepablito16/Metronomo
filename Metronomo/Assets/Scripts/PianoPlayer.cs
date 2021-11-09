using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlayer : MonoBehaviour
{
    public List<AudioSource> samplesBase = new List<AudioSource>();
    public AudioSource fluteBase = new AudioSource();


    float getPitch(int semitonos)
    {
        return Mathf.Pow(2f, semitonos / 12f);
    }

    public PianoPlayer()
    {
        samplesBase.Add(GameObject.Find("SampleBase1").GetComponent<AudioSource>());
        samplesBase.Add(GameObject.Find("SampleBase2").GetComponent<AudioSource>());
        samplesBase.Add(GameObject.Find("SampleBase3").GetComponent<AudioSource>());
        fluteBase = GameObject.Find("FluteBase").GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //playAcorde(new List<int> { 3, 6, 12 });

    }

    public void playAcorde(List<int> semitonosAcorde)
    {


        samplesBase[0].pitch = getPitch(semitonosAcorde[0]);
        samplesBase[0].Play();

        samplesBase[1].pitch = getPitch(semitonosAcorde[1]);
        samplesBase[1].Play();

        samplesBase[2].pitch = getPitch(semitonosAcorde[2]);
        samplesBase[2].Play();

    }

    public void playMelodía(List<int> semitonosAcorde)
    {

        int index = Random.Range(0, semitonosAcorde.Count);
        fluteBase.pitch = getPitch(semitonosAcorde[index]);
        fluteBase.Play();


    }


    // Update is called once per frame
    void Update()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlayer : MonoBehaviour
{
    public AudioSource[] samplesBase;

    float getPitch(int semitonos)
    {
        return Mathf.Pow(2f, semitonos / 12f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //playAcorde(new List<int> { 3, 6, 12 });
    }

    void playAcorde(List<int> semitonosAcorde)
    {
        samplesBase[0].pitch = getPitch(semitonosAcorde[0]);
        samplesBase[0].Play();

        samplesBase[1].pitch = getPitch(semitonosAcorde[1]);
        samplesBase[1].Play();

        samplesBase[2].pitch = getPitch(semitonosAcorde[2]);
        samplesBase[2].Play();
    }


    // Update is called once per frame
    void Update()
    {

    }

}

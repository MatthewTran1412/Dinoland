using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegaSoundEffects : MonoBehaviour
{
    //Variables

    AudioSource audioSource;


    //Sound Variants

    public AudioClip[] growlClips;

    public AudioClip[] yelpClips;

    public AudioClip[] barkClips;

    public AudioClip[] roarClips;

    public AudioClip[] deathClips;

    [SerializeField]private float timer;
    [SerializeField]private float lasttime;

    //Gather variables

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lasttime=Time.time;
    }
    //Growl Sounds (Random)
    //emergency
    public void Growl()
    {
        int Index = Random.Range(0, growlClips.Length);
        AudioClip clip = growlClips[Index];
        audioSource.PlayOneShot(clip);
    }

    //Yelp Sounds (Random)
    //hurt
    public void Yelp()
    {
        int Index = Random.Range(0, yelpClips.Length);

        AudioClip clip = yelpClips[Index];
        audioSource.PlayOneShot(clip);
    }

    //Bark Sounds (Random)
    //idle
    public void Bark()
    {
        int Index = Random.Range(0, barkClips.Length);
        AudioClip clip = barkClips[Index];
        audioSource.PlayOneShot(clip);
    }

    //Roar Sounds (Random)
    //have enemy
    public void Roar()
    {
        int Index = Random.Range(0, roarClips.Length);

        AudioClip clip = roarClips[Index];
        audioSource.PlayOneShot(clip);
    }

    //Death Sounds (Random)

    public void Death()
    {
        int Index = Random.Range(0, deathClips.Length);

        AudioClip clip = deathClips[Index];
        audioSource.PlayOneShot(clip);
    }
}

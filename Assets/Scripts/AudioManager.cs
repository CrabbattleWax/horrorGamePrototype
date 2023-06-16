using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    private AudioSource track1, track2;
    //private bool isPlayingTrack;
    public static AudioManager instance;
    private bool changeSong;
    private bool canActivate;
    private List<StationaryEnemyScript> enemyList = new List<StationaryEnemyScript>();
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 0.3f;
        GameObject[] enemyGO = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemyGO)
        {
            enemyList.Add(item.GetComponent<StationaryEnemyScript>());
        }
        if(instance == null)
        {
            instance = this;
        }
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track2.loop = true;
        track1.clip = audioClip1;
        track2.clip = audioClip2;
        track1.volume = 0;
        track2.volume = 0.2f;
        track2.Play();
        track1.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        changeSong = false;
        foreach (var item in enemyList)
        {
            if( item.chasing == true)
            {
                changeSong = true;
            }
        }
        if(changeSong == true)
        {
            if (canActivate == false)
            {
                StartCoroutine(FadeToChaseMusic());
                canActivate = true;
            }
        }
        else
        {
            if (canActivate == true)
            {
                StartCoroutine(FadeToCalmMusic());
                canActivate = false;
            }
        }
    }
    
    private IEnumerator FadeToCalmMusic()
    {
        float timeToFade = 2f;
        float timeElapsed = 0;
        while (timeElapsed<timeToFade)
        {
            track2.volume = Mathf.Lerp(0,0.1f,timeElapsed/timeToFade);
            track1.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }
    private IEnumerator FadeToChaseMusic()
    {
        float timeToFade = 2f;
        float timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            track1.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
            track2.volume = Mathf.Lerp(0.1f, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    
}

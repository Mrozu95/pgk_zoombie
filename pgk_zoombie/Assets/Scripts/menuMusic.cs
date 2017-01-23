using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour
{

    public GameObject ja;
    public AudioClip sound;
    private AudioSource source;

    static bool AudioBegin = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            source = GetComponent<AudioSource>();
            source.PlayOneShot(sound, 1F);
            DontDestroyOnLoad(ja);
            AudioBegin = true;
        }
    }
    void Update()
    {
        if (Application.loadedLevelName == "Lvl_1_testing")
        {
            source.Stop();
            AudioBegin = false;
        }
    }
}

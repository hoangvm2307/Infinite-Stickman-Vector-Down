using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
        backgroundMusicSource = GetComponent<AudioSource>();
    }
    [SerializeField] public  AudioSource backgroundMusicSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

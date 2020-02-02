using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    private AudioSource audio_source;
    private float vol = 1.0f;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    void Update()
    {
        audio_source.volume = vol;
    }

    public void SetVolume(float volume)
    {
        vol = volume;
    }
}

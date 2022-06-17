using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    public void Play()
    {
        m_AudioSource.Play();
    }
    public void ChangeVolume(float volume)
    {
        m_AudioSource.volume = volume;
    }
    public void Mute(bool mute)
    {
        m_AudioSource.mute = mute;
    }
}
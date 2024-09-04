using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] musicsounds;
    public SFX_Sounds[] SfxSounds;
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource SFXLoopSource;
    public static AudioManager instance;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
    }



    public void PlayMusic(string Audionames)
    {
        Sounds sounds = Array.Find(musicsounds, sounds => sounds.AudioName == Audionames);

        if(sounds == null)
        {
            print("Audio not found");
        }
        else
        {
            musicSource.clip = sounds.AudioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string Sfxnamee)
    {
        SFX_Sounds SFX = Array.Find(SfxSounds, SFX => SFX.SFXName == Sfxnamee);

        if (SFX == null)
        {
            print("Audio not found");
        }
        else
        {
            SFXSource.clip = SFX.SFXClips;
            SFXSource.loop = false;
            SFXSource.Play();
        }
    }

    public void PlaySFXLoop(string Sfxnames)
    {
        SFX_Sounds LoopSFX = Array.Find(SfxSounds, LoopSFX => LoopSFX.SFXName == Sfxnames);

        if (LoopSFX == null)
        {

            print("SFX LOOP NOT FOUND");
        }
        else
        {
            SFXLoopSource.clip = LoopSFX.SFXClips;
            SFXLoopSource.loop = true;
            SFXLoopSource.Play();
        }
    }

    public void StopSFXLoop()
    {
        if (SFXLoopSource.isPlaying)
        {
            SFXLoopSource.Stop();
        }
    }

    public void StopSFX()
    {
        if (SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource BackgroundMusic;
    [SerializeField]
    private AudioSource SoundEffect;

    public void PlaySong(string name)
    {
        BackgroundMusic.clip = ResourceManager.Instance.MusicDictionary[name];
        BackgroundMusic.Play();
    }

    public void PlaySfx(string name)
    {
        SoundEffect.PlayOneShot(ResourceManager.Instance.SfxDictionary[name]);
    }

    public void StopSounds()
    {
        BackgroundMusic.Stop();
        SoundEffect.Stop();
    }
}

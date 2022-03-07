using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource BackgroundMusic;
    [SerializeField]
    private AudioSource SoundEffect;

    [SerializeField]
    private AudioSource BeamEffect;

    public void PlaySong(string name)
    {
        BackgroundMusic.clip = ResourceManager.Instance.MusicDictionary[name];
        BackgroundMusic.Play();
    }

    public void PlaySfx(string name, float volume = 1.0f)
    {
        SoundEffect.PlayOneShot(ResourceManager.Instance.SfxDictionary[name], volume);
    }

    public void PlayBeamSound(string name)
    {
        BeamEffect.clip = ResourceManager.Instance.SfxDictionary[name];
        BeamEffect.Play();
    }

    public void StopBeamSound()
    {
        BeamEffect.Stop();
    }

    public void StopSounds()
    {
        BackgroundMusic.Stop();
        SoundEffect.Stop();
        BeamEffect.Stop();
    }
}

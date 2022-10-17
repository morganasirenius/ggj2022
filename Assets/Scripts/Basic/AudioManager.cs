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

    [SerializeField]
    private AudioSource DarkBeamEffect;

    public void PlaySong(string name)
    {
        BackgroundMusic.clip = ResourceManager.Instance.MusicDictionary[name];
        BackgroundMusic.Play();
    }

    public void PlaySfx(string name, float volume = 1.0f)
    {
        SoundEffect.PlayOneShot(ResourceManager.Instance.SfxDictionary[name], volume);
    }

    public void PlayRandomRescueSfx(float volume = 1.0f)
    {
        string sfxName = ResourceManager.Instance.RescueSfxNames[Random.Range(0, ResourceManager.Instance.RescueSfxNames.Count)];
        SoundEffect.PlayOneShot(ResourceManager.Instance.RescueSfxDictionary[sfxName], volume);
    }


    public void PlayBeamSound(string name, float volume = 1.0f)
    {
        BeamEffect.clip = ResourceManager.Instance.SfxDictionary[name];
        BeamEffect.volume = volume;
        BeamEffect.Play();
    }

    public void StopBeamSound()
    {
        BeamEffect.Stop();
    }


    public void PlayDarkBeamSound(string name, float volume = 1.0f)
    {
        DarkBeamEffect.clip = ResourceManager.Instance.SfxDictionary[name];
        DarkBeamEffect.volume = volume;
        DarkBeamEffect.Play();
    }

    public void StopDarkBeamSound()
    {
        DarkBeamEffect.Stop();
    }

    public void StopSounds()
    {
        BackgroundMusic.Stop();
        SoundEffect.Stop();
        BeamEffect.Stop();
        DarkBeamEffect.Stop();
    }
}

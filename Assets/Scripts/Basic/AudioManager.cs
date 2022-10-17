using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Background = 0,
    SFX,
    LightBeam,
    DarkBeam
}

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

    public void Start()
    {
        if (PlayerPrefs.HasKey(Globals.BackgroundVolumeKey))
        {
            BackgroundMusic.volume = PlayerPrefs.GetFloat(Globals.BackgroundVolumeKey);
            SoundEffect.volume = PlayerPrefs.GetFloat(Globals.SFXVolumeKey);
            BeamEffect.volume = PlayerPrefs.GetFloat(Globals.BeamVolumeKey);
            DarkBeamEffect.volume = PlayerPrefs.GetFloat(Globals.BeamVolumeKey);
        }

    }

    public void PlayMusic(string name)
    {
        BackgroundMusic.clip = ResourceManager.Instance.MusicDictionary[name];
        BackgroundMusic.Play();
    }

    public void PlaySfx(string name, float volume = -1f)
    {
        if (volume > -1)
        {
            SoundEffect.PlayOneShot(ResourceManager.Instance.SfxDictionary[name], volume);
        }
        else
        {
            SoundEffect.PlayOneShot(ResourceManager.Instance.SfxDictionary[name]);
        }
    }

    public void PlayRandomRescueSfx(float volume = -1f)
    {
        string sfxName = ResourceManager.Instance.RescueSfxNames[Random.Range(0, ResourceManager.Instance.RescueSfxNames.Count)];
        if (volume > -1)
        {
            SoundEffect.PlayOneShot(ResourceManager.Instance.RescueSfxDictionary[sfxName], volume);
        }
        else
        {
            SoundEffect.PlayOneShot(ResourceManager.Instance.RescueSfxDictionary[sfxName]);
        }
    }


    public void PlayBeamSound(string name, float volume = -1f)
    {
        if (volume > -1)
        {
            BeamEffect.volume = volume;
        }
        BeamEffect.clip = ResourceManager.Instance.SfxDictionary[name];
        BeamEffect.Play();
    }

    public void StopBeamSound()
    {
        BeamEffect.Stop();
    }

    public void PlayDarkBeamSound(string name, float volume = -1)
    {
        if (volume > -1)
        {
            DarkBeamEffect.volume = volume;
        }
        DarkBeamEffect.clip = ResourceManager.Instance.SfxDictionary[name];
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

    public void ChangeVolume(SoundType sound, float volume)
    {
        switch (sound)
        {
            case SoundType.Background:
                BackgroundMusic.volume = volume;
                break;
            case SoundType.SFX:
                SoundEffect.volume = volume;
                break;
            case SoundType.LightBeam:
                BeamEffect.volume = volume;
                break;
            case SoundType.DarkBeam:
                DarkBeamEffect.volume = volume;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameSettings : MonoBehaviour
{
    // Setting Sliders
    [SerializeField]
    Slider backgroundVolumeSlider;
    [SerializeField]
    public TMP_Text backgroundPercentText;
    [SerializeField]
    Slider sfxVolumeSlider;
    [SerializeField]
    public TMP_Text sfxPercentText;

    [SerializeField]
    Slider beamVolumeSlider;
    [SerializeField]
    public TMP_Text beamPercentText;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(Globals.BackgroundVolumeKey))
        {
            Debug.Log("No prefs found! Setting default values");
            PlayerPrefs.SetFloat(Globals.BackgroundVolumeKey, 1);
            PlayerPrefs.SetFloat(Globals.SFXVolumeKey, 1);
            PlayerPrefs.SetFloat(Globals.BeamVolumeKey, 1);
        }
        Load();
    }

    public void ChangeBackgroundVolume()
    {
        AudioManager.Instance.ChangeVolume(SoundType.Background, backgroundVolumeSlider.value);
        backgroundPercentText.text = string.Format("{0}%", (int)(backgroundVolumeSlider.value * 100));
        Save(Globals.BackgroundVolumeKey, backgroundVolumeSlider.value);
    }
    public void ChangeSFXVolume()
    {
        AudioManager.Instance.ChangeVolume(SoundType.SFX, sfxVolumeSlider.value);
        sfxPercentText.text = string.Format("{0}%", (int)(sfxVolumeSlider.value * 100));
        Save(Globals.SFXVolumeKey, sfxVolumeSlider.value);
    }

    public void ChangeBeamVolume()
    {
        // TODO: Maybe have separate sliders?
        AudioManager.Instance.ChangeVolume(SoundType.LightBeam, beamVolumeSlider.value);
        AudioManager.Instance.ChangeVolume(SoundType.DarkBeam, beamVolumeSlider.value);
        beamPercentText.text = string.Format("{0}%", (int)(beamVolumeSlider.value * 100));
        Save(Globals.BeamVolumeKey, beamVolumeSlider.value);
    }

    public void Load()
    {
        backgroundVolumeSlider.value = PlayerPrefs.GetFloat(Globals.BackgroundVolumeKey);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(Globals.SFXVolumeKey);
        beamVolumeSlider.value = PlayerPrefs.GetFloat(Globals.BeamVolumeKey);
    }

    public void Save(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
    }

}

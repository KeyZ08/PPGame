using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsChange : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public Slider sliderMusic;
    public Slider sliderEffects;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("MusicVolume") && !PlayerPrefs.HasKey("EffectsVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("EffectsVolume", 1);
        }
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        sliderEffects.value = PlayerPrefs.GetFloat("EffectsVolume");
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, sliderMusic.value));
        mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, sliderEffects.value));
    }

    public void MusicChange()
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, sliderMusic.value));
        PlayerPrefs.SetFloat("MusicVolume", sliderMusic.value);
    }

    public void EffectsChange()
    {
        mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, sliderEffects.value));
        PlayerPrefs.SetFloat("EffectsVolume", sliderEffects.value);
    }
}

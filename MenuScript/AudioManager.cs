using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider, audioSlider;

    private static AudioManager audioManager;

    public static AudioManager getInstance()
    {
        if (audioManager == null)
        {
            return audioManager = new AudioManager();
        }
        return audioManager;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetAudioVolume()
    {
        float volume = audioSlider.value;
        mixer.SetFloat("Audio", Mathf.Log10(volume) * 20);
    }

    // Start is called before the first frame update
    public void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadVolume();
        }
        else{
            SetMusicVolume();
            SetAudioVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        audioSlider.value = PlayerPrefs.GetFloat("Audio");
        SetMusicVolume();
        SetAudioVolume();
    }
}

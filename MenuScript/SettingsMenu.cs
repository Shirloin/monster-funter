using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioClip clip;
    //public Dropdown dropdown;
    public Resolution[] resolution;

    public string clipName;
    // Start is called before the first frame update
    void Start()
    {
        //resolution = Screen.resolutions;
        //dropdown.ClearOptions();
        //List<string> options = new List<string>();
        //for(int i=0; i<resolution.Length; i++)
        //{
        //    string option = resolution[i].width + "x" + resolution[i].height;
        //    options.Add(option);
        //}
        //dropdown.AddOptions(options);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuScript : MonoBehaviour
{
    public GameObject mainCam;
    public CinemachineVirtualCamera VCam;
    public GameObject SettingMenuPanel;
    public GameObject MainMenuPanel;
    public GameObject CharacterMenuPanel;
    public GameObject LoadingPanel;
    public AudioSource bgMusic;
    public PlayerSelection ps = PlayerSelection.Instance();

    // Start is called before the first frame update
    void Start()
    {
        mainCam.SetActive(true);
        CharacterMenuPanel.SetActive(false);
        SettingMenuPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        bgMusic.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void settingMenu()
    {
        Debug.Log("def");
        SettingMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        CharacterMenuPanel.SetActive(false);
        LoadingPanel.SetActive(false);
    }

    public void returnCamera()
    {
        VCam.Priority = 11;
        MainMenuPanel.SetActive(true);
        SettingMenuPanel.SetActive(false);
        CharacterMenuPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        ps.Reset();
    }

    public void CharacterMenu()
    {
        VCam.Priority = 9;
        MainMenuPanel.SetActive(false);
        SettingMenuPanel.SetActive(false);
        CharacterMenuPanel.SetActive(true);
        LoadingPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

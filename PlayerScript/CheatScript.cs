using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheatScript : MonoBehaviour
{

    public GameObject winPanel;
    public GameObject losePanel;
    private PlayerMovement pm;
    private FlyScript fs;

    private List<string> str;
    private PanelController pc;
    private BarController bc;


    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        fs = GetComponent<FlyScript>();
        bc = GetComponent<BarController>();
        pc = PanelController.instance;
        str = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        AddKeytoList(GetKeyPressed().ToString());
        DetectCheat();
    }

    private void DetectCheat(){
        if(GetStr().Contains("HESOYAM")){
            bc.Cheat();
            pc.InfoActivated(0);
            str.Clear();
        }
        else if(GetStr().Contains("IHATEYOU")){
            //addplayer movement speed
            pm.SpeedCheat();
            pc.InfoActivated(0);
            str.Clear();
        }
        else if(GetStr().Contains("ILOVEYOU")){
            //make player immediately die
            pc.Lose();
            pc.InfoActivated(0);
            str.Clear();
        }
        else if(GetStr().Contains("ICANFLY")){
            fs.FlyCheat();
            pc.InfoActivated(0);
            str.Clear();
        }
        else if(GetStr().Contains("BUDI")){
            //change character
            pm.ChangeCharacterCheat();
            pc.InfoActivated(0);
            str.Clear();
        }
        else if(GetStr().Contains("WIN")){
            PanelController.instance.Win();
            str.Clear();
        }
    }

    private string GetStr(){
        return string.Join("",str.ToArray());
    }

    private KeyCode GetKeyPressed(){
        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode))){
            if(Input.GetKeyDown(key)){
                return key;
            }
        }
        return KeyCode.None;
    }

    private void AddKeytoList(string keyStroke){
        if(!keyStroke.Equals("None")){
            str.Add(keyStroke);
            if(str.Count > 50){
                str.RemoveAt(0);
            }
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class AstraDialogue : MonoBehaviour {
    public Text textUI;
    private string lines = "Go go power ranger";
    private float speed = 0.5f;

    private bool mission = false;

    public static AstraDialogue instance = null;

    private void Awake() {
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        textUI.text = string.Empty;
        textUI.enabled = false;
    }

    public void StartDialogue(){
        textUI.text = "";
        textUI.enabled = true;
        if(mission==false && Mission.instance.X == 3 ){
            mission = true;
        }
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach(char c in lines){
            textUI.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    public void ResetDialogue(){
        textUI.enabled = false;
    }

    public bool GetMission{
        get{return mission;}
    }
}
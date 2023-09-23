using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LyraDialogue : MonoBehaviour {
    public Text textUI;
    private string lines = "Muchas gracias aficion esto es para vosotros";
    private float speed = 0.5f;

    private bool mission = false;

    public static LyraDialogue instance = null;

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

    private void Update() {
    }

    public void StartDialogue(){
        textUI.text = "";
        textUI.enabled = true;
        if(mission==false){
            mission = true;
        }
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in lines){
            textUI.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ResetDialogue(){
        textUI.enabled = false;
    }

    public bool Mission{
        get{return mission;}
    }
}
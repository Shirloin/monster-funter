using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour {
    private bool isClose;
    private bool isOpen;

    private void OnTriggerEnter(Collider other) {
        isClose = true;
    }
    private void OnTriggerExit(Collider other) {
        isClose = false;
    }

    private void Update() {
        if(isClose){
            if(Input.GetKeyDown(KeyCode.B)){
                if(isOpen){
                    LeanTween.rotateY(gameObject, 0, 0.5f);
                    isOpen = false;
                }
                else{
                    LeanTween.rotateY(gameObject, 90, 0.5f);
                    isOpen = true;
                }
            }
        }
        else{
             LeanTween.rotateY(gameObject, 0, 0.5f);
            isOpen = false;
        }
    }
}
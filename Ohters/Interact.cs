using UnityEngine;

public class Interact : MonoBehaviour {
        private bool isCollide = false;
    
    private void OnTriggerStay(Collider other) {
        if(isCollide){
            if(Input.GetKeyDown(KeyCode.C)){
                if(other.name == "Lyra"){
                    LyraDialogue.instance.StartDialogue();
                }
                else if(other.name == "Remy"){
                    RemyDialogue.instance.StartDialogue();
                }
                else if(other.name == "Astra"){
                    AstraDialogue.instance.StartDialogue();
                }
                else if(other.name == "Meat" || other.name == "Potion"){
                    Inventory.instance.AddItem(other.name);
                    Destroy(other.gameObject);
                }
                PanelController.instance.ResetGuider();
            }
            if(Input.GetKeyDown(KeyCode.J)){
                if(other.name == "Portal"){
                    ChangeScene.instance.MazeScene();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        isCollide = true;
        if(other.name == "Lyra" || other.name == "Remy" || other.name == "Astra"){
            PanelController.instance.ShowGuider(other.name, 1);
        }
        else if(other.name == "Meat" || other.name == "Potion"){
            PanelController.instance.ShowGuider(other.name, 0);
        }
        else if(other.name == "Door"){
            PanelController.instance.ShowGuider(other.name, 100);
        }
        else if(other.name == "Portal"){
            PanelController.instance.ShowGuider(other.name, 2);
        }
    }

    private void OnTriggerExit(Collider other) {
        isCollide = false;
        PanelController.instance.ResetGuider();
        LyraDialogue.instance.ResetDialogue();
    }


}
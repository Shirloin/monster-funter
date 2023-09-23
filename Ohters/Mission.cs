using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour {
    public Image img;
    public Text textUI;

    private bool[] missionComplete = new bool[15];

    private float timer = 3f;
    private float hideTimer = 3f;
    private bool hide = false;
    private int x;

    public static Mission instance = null;
    private WizardAttack wa;
    private FlyScript fs;

    private void Awake() {
        wa = GetComponent<WizardAttack>();
        fs = GetComponent<FlyScript>();
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        img.enabled = false;
        textUI.enabled = false;
        x = 0;
    }

    private void Update(){
        MissionTimer();
        if(hide){
            HideTimer();
        }
        if(timer <= 0.0f && !hide){
            textUI.color = Color.white;
            if(!missionComplete[0]){
                if(LyraDialogue.instance.Mission){
                    MissionComplete();
                }
                FirstMission();
            }
            else if(!missionComplete[1]){
                if(wa.GetMission){
                    MissionComplete();
                }
                SecondMission();
            }
            else if(!missionComplete[2]){
                if((fs.GetMission && wa.SkillMission)){
                    MissionComplete();
                }
                ThirdMission();
            }
            else if(!missionComplete[3]){
                if(AstraDialogue.instance.GetMission && RemyDialogue.instance.GetMission){
                    MissionComplete();
                }
                FourthMission();
            }
            else if(!missionComplete[4]){
                FifthMission();
            }
        }
    }

    public void MissionComplete(){
        missionComplete[x] = true;
        x++;
        textUI.color = Color.green;
        hideTimer = 3f;
        hide = true;
        PanelController.instance.InfoActivated(1);
    }

    private void HideTimer(){
        if(hideTimer > 0.0f){
            hideTimer -= Time.deltaTime;
        }
        else{
            img.enabled = false;
            textUI.enabled = false;
            hide = false;
            timer = 3f;
        }
    }

    private void MissionTimer(){
        if(timer > 0.0f){
            timer -= Time.deltaTime;
        }
    }

    private void FirstMission(){
        img.enabled = true;
        textUI.text = "Find and talk to Lyra";
        textUI.enabled = true;
    }

    private void SecondMission(){
        img.enabled = true;
        textUI.text = "Use basic attack 10 times";
        textUI.enabled = true;
    }

    private void ThirdMission(){
        img.enabled = true;
        textUI.text = "Use all skill available";
        textUI.enabled = true;
    }

    private void FourthMission(){
        img.enabled = true;
        textUI.text = "Talk to all the people in the Village";
        textUI.enabled = true;
    }

    private void FifthMission(){
        img.enabled = true;
        textUI.text = "Go to maze using the portal object and kill all the enemies";
        textUI.enabled = true;
    }

    public int X{
        get{return x;}
    }
}
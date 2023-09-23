using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour {
    public GameObject gamePanel, pausePanel, winPanel, losePanel, loadingPanel;
    public Image infoImage;
    public Text infoText;
    private float timer;

    [SerializeField]
    private Image Guider;
    [SerializeField]
    private Text GuiderText;
    public static PanelController instance = null;

    private void Start() {
        Reset();
        Game();
        ResetCheat();
        ResetGuider();
        timer = 0f;
    }


    private void Awake() {
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        Reset();
        Game();
        ResetCheat();
        ResetGuider();
        timer = 0f;
    }

    private void Update(){
        Timer();
    }

    public void PauseGame(){
        Reset();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void MenuScene(){
        Time.timeScale = 1;
        Reset();
        ChangeScene.instance.MenuScene();
    }
    public void Resume(){
        Time.timeScale = 1;
        gamePanel.SetActive(true);
        Reset();
    }

    public void Game(){
        Reset();
        gamePanel.SetActive(true);
        Time.timeScale = 1;
        // ChangeScene.instance.GameScene(); 
    }

    
    public void Win(){
        Reset();
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose(){
        Reset();
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Loading(){
        Reset();
        Time.timeScale = 1;
        loadingPanel.SetActive(true);
    }

    public void InfoActivated(int i){
        if(i==0){
            infoText.text = "CHEAT ACTIVATED";
        }
        else{
            infoText.text = "MISSION COMPLETED";
        }
        infoImage.enabled = true;
        infoText.enabled = true;
        timer = 3f;
    }

    private void Timer(){
        if(timer <= 0.0f){
            ResetCheat();
        }
        if(timer > 0.0f){
            timer -= Time.deltaTime;
        }
    }

    private void ResetCheat(){
        infoImage.enabled = false;
        infoText.enabled = false;
    }

    public void ShowGuider(string name, int i){
        if(i==0){
            GuiderText.text = "PRESS [C] TO TAKE A " + name.ToUpper();
        }
        else if(i==1){
            GuiderText.text = "PRESS [C] TO TALK TO " + name.ToUpper();
        }
        else if(i==2){
            GuiderText.text = "PRESS [J] TO USE THE PORTAL";
        }
        else{
            GuiderText.text = "PRESS [B] TO OPEN";
        }
        GuiderText.enabled = true;
        Guider.enabled = true;
    }

    public void ResetGuider(){
        GuiderText.enabled = false;
        Guider.enabled = false;
    }

    public void Reset(){
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        loadingPanel.SetActive(false); 
    }
    
}
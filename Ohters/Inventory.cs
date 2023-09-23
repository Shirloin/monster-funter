using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour {
    int currPos = 1;
    public Text txtUI;
    public Image img1, img2;
    public Sprite meatSprite, potionSprite;

    public static Inventory instance = null;
    private void Awake() {
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        UpdateUI();
    }

    

    private void Update() {

        //Waktu mau ganti
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(currPos==1){
                currPos = 2;
            }
            else if(currPos==2){
                currPos = 1;
            }
            UpdateUI();
        }
        //Waktu mau pake
        else if (Input.GetKeyDown(KeyCode.G))
        {
            if(currPos==1){
                if(GameManager.instance.Meat>0){
                    GameManager.instance.Stamina = 100f;
                    GameManager.instance.Meat--;
                }
            }
            else if(currPos==2){
                if(GameManager.instance.Potion > 0){
                    GameManager.instance.Health += 50f;
                    GameManager.instance.Potion--;
                }
            }
            UpdateUI();
        }
    }

    private void UpdateUI(){
        if(currPos==1){
            img1.sprite = meatSprite;
            img2.sprite = potionSprite;
            txtUI.text = "Meat: " + GameManager.instance.Meat.ToString();
        }
        else if(currPos==2){
            img1.sprite = potionSprite;
            img2.sprite = meatSprite;
            txtUI.text ="Potion: " + GameManager.instance.Potion.ToString();
        }
    }

    public void AddItem(string name){
        if(name == "Meat"){
            GameManager.instance.Meat++;
        }
        else if(name == "Potion"){
            GameManager.instance.Potion++;
        }
        UpdateUI();
    }
}
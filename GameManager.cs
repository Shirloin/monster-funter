using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;
    private static float health = 100f;
    private static float stamina = 100f;
    private int meat = 2, potion = 1;

    private void Awake() {
        if(instance==null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PanelController.instance.PauseGame();
        }
    }

    public float Health{
        get{return health;}
        set{health = value;}
    }
    public float Stamina{
        get{return stamina;}
        set{stamina = value;}
    }

    public int Meat{
        get{return meat;}
        set{meat = value;}
    }
    public int Potion{
        get{return potion;}
        set{potion = value;}
    }
}
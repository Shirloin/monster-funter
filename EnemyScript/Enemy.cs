using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    
    private float health = 30f;
    private float swipeDamage = 10f;
    private float damageDelay = 0.3f;

    [SerializeField]
    public Slider healthBar;

    public static Enemy instance;

    private void Awake() {
        if(instance==null){
            instance = this;
        }
        else if(instance!=this){
            Destroy(gameObject);
        }
        healthBar.value = health;
    }
    private void Update() {
        healthBar.value = health;
    }

    public void TakeDamage(){
        health -= 10;
    }
    public float Health{
        get{return health;}
    }
}
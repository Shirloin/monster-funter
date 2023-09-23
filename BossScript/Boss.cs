using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    private float health = 600f;
    private float swipeDamage = 20f;
    private float punchDamage = 20f;
    private float jumpAttackDamage = 50;
    private float fireBreathDamage = 5;
    private float damageDelay = 0.6f;

    [SerializeField]
    public Slider healthBar;

    public static Boss instance;

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
        health -= 50;
    }

    public float DamageDelay{
        get{return damageDelay;}
    }

    public float Health{
        get{return health;}
        set{health = value;}
    }

    public float SwipeDamage{
        get{return swipeDamage;}
    }

    public float PunchDamage{
        get{return punchDamage;}
    }

    public float JumpAttackDamage{
        get{return jumpAttackDamage;}
    }
    public float FireBreathDamage{
        get{return fireBreathDamage;}
    }

}
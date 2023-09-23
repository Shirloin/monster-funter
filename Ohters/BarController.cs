using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    private float currHealth;
    private float currStamina;
    private float maxHealth = 100f;
    private float maxStamina = 100f;
    private float staminaDecreaseSpeed = 10f;
    private float regenStamina = 5f;

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        healthBar.value = GameManager.instance.Health;
        staminaBar.value = GameManager.instance.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.Stamina > maxStamina){
            GameManager.instance.Stamina = maxStamina;
        }
        if(GameManager.instance.Health > maxHealth){
            GameManager.instance.Health = maxHealth;
        }
        RegenStamina();
        staminaBar.value = GameManager.instance.Stamina;
        healthBar.value = GameManager.instance.Health;
    }

    public void Running()
    {
        if(GameManager.instance.Stamina > 0.0f){
            GameManager.instance.Stamina -= staminaDecreaseSpeed * Time.deltaTime;
        }
    }

    public void RegenStamina(){
        if(GameManager.instance.Stamina < maxStamina){
            GameManager.instance.Stamina += regenStamina * Time.deltaTime;
            // if(GameManager.instance.Stamina > maxStamina){
            //     GameManager.instance.Stamina = 100f;
            // }
        }
    }

    public void ReduceHealth(){
        GameManager.instance.Health -= 10;
    }

    public void Cheat(){
        healthBar.value = GameManager.instance.Health = maxHealth;
        staminaBar.value = GameManager.instance.Stamina = maxStamina;
    }

}

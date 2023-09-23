using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    BarController barController;
    public static PlayerController instance;

    private void Awake() {
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        barController = GetComponent<BarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isPlayerRunning())
        {
            barController.Running();
        }
        else{
            barController.RegenStamina();
        }
        if(GameManager.instance.Health <= 0.0f){
            playerMovement.playerAnim.SetTrigger("Death");
            StartCoroutine(Die());
        }
    }
    public void TakeDamage(float damage){
        GameManager.instance.Health -= damage;
    }

    private void OnCollisionEnter(Collision other) {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "Jump"){
            TakeDamage(40);
        }
    }

    IEnumerator Die(){
        yield return new WaitForSeconds(4f);
        PanelController.instance.Lose();
    }

    

    
}

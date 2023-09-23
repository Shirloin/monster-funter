using UnityEngine;
using System.Collections;


public class PaladinAttack : MonoBehaviour {
    private int clickCount;
    private bool isAttack;
    private int currAtt;
    private float coolDownTime = 1f;
    private float lastClickTime = 0f;
    private float maxComboDelay = 1f;
    private float animatorSpeed = 1f;

    private PlayerMovement playerMovement;

    private bool isRolling = false;
    private float cooldownRoll = 3f;
    private bool isRollCooldown = false;
    public AudioClip rollSound;
    private float rollTimer;
    private float rollCooldown;
    private bool isRageCooldown;

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        isRageCooldown = false;
    }

    public void BasicAttack(){
        int a = playerMovement.playerAnim.GetInteger("Attack");
        float b = playerMovement.playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(playerMovement.controller.isGrounded){
            if(Input.GetMouseButtonDown(0)){
                lastClickTime = Time.time;
                clickCount++;
                isAttack = true;
                coolDownTime = 2f;

                if(clickCount == 1){
                    SetAnimator(1);
                }
                clickCount = Mathf.Clamp(clickCount, 0, 3);
                if(clickCount >= 2 && b > 0.7f && a==1){
                    SetAnimator(2);
                }
                if(clickCount >= 3 && b > 0.7f && a==2){
                    SetAnimator(3);
                }
            }
            if(Time.time - lastClickTime > coolDownTime ){
                if(a==1 && b > 0.7f){
                    SetAnimator(0);
                }
                if(a==2 && b > 0.7f){
                    SetAnimator(0);
                }
                if(a==3 && b > 0.7f){
                    SetAnimator(0);
                }
                clickCount = 0;
                isAttack = false;
            }
        }
    }

    public void RageSkill(){
        if(playerMovement.controller.isGrounded && !isRageCooldown){
            if(Input.GetKeyDown(KeyCode.R)){
                playerMovement.playerAnim.SetTrigger("Rage");
                playerMovement.IncreaseSpeed();
                isRageCooldown = true;
                StartCoroutine(RageCooldown());
            }
        }
    }

    void SetAnimator(int i){
         playerMovement.playerAnim.SetInteger("Attack", i);
    }

    public void RollSkill(){
        if(playerMovement.controller.isGrounded){
            if(Input.GetKeyDown(KeyCode.F) && !isRolling && rollCooldown <= 0.0f){
                playerMovement.playerAnim.SetTrigger("Rolling");
                isRolling = true;
                isRollCooldown = true;
                rollTimer = 2.5f;
                rollCooldown = 3f;
                // AudioSource.PlayClipAtPoint(rollSound, transform.position);
            }
            if(rollTimer > 0.0f && isRolling){
                Vector3 dir;
                if(rollTimer <= 2 && rollTimer >= 1){
                    dir = -transform.forward * 0.8f;
                }
                else{
                     dir = -transform.forward * 0.5f;
                }
                transform.Translate(dir * Time.deltaTime);
                rollTimer -= Time.deltaTime;
            }
            if(rollCooldown > 0.0f){
                rollCooldown -= Time.deltaTime;
            }
            if(rollTimer > 0.0f){
                print("rollnot done");
            }
            if(rollTimer <= 0.0f){
                print("rolling done");
                isRolling = false;
            }
        }
    }

    private IEnumerator RageCooldown(){
        yield return new WaitForSeconds(5f);
        isRageCooldown = false;
    }

}
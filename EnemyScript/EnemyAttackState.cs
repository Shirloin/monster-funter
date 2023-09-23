using UnityEngine;
using System.Collections;
using System;

public class EnemyAttackState : EnemyState
{
    private System.Random rand = new System.Random(DateTime.Now.Millisecond);
    private bool isCooldown = false;
    private bool isAttacking = false;
    private bool hasTakenDamage = false;
    private EnemyAttackFactory af = new EnemyAttackFactory();
    private float delay = 3f;
    public void EnterState(EnemyController controller)
    {
        isCooldown = false;
        isAttacking = false;
        hasTakenDamage = false;
    }

    public void ExitState(EnemyController controller)
    {
        controller.GetAnimator.ResetTrigger("Swipe1");
        controller.GetAnimator.ResetTrigger("Swipe2");
        controller.GetAnimator.ResetTrigger("Swipe3");
    }

    public void UpdateState(EnemyController controller)
    {
        AnimatorStateInfo stateInfo = controller.GetAnimator.GetCurrentAnimatorStateInfo(0);
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Standing Melee Attack 360 High"){
            Vector3 relativePos = controller.Target.position - controller.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion curr = controller.transform.localRotation;
            controller.transform.localRotation = Quaternion.Slerp(curr, rotation, Time.deltaTime * 5f);
        }
        float distance = (controller.GetAgent.transform.position - controller.Target.transform.position).magnitude;
        if(distance >= 3f){
            controller.ChangeState(new EnemyChaseState());
        }
        // Debug.Log(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Mutant Swiping" && stateInfo.normalizedTime > 0.59f && stateInfo.normalizedTime < 0.6f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(10);
        }
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Zombie Punching" && stateInfo.normalizedTime > 0.3f && stateInfo.normalizedTime < 0.31f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(10);
        }
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Standing Melee Attack 360 High" && stateInfo.normalizedTime > 0.3f && stateInfo.normalizedTime < 0.31f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(10);
        }
        if(isCooldown){
            Delay();
        }
        if(!isCooldown){
            int i = rand.Next(3);
            Debug.Log(i);
            EnemyAttack ba = af.GetAttack(i);
            ba.Attack(controller);
            hasTakenDamage = false;
            isCooldown = true;
            delay = 5f;
        }
    }

    void Delay(){
        delay -= Time.deltaTime;
        if(delay <= 0){
            isCooldown = false;
        }
    }
}
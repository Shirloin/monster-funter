using UnityEngine;
using System.Collections;
using System;

public class AttackingState : BossState
{
    private System.Random rand = new System.Random(DateTime.Now.Millisecond);
    private AttackFactory af = new AttackFactory();

    private float delay = 3f;
    private bool isCooldown = false;
    private bool isAttacking = false;
    private bool hasTakenDamage = false;
    public void EnterState(BossController controller)
    {
        isCooldown = false;
        isAttacking = false;
        hasTakenDamage = false;
    }

    public void ExitState(BossController controller)
    {
        controller.GetAnimator.ResetTrigger("Punch");
        controller.GetAnimator.ResetTrigger("Swipe");
        controller.GetAnimator.ResetTrigger("Jump");
        controller.GetAnimator.ResetTrigger("Fire");
        Debug.Log("Exit State");
    }

    public void UpdateState(BossController controller)
    {
        AnimatorStateInfo stateInfo = controller.GetAnimator.GetCurrentAnimatorStateInfo(0);

        // Debug.Log("Attack State");
        Vector3 relativePos = controller.Target.position - controller.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion curr = controller.transform.localRotation;
        controller.transform.localRotation = Quaternion.Slerp(curr, rotation, Time.deltaTime * 5f);
        float distance = (controller.GetBoss.transform.position - controller.Target.transform.position).magnitude;
        if(distance >= controller.AttackRange){
            controller.ChangeState(new ChaseState());
        }
        Debug.Log(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Mutant Swiping" && stateInfo.normalizedTime > 0.59f && stateInfo.normalizedTime < 0.6f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(20);
        }
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Mutant Flexing Muscles" && stateInfo.normalizedTime > 0.48f && stateInfo.normalizedTime < 0.49f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(20);
        }
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Mutant Roaring" && stateInfo.normalizedTime > 0.3f && stateInfo.normalizedTime < 0.8f){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(0.2f);
        }
        if(controller.GetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="Jump Attack" && stateInfo.normalizedTime > 0.59f && stateInfo.normalizedTime < 0.6f && !hasTakenDamage){
            hasTakenDamage = true;
            PlayerController.instance.TakeDamage(40);
        }
        if(isCooldown){
            Delay();
        }
        if(!isCooldown){
            int i = rand.Next(4);
            Debug.Log(i);
            BossAttack ba = af.GetAttack(i);
            ba.Attack(controller);
            hasTakenDamage = false;
            isCooldown = true;
            delay = 6f;
        }

    }

    void Delay(){
        delay -= Time.deltaTime;
        if(delay <= 0){
            isCooldown = false;
        }
    }
}
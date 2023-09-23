using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BossState
{
    public void EnterState(BossController controller)
    {
        controller.GetAgent.isStopped = false;
        controller.GetAnimator.SetBool("Chase", true); 
        controller.GetAgent.SetDestination(controller.Target.transform.position);
    }

    public void ExitState(BossController controller)
    {
        controller.GetAnimator.SetBool("Chase", false);
        controller.GetAgent.isStopped = true;
    }

    public void UpdateState(BossController controller)
    {
        controller.GetAgent.SetDestination(controller.Target.transform.position);
        // Debug.Log(controller.GetBoss.remainingDistance);
        if(controller.GetBoss.remainingDistance < controller.AttackRange){
            // Debug.Log("Enter Attack Range");
            controller.ChangeState(new AttackingState());
        }
    }
}
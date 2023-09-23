using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public void EnterState(EnemyController controller)
    {
        controller.GetAgent.isStopped = false;
        controller.GetAnimator.SetBool("Chase", true); 
        controller.GetAgent.SetDestination(controller.Target.transform.position);
    }

    public void ExitState(EnemyController controller)
    {
        controller.GetAnimator.SetBool("Chase", false);
        controller.GetAgent.isStopped = true;
    }

    public void UpdateState(EnemyController controller)
    {
        controller.GetAgent.SetDestination(controller.Target.transform.position);
        if(controller.GetAgent.remainingDistance < 3f){
            controller.ChangeState(new EnemyAttackState());
        }
    }
}
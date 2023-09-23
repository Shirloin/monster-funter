using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public void EnterState(EnemyController controller)
    {
        controller.GetAnimator.SetBool("Idle", true);
    }

    public void ExitState(EnemyController controller)
    {
        controller.GetAnimator.SetBool("Idle", false);
    }

    public void UpdateState(EnemyController controller)
    {
        if(RoomManager.instance.IsInEnemyRoom){
            controller.ChangeState(new EnemyChaseState());
        }
    }
}
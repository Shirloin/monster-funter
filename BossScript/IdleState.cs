using UnityEngine;

public class IdleState : BossState
{
    public void EnterState(BossController controller)
    {
        controller.GetAnimator.SetBool("Idle", true);
    }

    public void ExitState(BossController controller)
    {
        controller.GetAnimator.SetBool("Idle", false);
    }

    public void UpdateState(BossController controller)
    {
        if(RoomManager.instance.IsInBossRoom){
            controller.ChangeState(new ChaseState());
        }
    }
}
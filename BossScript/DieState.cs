using UnityEngine;

public class DieState : MonoBehaviour, BossState
{
    public void EnterState(BossController controller)
    {
        controller.GetAnimator.SetTrigger("Death");
    }

    public void ExitState(BossController controller)
    {
        
    }

    public void UpdateState(BossController controller)
    {
        
    }
}
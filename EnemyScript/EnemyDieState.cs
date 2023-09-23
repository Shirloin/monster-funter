using UnityEngine;

public class EnemyDieState : EnemyState
{
    public void EnterState(EnemyController controller)
    {
        controller.GetAnimator.SetTrigger("Die");
    }

    public void ExitState(EnemyController controller)
    {
    }

    public void UpdateState(EnemyController controller)
    {
    }
}
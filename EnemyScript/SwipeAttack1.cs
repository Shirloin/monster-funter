public class SwipeAttack1 : EnemyAttack
{
    public void Attack(EnemyController controller)
    {
        controller.GetAnimator.SetTrigger("Swipe1");
    }
}
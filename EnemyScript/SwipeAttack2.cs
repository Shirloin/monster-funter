public class SwipeAttack2 : EnemyAttack
{
    public void Attack(EnemyController controller)
    {
        controller.GetAnimator.SetTrigger("Swipe2");
    }
}
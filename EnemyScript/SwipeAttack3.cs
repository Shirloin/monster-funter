public class SwipeAttack3 : EnemyAttack
{
    public void Attack(EnemyController controller)
    {
        controller.GetAnimator.SetTrigger("Swipe3");
    }
}
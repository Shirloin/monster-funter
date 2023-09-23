using UnityEngine;
using System.Collections;

public class SwipeAttack :BossAttack
{
    public void Attack(BossController controller)
    {
        controller.GetAnimator.SetTrigger("Swipe");
        controller.hand1.gameObject.tag = "Swipe";
    }
}
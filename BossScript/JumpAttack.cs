using System.Collections;
using UnityEngine;

public class JumpAttack : BossAttack
{
    public void Attack(BossController controller)
    {
        controller.GetAnimator.SetTrigger("Jump");
        controller.hand1.gameObject.tag = "Jump";
        controller.hand2.gameObject.tag = "Jump";
        controller.gameObject.tag = "Jump";
    }
}
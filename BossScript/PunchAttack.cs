using System.Collections;
using UnityEngine;

public class PunchAttack : BossAttack
{
    public void Attack(BossController controller)
    {
        controller.GetAnimator.SetTrigger("Punch");
        controller.hand1.gameObject.tag = "Punch";
        controller.hand2.gameObject.tag = "Punch";
    }
}
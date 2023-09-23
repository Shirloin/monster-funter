using System.Collections;
using UnityEngine;

public class FireBreath : BossAttack
{

    public void Attack(BossController controller)
    {
        controller.GetAnimator.SetTrigger("Fire");
        controller.ActivateFire();
    }
}
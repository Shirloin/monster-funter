using UnityEngine;

public class AttackFactory {
    public BossAttack GetAttack(int i){
        if(i==0){
            return new SwipeAttack();
        }
        else if(i==1){
            return new PunchAttack();
        }
        else if(i==2){
            return new JumpAttack();
        }
        else if(i==3){
            return new FireBreath();
        }
        return null;
    }
}
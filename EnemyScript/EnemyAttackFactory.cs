using UnityEngine;
public class EnemyAttackFactory {
    public EnemyAttack GetAttack(int i){
        if(i==0){
            return new SwipeAttack1();
        }
        else if(i==1){
            return new SwipeAttack2();
        }
        else if(i==2){
            return new SwipeAttack3();
        }
        return null;
    }
}
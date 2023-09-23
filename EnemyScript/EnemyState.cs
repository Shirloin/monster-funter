using UnityEngine;

public interface EnemyState {
    void EnterState(EnemyController controller);
    void UpdateState(EnemyController controller);
    void ExitState(EnemyController controller);

}
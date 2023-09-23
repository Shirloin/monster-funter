using UnityEngine;

public interface BossState {
    void EnterState(BossController controller);
    void UpdateState(BossController controller);
    void ExitState(BossController controller);

}
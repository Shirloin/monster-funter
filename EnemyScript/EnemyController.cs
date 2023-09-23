using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {
    
    private EnemyState currState;
    private Animator animator;

    private NavMeshAgent enemy;

    private Transform target;

    public static EnemyController instance;

    private void Awake(){
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    private void Start() {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeState(new EnemyIdleState());
    }
    private void Update() {
        if(!RoomManager.instance.IsInEnemyRoom){
            ChangeState(new EnemyIdleState());
        }
        if(Enemy.instance.Health<1){
            ChangeState(new EnemyDieState());
        }
        currState.UpdateState(this);
    }

    public void ChangeState(EnemyState newState){
        if(currState != null){
            currState.ExitState(this);
        }
        currState = newState;
        currState.EnterState(this);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Bullet"){
            Enemy.instance.TakeDamage();
            Destroy(other.gameObject);
        }
    }

    public Transform Target{
        get{return target;}
    }

    public Animator GetAnimator{
        get{return animator;}
    }

    public NavMeshAgent GetAgent{
        get{return enemy;}
    }


}
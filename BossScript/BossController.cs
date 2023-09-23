using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour {
    
    private BossState currState;
    private Animator animator;

    private NavMeshAgent boss;
    private float attackRange = 5f;
    [SerializeField]
    private Transform target;
    public GameObject hand1, hand2; 
    public ParticleSystem fire;

    private bool isDelay;

    public static BossController instance;

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
        boss = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeState(new IdleState());
        isDelay = false;
        StopParticleSystem();
    }

    public void ChangeState(BossState newState){
        if(currState != null){
            currState.ExitState(this);
        }
        currState = newState;
        currState.EnterState(this);
    }

    private void Update() {
        if(!RoomManager.instance.IsInBossRoom){
            ChangeState(new IdleState());
        }
        if(Boss.instance.Health<1){
            ChangeState(new DieState());
        }
        currState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            print("Hit");
        }
        if(other.gameObject.tag == "Bullet"){
            print("trigger");
            Boss.instance.TakeDamage();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Bullet"){
            print("collision");
            Boss.instance.TakeDamage();
            Destroy(other.gameObject);
        }
    }

    public void ActivateFire(){
        Invoke("ActivateParticleSystem", 0.5f);
    }

    public void ActivateParticleSystem(){
        fire.gameObject.SetActive(true);
        fire.Play();
        Invoke("StopParticleSystem", 5f);
    }

    public void StopParticleSystem(){
        fire.Stop();
        fire.gameObject.SetActive(false);
    }

    public NavMeshAgent GetBoss{
        get{return boss;}
    }

    public float AttackRange{
        get{return attackRange;}
    }

    public Transform Target{
        get{return target;}
    }

    public Animator GetAnimator{
        get{return animator;}
    }

    public NavMeshAgent GetAgent{
        get{return boss;}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walking,
        Look
    }

    private State state = State.Idle;
    private bool isIdle = true;
    private bool isWalking = false;
    private float timer = 0f;
    private NavMeshAgent agent;
    private Animator animator;
    private bool playerInRange = false;
    public Transform playerTransform;
    public GameObject lookObject;
    public Canvas interactHUD;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //interactHUD.enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartIdle();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    StartWalking();
                }
                break;
            case State.Walking:
                animator.SetBool("Idle", false);
                animator.SetBool("Walking", true);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    StartIdle();
                }
                break;
            case State.Look:
                if (playerInRange)
                {
                    Vector3 relativePos = playerTransform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    Quaternion curr = transform.localRotation;
                    transform.localRotation = Quaternion.Slerp(curr, rotation, Time.deltaTime * 5f);

                    // Vector3 direction = playerTransform.position - transform.position;
                    // Quaternion lookRotate = Quaternion.LookRotation(direction);
                    // transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotate, 5 * Time.deltaTime);
                    isIdle = true;
                    isWalking = false;
                    animator.SetBool("Idle", true);
                    animator.SetBool("Walking", false);
                }
                break;
        }
        if(playerInRange){
            if(Input.GetKeyDown(KeyCode.C)){
                
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerInRange = true;
            //interactHUD.enabled = true;
            state = State.Look;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerInRange = false;
            StartIdle();
            //interactHUD.enabled = false;
        }
    }
    void StartIdle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Walking", false);
        isIdle = true;
        isWalking = false;
        state = State.Idle;
        timer = Random.Range(20f, 60f);
    }

    void StartWalking()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", true);
        isIdle = false;
        isWalking = true;
        state = State.Walking;
        agent.SetDestination(GetRandomDestination());
    }

    Vector3 GetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }

    void StartLooking()
    {
        state = State.Look;
    }
    void StartTalking()
    {
        print("Hello");
    }

}

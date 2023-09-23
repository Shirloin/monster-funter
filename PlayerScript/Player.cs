using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float walkSpeed, walkBackSpeed, oldSpeed, sprintSpeed, rotateSpeed;
    public bool walking;
    public Transform playerTrans;
    public GameObject footStepSound;

    // Start is called before the first frame update
    void Start()
    {
        footStepSound.SetActive(false);
        playerAnim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * walkBackSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //float VerticalAxis = Input.GetAxis("Vertical");
        //float HorizontalAxis = Input.GetAxis("Horizontal");
        //Vector3 movement = VerticalAxis * transform.forward + HorizontalAxis * transform.right;
        //transform.position += movement * 0.03f;

        //playerAnim.SetFloat("Vertical", VerticalAxis);
        //playerAnim.SetFloat("Horizontal", HorizontalAxis);

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;
            footStep();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
            stopFootStep();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            footStep();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            stopFootStep();
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed = sprintSpeed;
                playerAnim.SetTrigger("Run");
                playerAnim.ResetTrigger("Walk");
                footStep();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = oldSpeed;
                playerAnim.ResetTrigger("Run");
                playerAnim.SetTrigger("Walk");
                stopFootStep();
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            playerAnim.SetTrigger("Attack");
            playerAnim.ResetTrigger("Idle");
            playerAnim.SetTrigger("Idle");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerAnim.SetTrigger("JumpStart");
            playerAnim.SetTrigger("InAir");
            playerAnim.SetTrigger("JumpLand");
            playerAnim.SetTrigger("Idle");
        }
    }
    void footStep()
    {
        footStepSound.SetActive(true);
    }
    void stopFootStep()
    {
        footStepSound.SetActive(false);
    }

    void OnLand()
    {
        playerAnim.ResetTrigger("JumpStart");
        playerAnim.ResetTrigger("InAir");
        playerAnim.ResetTrigger("JumpLand");
        playerAnim.SetTrigger("Idle");
    }
}

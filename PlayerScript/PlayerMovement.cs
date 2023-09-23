using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnim;
    public Animator[] animations;
    public CharacterController controller;

    public Transform cam;
    private float walkSpeed = 5f;
    private float speed = 5f;
    private float sprintSpeed = 12f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float gravity;
    private float gravityMultiplier = 3.0f;
    private float velocity;
    private float jumpHeight = 2f;
    private Vector3 jumpVelocity;
    private Vector3 moveDir;

    private float dodgeTimer;
    private bool isWalking;
    private bool isRunning;
    private float horizontal;
    private float vertical;

    private float x;
    private float y;
    private float animatorSpeed;

    public LayerMask groundMask;

    private FlyScript fly;
    private WizardAttack wizardAttack;
    private PaladinAttack paladinAttack;

    public GameObject wizard;
    public GameObject paladin;
    private BarController bc;


    private int i;


    // Start is called before the first frame update
    void Start()
    {
        gravity = -10f;
        x = 1;
        y = 1;
        i = 0;
        i = PlayerPrefs.GetInt("selectedCharacter");
        if(i==0){
            wizard.SetActive(true);
            paladin.SetActive(false);
        }
        else{
            paladin.SetActive(true);
            wizard.SetActive(false);
        }
        isRunning = false;
        playerAnim = animations[i];
        playerAnim = GetComponentInChildren<Animator>();
        bc = GetComponent<BarController>();
        fly = GetComponent<FlyScript>();
        wizardAttack = GetComponent<WizardAttack>();
        paladinAttack = GetComponent<PaladinAttack>();
        groundMask = LayerMask.GetMask("Ground");
        jumpHeight = 1.5f;
        animatorSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && wizardAttack.IsAiming == false)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = velocity;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if(!wizardAttack.IsAiming){
            playerAnim.SetFloat("Vertical", vertical * x);
            playerAnim.SetFloat("Horizontal", horizontal * y);
        }
        playerAnim.speed = animatorSpeed;
        if(Input.GetKeyDown(KeyCode.P)){
            bc.ReduceHealth();
        }
        ApplyWalk();
        ApplyGravity();
        ApplyJump();
        ApplyDodge();
        if(i==0){
            wizardAttack.FireAttack();
            wizardAttack.BasicAttack();
            fly.ApplyFly();
        }
        else{
            paladinAttack.BasicAttack();
            paladinAttack.RollSkill();
        }
    }

    void ApplyWalk()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            if (Input.GetKey(KeyCode.LeftShift) && GameManager.instance.Stamina >= 0.0f)
            {
                x = 2;
                y = 2;
                isRunning = true;
                isWalking = false;
                speed = sprintSpeed;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                x = 1;
                y = 1;
                isRunning = false;
                isWalking = false;
                speed = walkSpeed;
            }
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        { 
            isWalking = false;
            isRunning = false;
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    void ApplyJump()
    {
        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            playerAnim.SetTrigger("Jump");
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
        jumpVelocity.y += gravity * Time.deltaTime;
        controller.Move(jumpVelocity * Time.deltaTime);
    }

    void ApplyDodge(){
        if(Input.GetKeyDown(KeyCode.V)){
            dodgeTimer = 1f;
            playerAnim.SetBool("Dodge", true);
        }
        if(dodgeTimer > 0 && playerAnim.GetBool("Dodge")){
            moveDir = -transform.forward * 3f;

            controller.Move(moveDir.normalized* Time.deltaTime);
            dodgeTimer -= Time.deltaTime;
        }
        else{
            playerAnim.SetBool("Dodge", false);
        }
    }
    public bool isPlayerRunning()
    {
        return isRunning;
    }

    public float Gravity{
        get{return gravity;}
        set{
            gravity = value;
        }
    }

    public void IncreaseSpeed(){
        walkSpeed = sprintSpeed = speed += 2f;
        animatorSpeed += 0.2f;
    }

    public void SpeedCheat(){
        walkSpeed = sprintSpeed = speed = 20;
        animatorSpeed = 2f;
    }

    public void ChangeCharacterCheat(){
        if(i==0){
            playerAnim = animations[1];
            i = 1;
            PlayerPrefs.SetInt("selectedCharacter", 1);
            paladin.SetActive(true);
            wizard.SetActive(false);
        }
        else if(i==1){
            playerAnim = animations[0];
            i = 0;
            PlayerPrefs.SetInt("selectedCharacter", 0);
            paladin.SetActive(false);
            wizard.SetActive(true);
        }
    }
}

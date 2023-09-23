using UnityEngine;

public class FlyScript : MonoBehaviour {
    
    private PlayerMovement playerMovement;
    private bool isFlying = false;
    private float flyDuration = 5f;
    private float flySpeed = 20f;
    private float flyHeight = 50f;
    private float cooldownTime = 10f;
    private bool isCooldown = false;
    private float flyTimer = 0f;
    private float cooldownTimer = 0f;
    private bool mission;

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        mission = false;
    }

    public void ApplyFly()
    {
        ApplyCooldown();
        if (Input.GetKeyDown(KeyCode.R) && !isFlying && cooldownTimer <= 0f)
        {
            mission = true;
            playerMovement.playerAnim.SetBool("Flying", true);
            isFlying = true;
            flyTimer = flyDuration;
            playerMovement.Gravity = 0;
        }
        if (isFlying)
        {
            float moveSpeed = flySpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveSpeed + Vector3.up * flyHeight * Time.deltaTime);
            
            float rotationX = Input.GetAxis("Mouse X") * Time.deltaTime * flySpeed * 2f;
            float rotationY = Input.GetAxis("Mouse Y") * Time.deltaTime * flySpeed * 2f;
            transform.Rotate(new Vector3(-rotationY, rotationX, 0));

            flyTimer -= Time.deltaTime;
            RaycastHit hit;
            if (flyTimer <= 0f || Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f, playerMovement.groundMask)) 
            {
                StopFlying();
            }
            else{
                Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

                if(direction.magnitude >= 0.1f){
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerMovement.cam.eulerAngles.y;
                    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                }
            }
        }
    }

    private void ApplyCooldown()
    {
        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void StopFlying()
    {
        playerMovement.playerAnim.SetBool("Flying", false);
        playerMovement.playerAnim.SetFloat("Vertical", 0);
        playerMovement.playerAnim.SetFloat("Horizontal", 0);
        isFlying = false;
        playerMovement.Gravity = -10f;
        cooldownTimer = cooldownTime;
    }

    public bool IsFlying{
        get{return isFlying;}
    }

    public void FlyCheat(){
        this.flyDuration = 10;
        this.flySpeed = 25;
    }

    public bool GetMission{
        get{return mission;}
    }
}
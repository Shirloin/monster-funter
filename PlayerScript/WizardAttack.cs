using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class WizardAttack:MonoBehaviour{
    private PlayerMovement playerMovement;
    // public GameObject fire;
    public ParticleSystem fire;

    public GameObject crosshair;

    public GameObject player;
    public CinemachineFreeLook fCam;
    public ParticleSystem projectilePrefab;

    public Transform hand;
    private bool isAiming = false;
    public Rig rig;
    public Transform canvas;
    public Transform rigTarget;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    private float skillTimer;
    private bool isSkillActivated;
    private float skillCooldown;
    private int attackCount;

    private bool mission, skillmission;

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        rig.weight =0f;
        fCam.m_Lens.FieldOfView = 45f;
        crosshair.SetActive(false);
        attackCount = 0;
        mission = false;
        skillmission = false;  
        fire.gameObject.SetActive(false);
    }

    public void FireAttack()
    {
        ApplyCooldown();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(Mission.instance.X==2 && skillmission == false){
                skillmission = true;
            }
            skillTimer = 0.5f;
            isSkillActivated = true;
            playerMovement.playerAnim.SetTrigger("Skill");
            
            Invoke("ActivateParticleSystem", 1f);
        }
        if(isSkillActivated){
            skillTimer -= Time.deltaTime;
            if(skillTimer <= 0f){
                isSkillActivated = false;
                skillCooldown = 5f;
            }
        }
    }

    private void ActivateParticleSystem(){
        fire.gameObject.SetActive(true);
        fire.Play();
        Invoke("StopParticleSystem", 1.3f);
    }

    private void StopParticleSystem(){
        fire.Stop();
        fire.gameObject.SetActive(false);
    }

    public void BasicAttack()
    {   if(attackCount==10){
            mission = true;
        }
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            crosshair.SetActive(true);
            fCam.m_Lens.FieldOfView = Mathf.Lerp(fCam.m_Lens.FieldOfView, 35f, 0.5f);
            SetRig();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            crosshair.SetActive(false);
            fCam.m_Lens.FieldOfView = Mathf.Lerp(fCam.m_Lens.FieldOfView, 45f, 0.5f);
        }

        if (isAiming)
        {
            rigTarget.position = canvas.position;
            Vector3 direction = crosshair.transform.position - transform.position;
            direction.y = 0;
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            player.transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 5f);
            if (Input.GetMouseButtonDown(0))
            {
                 var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 50;
                if(Mission.instance.X == 1 && mission == false){
                    attackCount++;
                }
                ParticleSystem projectile = Instantiate(projectilePrefab, hand.position, Quaternion.identity);
                Destroy(projectile.gameObject, 1f);

                isAiming = false;
                ResetRig();

                crosshair.SetActive(false);
                 fCam.m_Lens.FieldOfView = Mathf.Lerp(fCam.m_Lens.FieldOfView, 45f, 0.5f);
            }
        }
        else
        {
            ResetRig();
        }
    }

    void ApplyCooldown(){
        if(skillCooldown > 0f){
            skillCooldown -= Time.deltaTime;
        }
    }

    void ResetRig()
    {
        float x = 2f;
        if(rig.weight > 0)
        {
            rig.weight -= x * Time.deltaTime;
        }
    }

    void SetRig()
    {
        float x = 2f;
        if(rig.weight < 1)
        {
            rig.weight += x * Time.deltaTime;
        }
    }

    public bool IsSkillActivated{
        get{return isSkillActivated;}
    }
    public bool IsAiming{
        get{return isAiming;}
    }

    public bool GetMission{
        get{return mission;}
    }

    public bool SkillMission{
        get{return skillmission;}
    }
}
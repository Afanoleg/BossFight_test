using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;


public class Boss : MonoBehaviour
{
    public Transform playerTarget;
    public LookAtConstraint lookAtPlayer;


    //BossHealthPoints
    public int restoreShieldEveryHealthDamage = 100;
    public int brainHealth = 300;
    public int ragePhaseHealth = 100;
    public int shieldHealth = 100;

    //HealthBar
    public Slider hpShieldBar;
    public Slider hpBrainBar;

    //Animator
    public Animator controller;

    //BossCollider
    public BoxCollider bossCollider;

    public HitTarget[] brainTargets;
    public HitTarget[] bodyTargets;

    private int currentBrainHealth;
    private int currentShieldHealth;


    //Launcher Attack
    public GameObject grenade;
    public Transform grenadeLaunchPoint;
    public float grenadeLaunchRange = 100f;
    public float frontSpeedForce = 30f;
    public float upSpeedForce = 7f;

    //Hand Attack
    public BoxCollider hand;
    public int handAttackDamage = 15;
    public int radius = 3;

    public float minAtackDistance = 10f;
    public float maxAtackDistance = 100f;


    private void Awake()
    {
        lookAtPlayer.enabled = true;
        currentBrainHealth = brainHealth;
        currentShieldHealth = shieldHealth;        
    }

    private void Update()
    {   
        TryAttack();
        UpdateHPBar();
        UpdateAnimator();
    }

    public void AttackLauncher()
    {
        Rigidbody grenadeBody = Instantiate(grenade, grenadeLaunchPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        grenadeBody.transform.LookAt(playerTarget.position);
        grenadeBody.AddForce(transform.forward * frontSpeedForce, ForceMode.Impulse);
        grenadeBody.AddForce(transform.up * upSpeedForce, ForceMode.Force);
    }

    public void TryAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        
        Debug.Log(distance);

        if (distance > maxAtackDistance)
        {
            controller.SetBool("Attack_Shooting", false);
            controller.SetBool("Attack_Hand", false);
        }            
        else if (distance <= maxAtackDistance && distance >= minAtackDistance)
        {
            controller.SetBool("Attack_Shooting", true);
            controller.SetBool("Attack_Hand", false);
        }
        else if (distance < minAtackDistance)
        {
            controller.SetBool("Attack_Hand", true);
            controller.SetBool("Attack_Shooting", false);
        }
    }

    private void OnEnable()
    {
        foreach (var brainTarget in brainTargets)
        {
            brainTarget.OnHit += OnBrainHit;
        }
        foreach (var bodyTarget in bodyTargets)
        {
            bodyTarget.OnHit += OnBodyHit;
        }

        controller.GetBehaviour<ShieldsUp>().shieldsUp += OnShieldsUp;
    }

    public void OnShieldsUp()
    {
        currentShieldHealth = 100;
        if(currentShieldHealth <= 0)
        {
            controller.SetBool("WeakPhase", true);
        }
    }

    private void OnDisable()
    {
        foreach (var brainTarget in brainTargets)
        {
            brainTarget.OnHit -= OnBrainHit;
        }
        foreach (var bodyTarget in bodyTargets)
        {
            bodyTarget.OnHit -= OnBodyHit;
        }

        controller.GetBehaviour<ShieldsUp>().shieldsUp -= OnShieldsUp;
    }

    private void OnBodyHit(int damage)
    {
        var weakPhaseBehaviuor = controller.GetBehaviour<WeakPhase>();
        if (weakPhaseBehaviuor != null && weakPhaseBehaviuor.isInvulnerable)
            return;

        currentShieldHealth = Mathf.Max(0, currentShieldHealth - damage);     
    }

    private void OnBrainHit(int damage)
    {
        var weakPhaseBehaviuor = controller.GetBehaviour<WeakPhase>();
        if (weakPhaseBehaviuor != null && weakPhaseBehaviuor.isInvulnerable)
            return;

        currentShieldHealth = Mathf.Max(0, currentShieldHealth - damage);
        currentBrainHealth = Mathf.Max(0, currentBrainHealth - damage);

        if (currentShieldHealth > 0)
            return;

        if (currentBrainHealth % restoreShieldEveryHealthDamage == 0 && currentBrainHealth > 0 && currentBrainHealth < brainHealth)
        {
            currentShieldHealth = shieldHealth;
        }
    }

    private void UpdateHPBar()
    {
        hpBrainBar.value = currentBrainHealth;
        hpShieldBar.value = currentShieldHealth;
    }

    private void UpdateAnimator()
    {
        if (currentBrainHealth == 0)
        {
            controller.SetBool("Death", true);
        }
        if (currentShieldHealth == 0 && currentBrainHealth != 0) 
        {
            controller.SetBool("WeakPhase", true);
            lookAtPlayer.enabled = false;
        }
        if (currentShieldHealth == shieldHealth)
        {
            lookAtPlayer.enabled = true;
            controller.SetBool("WeakPhase", false);
            if (currentBrainHealth > ragePhaseHealth)
            {
                controller.SetBool("SecondPhase", false);
            }
            return;
        }
        if (currentBrainHealth <= ragePhaseHealth)
        {
            controller.SetBool("SecondPhase", true);

            if (controller.GetBool("WeakPhase") && !controller.GetBool("SecondPhase"))
            {
                controller.SetBool("WeakPhase", false);                
            }
            return;        
        }      
    }    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugScript : MonoBehaviour
{
    [Header("Effects")]
    public ParticleSystem damageVFX;
    public AudioClip damageSFX;
    public ParticleSystem dieVFX;
    public AudioClip dieSFX;
    public string property;

    [Header("Stats")]
    public Transform target;
    public int alertDistance = 70;
    public int looseDistance = 400;
    public int attackDistance = 40;
    public float attackRate = 1;

    [Header("Ignore")]
    [HideInInspector] public Health health;
    NavMeshAgent agent;
    float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        health.damageEvent.AddListener(Damage);
        health.deathEvent.AddListener(Kill);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerMechController.me.transform.position) < alertDistance)
            target = PlayerMechController.me.transform;
        if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) > looseDistance)
            target = null;
        if (target & !health.dead)
        {
            if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) > agent.stoppingDistance)
            {
                agent.enabled = true;
                agent.SetDestination(target.position);
            }
            else
                agent.enabled = false;
            if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) <= attackDistance & Time.time >= lastAttackTime + attackRate)
                Attack();
        }
        else
        {
            lastAttackTime = 0;
            agent.isStopped = true;
        }
    }

    public void Attack()
    {
        lastAttackTime = Time.time;
        PlayerMechController.me.Stun(true);
    }

    public void Damage()
    {
        FXManager.SpawnVFX(damageVFX, transform.position, transform.eulerAngles, null, 5);
        FXManager.SpawnSFX(damageSFX, transform.position, 15, 5);
    }

    public void Kill()
    {
        SpawnEffect effect = gameObject.AddComponent<SpawnEffect>();
        effect.spawnEffectTime = 2;
        effect.pause = 0;
        effect.fadeIn = GameManager.me.fadeIn;
        effect.useAlpha = true;
        effect.property = property;
        effect.fadeDelay = 0;
        FXManager.SpawnVFX(dieVFX, transform.position, transform.eulerAngles, null, 5);
        FXManager.SpawnSFX(dieSFX, transform.position, 15, 5);
        Destroy(gameObject, 2);
    }

    public void OnDestroy()
    {
        
    }
}

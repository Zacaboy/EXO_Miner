using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BugScript : MonoBehaviour
{
    [Header("Effects")]
    public ParticleSystem damageVFX;
    public AudioClip damageSFX;
    public ParticleSystem dieVFX;
    public AudioClip dieSFX;
    public Material fadeMat;
    public string property;

    [Header("Stats")]
    public int alertDistance = 70;
    public int looseDistance = 400;
    public int attackDistance = 40; // This is how close the target has to be for the AI to attack
    public int attackRange = 40; // This is the range of the attack
    public float attackRate = 1;
    public int attackDamage = 25;
    public float stunTime = 1;

    [Header("Animations")]
    public List<AnimationClip> attacks = new List<AnimationClip>();
    public float[] attackRaycasts;
    public List<AnimationClip> damages = new List<AnimationClip>();
    public float fadeDelay = 0.6f;

    [Header("Behaviour")]
    [Range(0f, 1f)]
    public float attackStunChance = 0.5f;
    public bool attackWhenClose = true;
    [Range(0f, 1f)]
    public float attackWhenCloseStunChance = 0.5f;

    [Header("Misc")]
    public LayerMask mask;
    public bool showDebug;

    [Header("Ignore")]
    [HideInInspector] public Health health;
    [HideInInspector] public Transform target;
    [HideInInspector] public Animator ani;
    [HideInInspector] public NavMeshAgent agent;
    Transform attackPos;
    Transform bloodPos;
    AnimationClip currrentDamage;
    AnimationClip currrentAttack;
    float spawnTime;
    bool close;
    float lastAttackingTime;
    float lastDamageTime;
    float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
        health.damageEvent.AddListener(Damage);
        health.deathEvent.AddListener(Die);
        if (GetComponent<SpawnEffect>())
            GetComponent<SpawnEffect>().newMat = fadeMat;
        spawnTime = Time.time;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "Attack Pos")
                attackPos = t;
            if (t.name == "Blood Pos")
                bloodPos = t;
        }
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) < alertDistance & !target)
            target = PlayerMechController.me.transform;
        if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) > looseDistance)
            target = null;
        int movement = 0;
        bool canMove = false;
        if (lastAttackingTime > 0)
        {
            lastAttackTime = Time.time;
            if (Time.time >= lastAttackingTime + currrentAttack.length)
            {
                lastAttackingTime = 0;
                currrentAttack = null;
            }
        }
        if (lastDamageTime > 0)
        {
            lastAttackTime = Time.time;
            if (Time.time >= lastDamageTime + currrentDamage.length)
            {
                lastDamageTime = 0;
                currrentDamage = null;
            }
        }
        if (lastAttackingTime == 0 & lastDamageTime == 0)
            canMove = true;
        if (target & !health.dead)
        {
            if (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance & canMove)
            {
                agent.enabled = true;
                close = false;
                if (Vector3.Distance(agent.destination, target.position) > 25)
                    agent.SetDestination(target.position);
            }
            else
            {
                if (lastAttackingTime == 0)
                    if (attackWhenClose & !close & Time.time >= PlayerMechController.me.lastStunTime + 1)
                    {
                        float stunChance = Random.Range(0.01f, 1);
                        Attack(attackWhenCloseStunChance >= stunChance);
                    }
                transform.LookAt(target.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                agent.enabled = false;
                close = true;
            }
            if (canMove)
                if (Vector3.Distance(transform.position, PlayerMechController.me.transform.position) <= attackDistance & Time.time >= lastAttackTime + attackRate)
                {
                    float stunChance = Random.Range(0.01f, 1);
                    Attack(attackStunChance >= stunChance);
                }
        }
        else
        {
            lastAttackTime = 0;
            agent.enabled = false;
            close = false;
        }
        if (agent.enabled & canMove & !health.dead)
            if (agent.velocity.sqrMagnitude > 1)
                movement = 1;
            ani.SetInteger("Movement", movement);
    }

    public void Attack(bool stun)
    {
        lastAttackTime = Time.time;
        lastAttackingTime = Time.time;
        AnimationClip clip = attacks[Random.Range(0, attacks.Count - 1)];
        currrentAttack = clip;
        ani.CrossFadeInFixedTime(clip.name, 0.1f);
        StartCoroutine(WaitAttack(attackRaycasts[attacks.IndexOf(clip)], stun));
    }

    public IEnumerator WaitAttack(float time, bool stun)
    {
        yield return new WaitForSeconds(time);
        RaycastHit hit;
        if (Physics.Raycast(attackPos.position, transform.TransformDirection(Vector3.forward), out hit, attackRange, mask))
        {
            if (showDebug)
                Debug.DrawRay(attackPos.position, attackPos.TransformDirection(Vector3.forward) * attackRange, Color.green, 1);
            if (hit.transform.GetComponent<PlayerMechController>())
            {
                hit.transform.GetComponent<PlayerMechController>().health.Damage(attackDamage, DamageType.Physical);
                if (stun)
                    hit.transform.GetComponent<PlayerMechController>().Stun(stunTime, true);
            }
        }
        else if (showDebug)
            Debug.DrawRay(attackPos.position, attackPos.TransformDirection(Vector3.forward) * attackRange, Color.red, 1);
    }

    public void Damage()
    {
        FXManager.SpawnVFX(damageVFX, bloodPos.position, bloodPos.localEulerAngles, null, 25);
        FXManager.SpawnSFX(damageSFX, bloodPos.position, 15, 5);
        AnimationClip clip = damages[Random.Range(0, damages.Count - 1)];
        currrentDamage = clip;
        ani.CrossFadeInFixedTime(clip.name, 0.1f);
        lastDamageTime = Time.time;
        if (!target)
            target = PlayerMechController.me.transform;
    }

    public void Die()
    {
        SpawnEffect effect = gameObject.AddComponent<SpawnEffect>();
        effect.spawnEffectTime = 2;
        effect.pause = 0;
        effect.fadeIn = GameManager.me.fadeIn;
        effect.useAlpha = true;
        effect.property = property;
        effect.fadeDelay = fadeDelay;
        effect.newMat = fadeMat;
        FXManager.SpawnVFX(dieVFX, transform.position, transform.eulerAngles, null, 5);
        FXManager.SpawnSFX(dieSFX, transform.position, 15, 5);
        ani.CrossFadeInFixedTime("Die", 0.1f);
        Destroy(gameObject, 6);
    }

    public void OnDestroy()
    {
        
    }
}

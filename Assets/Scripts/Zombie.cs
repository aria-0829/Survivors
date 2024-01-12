using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Zombie : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] Animator animator;
    private bool isWalking = false;
    private int IsWalking = Animator.StringToHash("isWalking");

    [SerializeField] GameObject player;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] ParticleSystem particle;

    [SerializeField, Range(1, 5)]
    private float maxSpeed = 2f;
    [SerializeField] float moveSpeed = 0f;

    private float coolDown = 3f;
    private float lastAttackTime = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = GetComponentInChildren<HealthSystem>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        moveSpeed = Random.Range(0.3f, maxSpeed);
        Invoke("StartWalking", 2);

        if (healthSystem != null)
        {
            healthSystem.OnDamage += HealthSystem_OnDamage;
            healthSystem.OnDeath += HealthSystem_OnDeath;
        }
    }

    void Update()
    {
        if (player == null || healthSystem.IsDead())
        {
            return;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            isWalking = false;
            agent.SetDestination(transform.position);

            animator.SetTrigger("attack");

            if (Time.time - lastAttackTime > coolDown)
            {
                lastAttackTime = Time.time;
                player.GetComponentInChildren<HealthSystem>().TakeDamage(10f);
            }
        }
        else
        {
            isWalking = true;
            agent.SetDestination(player.transform.position);
        }

        animator.SetBool(IsWalking, isWalking);
    }

    private void StartWalking()
    {
        isWalking = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Bullet>(out Bullet bullet))
        {
            healthSystem?.TakeDamage(bullet.GetDamage());
        }
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger("die");
        healthSystem.OnDamage -= HealthSystem_OnDamage;
        Invoke("Destroy", 2f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        if (healthSystem.IsDead())
        {
            return;
        }
        else
        {
            animator.SetTrigger("hit");
            particle.Play();
        }
    }
}

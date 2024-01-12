using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Vampire : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject player;

    [SerializeField] private HealthSystem healthSystem;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = GetComponentInChildren<HealthSystem>();
    }

    void Start()
    {
        Invoke("StartWalking", 3);

        if (healthSystem != null)
        {
            healthSystem.OnDamage += HealthSystem_OnDamage;
            healthSystem.OnDeath += HealthSystem_OnDeath;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            animator.SetTrigger("attack");
            player.GetComponentInChildren<HealthSystem>().TakeDamage(5f);
        }
        else
        {
            animator.SetTrigger("attack");
        }
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
        Invoke("Destroy", 3);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        animator.SetTrigger("hit");
    }
}

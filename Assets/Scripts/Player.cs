using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform weapon;
    [SerializeField] Rigidbody bullet;
    [SerializeField] ParticleSystem particle;

    private bool isWalking = false;
    private bool isRunning = false;
    private int IsWalking = Animator.StringToHash("isWalking");
    private int IsRunning = Animator.StringToHash("isRunning");
    private int Attack = Animator.StringToHash("attack");

    private Vector3 direction;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float turnSpeed = 20f;

    public HealthSystem healthSystem;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponentInChildren<HealthSystem>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDamage += HealthSystem_OnDamage;
            healthSystem.OnHeal += HealthSystem_OnHeal;
            healthSystem.OnDeath += HealthSystem_OnDeath;
        }
    }

    void Update()
    {
        GetInput();
        Animate();
        
        transform.position += moveSpeed * Time.deltaTime * direction;
        transform.forward = Vector3.Slerp(transform.forward, direction, turnSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //Attacking
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(Attack);

            //Rigidbody p = Instantiate(bullet, weapon.position, transform.rotation);
            //p.velocity = transform.forward * 5f;

            Vector3 targetDirection = (MouseUtil.Instance.GetMousePosition() - transform.position).normalized;
            //float bulletHeight = Random.Range(0.5f, 1.8f);
            //Vector3 bulletPosition = transform.position + targetDirection + Vector3.up;
            Rigidbody p = Instantiate(bullet, weapon.position, Quaternion.identity);
            p.velocity = targetDirection * 10f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger(Attack);
        }
    }

    private void GetInput()
    {
        Vector2 input = new Vector2(0, 0);

        //Walking
        if (Input.GetKey(KeyCode.W))
        {
            isWalking = true;
            input.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isWalking = true;
            input.y = -1;
        }

        //Turning
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
            input.x = -1;
            transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            input.x = +1;
            transform.Rotate(new Vector3(0, +turnSpeed, 0) * Time.deltaTime);
        }

        //KeyUp
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isWalking = false;
        }

        input = input.normalized;

        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            moveSpeed /= 2;
        }

        

        direction = new Vector3(input.x, 0f, input.y);
    }

    private void Animate()
    {
        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsRunning, isRunning);
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger("die");
        healthSystem.OnDamage -= HealthSystem_OnDamage;
        Invoke("Destroy", 3);
        GameManager.Instance.GameOver();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        animator.SetTrigger("hit");
        particle.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAround : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed = 0.8f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isWalking", true);

        // randomize the moving direction
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        // move the object
        transform.position += walkSpeed * transform.forward * Time.deltaTime;
        
        // if hit a wall, move to the opposite direction
        if (Physics.Raycast(transform.position, transform.forward, 1))
        {
            transform.Rotate(0, 180, 0);
        }
    }
}

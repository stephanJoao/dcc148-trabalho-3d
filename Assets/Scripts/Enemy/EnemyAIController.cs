using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    private bool chase = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(chase)
            agent.destination = player.transform.position;

        if(Vector3.Distance(player.transform.position, transform.position) <= 0.2f)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            chase = true;
            animator.SetBool("Chase", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            chase = false;
            animator.SetBool("Chase", false);
        }
    }
}

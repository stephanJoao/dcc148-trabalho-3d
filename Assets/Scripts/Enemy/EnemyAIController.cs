using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour, IKillable
{

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    private bool chase = false;
    private bool dead = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!dead)
        {
            if (chase)
                agent.destination = player.transform.position;

            if (Vector3.Distance(player.transform.position, transform.position) <= 2f)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
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

    public void Damage()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        dead = true;
        agent.isStopped = true;
        Destroy(gameObject, 5f);

    }
}

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
    public bool dead = false;

    [Header("SFX")]
    [Space(10)]
    [SerializeField] List<AudioClip> footstepSounds;
    [SerializeField] List<AudioClip> hurtSounds;
    [SerializeField] List<AudioClip> idleSounds;
    [SerializeField] List<AudioClip> attackSounds;

    private AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private void OnTriggerStay(Collider other)
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

    public void PlayFootstepSound()
    {
        SoundManager.PlayAudioClip(SoundManager.GetRandomSound(footstepSounds), audioSource);
    }
    public void PlayHurtSound()
    {
        SoundManager.PlayAudioClip(SoundManager.GetRandomSound(hurtSounds), audioSource);
    }
    public void PlayIdleSound()
    {
        SoundManager.PlayAudioClip(SoundManager.GetRandomSound(idleSounds), audioSource);
    }
    public void PlayAttackSound()
    {
        SoundManager.PlayAudioClip(SoundManager.GetRandomSound(attackSounds), audioSource);
    }

    public void Death()
    {
        dead = true;
        agent.isStopped = true;
        PlayHurtSound();
        Destroy(gameObject, 5f);

    }
}

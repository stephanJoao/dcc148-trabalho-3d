using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IKillable
{
    private float moveX;
    private float moveY;
    private float speed = 5f;
    private float mouseSensitivity = 500f;
    private bool dead = false;

    [SerializeField] GameObject bullet; 
    [SerializeField] ParticleSystem muzzleFlash;

    private Animator animator;

    [SerializeField] Transform bulletSpawn;

    [Header("Pool Config")]
    [Space(5)]
    [SerializeField] List<GameObject> bulletPool;
    private readonly int poolSize = 10;

    public List<AudioClip> shootSounds;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        for (int i = 0; i <= poolSize; i++)
        {
            var b = Instantiate(bullet);
            b.SetActive(false);
            bulletPool.Add(b);
        }

    }

    void Update()
    {
        if (!dead)
        {

            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            animator.SetFloat("posY", moveY);
            animator.SetFloat("posX", moveX);


            transform.Translate(moveX * Time.deltaTime * speed, 0, moveY * Time.deltaTime * speed);

            transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
            Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, 0, 0);

            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                animator.Play("Shoot2");
                Shoot();
            }
            else if (Input.GetMouseButton(1))
            {

                if (animator.GetBool("aim") != true)
                {
                    animator.SetBool("aim", true);
                }


            }
            else if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("shoot");
            }
            else
            {
                animator.SetBool("aim", false);
            }
        }
    }

    public void PlayShootSound()
    {
        SoundManager.PlayAudioClip(SoundManager.GetRandomSound(shootSounds), audioSource);
    }
    private void Shoot()
    {
        foreach (GameObject b in bulletPool)
        {
            if (!b.activeSelf)
            {
                b.SetActive(true);
                muzzleFlash.Play();
                PlayShootSound();
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GetComponentInChildren<RagdollHandler>().SetRagdollState(true);
            Death();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Death()
    {
        dead = true;
        Invoke(nameof(RestartGame), 2f);
        
    }

}

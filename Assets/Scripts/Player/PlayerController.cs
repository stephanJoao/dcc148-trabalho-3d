using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private float speed = 5f;
    private float mouseSensitivity = 500f;

    [SerializeField] GameObject bullet;

    private Animator animator;

    [SerializeField] Transform bulletSpawn;
    [Header("aim layer Collision")]
    [Space(5)]
    public LayerMask layerMask;
    public float rayRange;


    [Header("Pool Config")]
    [Space(5)]
    [SerializeField] List<GameObject> bulletPool;
    private readonly int poolSize = 10;

	public AudioSource audioShoot;
	public AudioSource audioAmbience;

    void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i <= poolSize; i++)
        {
            var b = Instantiate(bullet);
            b.SetActive(false);
            bulletPool.Add(b);
        }
		audioAmbience.Play();		

    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        animator.SetFloat("posY", moveY);
        animator.SetFloat("posX", moveX);


        transform.Translate(moveX * Time.deltaTime * speed, 0, moveY * Time.deltaTime * speed);

        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, 0, 0);

        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            animator.Play("Shoot2");
            Shoot();
        }
        else if(Input.GetMouseButton(1))
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

    private void Shoot()
    {
        foreach (GameObject b in bulletPool)
        {
            if (!b.activeSelf)
            {
                b.SetActive(true);
                audioShoot.Play();
                break;
            }
        }
    }

}

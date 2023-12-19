using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private float speed = 5f;
    private float mouseSensitivity = 150f;
    private float rotationSpeed = 5f;

    private Animator animator;

    [Header("aim layer Collision")]
    [Space(5)]
    public LayerMask layerMask;
    public float rayRange;


    [Header("Pool Config")]
    [Space(5)]
    [SerializeField] List<GameObject> bulletPool;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    private readonly int poolSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i <= poolSize; i++)
        {
            Instantiate(bullet);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    // Update is called once per frame
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
        }
        else if(Input.GetMouseButton(1))
        {

            if (animator.GetBool("aim") != true)
            {
                animator.SetBool("aim", true);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, rayRange, layerMask))
            {
                Vector3 lookDirection = hit.point - transform.position;

                if (lookDirection != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(lookDirection);

                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            }


        }
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("shoot");
            Shoot();
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
                b.transform.position = bulletSpawn.position;
                b.SetActive(true);
                break;
            }
        }
    }
}

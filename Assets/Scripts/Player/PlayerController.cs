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

    public LayerMask layerMask;

    private Animator animator;

    [SerializeField] GameObject[] bulletPool;
    [SerializeField] Transform bulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
  
        animator.SetFloat("posY", moveY);
        animator.SetFloat("posX", moveX);

   
        transform.Translate(0, 0, moveY * Time.deltaTime * speed);
        transform.Translate(moveX * Time.deltaTime * speed, 0, 0);

        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, 0, 0);
        

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("shoot2", false);

            animator.SetBool("aim", true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f, layerMask))
            {
                Vector3 lookDirection = hit.point - transform.position;

                if (lookDirection != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(lookDirection);

                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("shoot2", true);
            }
        }
        else if(Input.GetMouseButtonDown(0))
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
        foreach(GameObject b in bulletPool)
        {
            if (!b.activeSelf)
            { 
                Instantiate(b);
                b.transform.position = bulletSpawn.position;
                b.SetActive(true);
                break;
            }
        }
    }
}

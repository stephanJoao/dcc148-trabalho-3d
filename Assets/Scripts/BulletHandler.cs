using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;

    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.Translate(bulletSpeed * Time.deltaTime * transform.forward);
        }
    }
}

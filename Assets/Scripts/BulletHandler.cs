using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.Translate(transform.forward * bulletSpeed);
        }
    }
}

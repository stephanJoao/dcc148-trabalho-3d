using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] Transform bulletSpawn;
    [SerializeField] Transform player;

    [SerializeField] float bulletSpeed = 5f;

    private Rigidbody bulletRb;

    private bool bulletActive = false;


    private void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bulletSpawn = GameObject.FindGameObjectWithTag("BulletSpawn").GetComponent<Transform>();



    }
    void Update()
    {

        if(gameObject.activeSelf)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
            if (!bulletActive)
            {
                transform.rotation = player.transform.rotation;
                transform.position = bulletSpawn.transform.position;
                Invoke(nameof(SetInactive), 5f);
            }
            bulletActive = true;
        }
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
        bulletActive = false;
    }
}

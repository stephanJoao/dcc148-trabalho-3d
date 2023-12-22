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

    public Vector3 offset;

    [SerializeField] ParticleSystem bulletParticle;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bulletSpawn = GameObject.FindGameObjectWithTag("BulletSpawn").GetComponent<Transform>();
        bulletParticle = GameObject.FindGameObjectWithTag("BulletParticle").GetComponent<ParticleSystem>();



    }
    void Update()
    {

        if (gameObject.activeSelf)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
            if (!bulletActive)
            {
                transform.rotation = player.transform.rotation;
                transform.position = bulletSpawn.transform.position + offset;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            bulletParticle.transform.position = transform.position;
            bulletParticle.Play();
            SetInactive();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<RagdollHandler>().SetRagdollState(true);
            collision.rigidbody.AddForce(bulletRb.velocity * bulletSpeed, ForceMode.Impulse);
            collision.gameObject.GetComponentInParent<IKillable>().Death();
            SetInactive();
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;

    private bool bulletActive = false;
    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.Translate(bulletSpeed * Time.deltaTime * transform.forward);
            if(!bulletActive)
                Invoke(nameof(SetInactive), 5f);
            bulletActive = true;
        }
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
        bulletActive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // if W or S is pressed move forward or backward and change animation to walk
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            // change animation to walk
            GetComponent<Animator>().SetBool("Foward Running", true);
            
        }   
        else
        {
            // change animation to idle
            GetComponent<Animator>().SetBool("Foward Running", false);
        }

        // player is a capsule make it move forward and backward with W and S and the axis of the capsule is the z axis
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 20);
        // player is a capsule make it move left and right with A and D and the axis of the capsule is the x axis
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 20, 0, 0);
        // make capsule rotate with mouse movement
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100, 0);
        // make camera tilt with mouse movement
        Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * 100, 0, 0);

    }
}

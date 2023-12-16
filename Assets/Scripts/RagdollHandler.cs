using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Animator _animator;
    public Rigidbody[] rigidbodies;
    public Collider[] colliders;

    bool ragdollState = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        SetRagdollState(ragdollState);
    }

    private void SetRagdollState(bool state)
    {
        _animator.enabled = !state;

        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = !state;
        }

        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetRagdollState(!ragdollState);
            ragdollState = !ragdollState;
        }
    }
}

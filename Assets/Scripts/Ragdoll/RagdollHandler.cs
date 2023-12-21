using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    bool ragdollState = false;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }
    void Start()
    {
        _animator = GetComponentInParent<Animator>();

        SetRagdollState(ragdollState);
    }

    public void SetRagdollState(bool state)
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

        ragdollState = !ragdollState;
    }

}

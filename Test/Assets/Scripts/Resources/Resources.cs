using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Resources : MonoBehaviour
{
    [SerializeField] protected float MaxVelocity;
    protected Rigidbody Rigidbody;

    abstract public void TransitResource(Transform target);
}

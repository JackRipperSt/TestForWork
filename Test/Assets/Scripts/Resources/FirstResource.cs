using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstResource : Resources
{

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(Rigidbody.velocity.y >= MaxVelocity)
        {
            Rigidbody.velocity = Vector3.zero;

        }
    }

    public override void TransitResource(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joistick;
    [SerializeField] private float _moveSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(_joistick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joistick.Vertical * _moveSpeed) * Time.fixedDeltaTime, ForceMode.Impulse) ;

        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _moveSpeed);
        if (_joistick.Horizontal != 0 || _joistick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity * Time.deltaTime);
        }
    }
}

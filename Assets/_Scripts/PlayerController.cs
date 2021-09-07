using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;

    [SerializeField]
    private float turnSpeed;

    private Quaternion rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal , 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("IsWalking", isWalking);
        Vector3 desiredForeward = Vector3.RotateTowards(transform.forward, movement, Time.fixedDeltaTime * turnSpeed, 0f);
         rotation = Quaternion.LookRotation(desiredForeward);     
    }

    private void OnAnimatorMove()
    {
        //Spacio = Spacio inicial + Velocidad * tiempo
        // S = S0 + V * T
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}

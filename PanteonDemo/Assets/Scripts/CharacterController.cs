using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Animator _animator;
    private bool _dragging;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponentInChildren<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _dragging = true;
        else if (Input.GetMouseButtonUp(0))
            _dragging = false;

        if (_dragging)
            _rigidBody.velocity = Vector3.forward * 4f;

        if (_rigidBody.velocity.magnitude > 3f)
        {
            _animator.SetBool("isRunning", true);
            Debug.Log("true");
        }
        else
        {
            _animator.SetBool("isRunning", false);
            Debug.Log("false");
        }
    }
}

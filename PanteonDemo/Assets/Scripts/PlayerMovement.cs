using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private float RotateSpeed;

    private Rigidbody _rigidBody;
    private Animator _animator;
    private bool _dragging;

    void Start()
    {
        _rigidBody = gameObject.GetComponentInChildren<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _dragging = true;
        else if (Input.GetMouseButtonUp(0))
            _dragging = false;

        if (_dragging)
        {
            _rigidBody.velocity = transform.forward * MovementSpeed;
            _rigidBody.angularVelocity = Vector3.zero;
            Rotate();
            _animator.SetBool("isRunning", true);
            Debug.Log("true");
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
            _animator.SetBool("isRunning", false);
            Debug.Log("false");
        }
    }

    private void Rotate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);

            if (Vector3.Distance(targetPoint, transform.position) > 1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
            }
        }
    }
}

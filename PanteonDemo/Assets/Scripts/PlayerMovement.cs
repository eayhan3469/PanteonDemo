using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private float RotateSpeed;

    [SerializeField]
    private float ImpactForce;

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
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
            _animator.SetBool("isRunning", false);
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
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MobileObstacle")
            SceneManager.LoadScene("MainScene");

        if (collision.gameObject.tag == "RotatingPlatform")
        {
            _rigidBody.AddForce(Vector3.left * 10f, ForceMode.Impulse);
        }
    }
}

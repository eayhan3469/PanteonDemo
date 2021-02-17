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

    [HideInInspector]
    public bool IsRunOver;

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
        if (!IsRunOver)
        {
            if (Input.GetMouseButtonDown(0))
                _dragging = true;
            else if (Input.GetMouseButtonUp(0))
                _dragging = false;

            if (GameManager.Instance.HasGameStart)
                Move();
        }
    }

    private void Move()
    {
        if (_dragging)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, MovementSpeed * Time.deltaTime);
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

    private void Stop()
    {
        _rigidBody.velocity = Vector3.zero;
        _animator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MobileObstacle" || collision.gameObject.tag == "DropZone")
            SceneManager.LoadScene("MainScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
//            other.gameObject.GetComponent<BoxCollider>().Raycast.ig = false;
            other.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            UIController.Instance.PercentageText.gameObject.SetActive(true);
            IsRunOver = true;
            Stop();
        }
    }
}

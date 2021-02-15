using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Vector3[] WayPoints;

    private int _index = 0;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
    }

    private void GoToNextPoint()
    {
        if (WayPoints.Length == 0)
        {
            Debug.LogError("There is no waypoint");
            return;
        }

        _agent.destination = WayPoints[_index];
        Debug.Log(_agent.destination);

        if (_index < WayPoints.Length - 1)
            _index++;
    }

    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 4f)
            GoToNextPoint();

        if (_rigidbody.velocity.magnitude > 0f)
            _animator.SetBool("isRunning", true);
        else
            _animator.SetBool("isRunning", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
            _agent.isStopped = true;
    }

    void OnDrawGizmosSelected()
    {
        foreach (var pos in WayPoints)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pos, 1);
        }
    }
}

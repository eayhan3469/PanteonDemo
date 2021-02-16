using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3[] WayPoints;

    [SerializeField]
    private Vector3 LeftEdgePoint;

    [SerializeField]
    private Vector3 RightEdgePoint;

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

        RandomizeWayPoints();
    }

    private void RandomizeWayPoints()
    {
        for (int i = 0; i < WayPoints.Length; i++)
        {
            var randomX = Random.Range(LeftEdgePoint.x, RightEdgePoint.x);
            WayPoints[i] = new Vector3(randomX, WayPoints[i].y, WayPoints[i].z);
        }
    }

    private void SetNextDestination()
    {
        if (WayPoints.Length == 0)
            Debug.LogError("There is no waypoint");

        _agent.SetDestination(WayPoints[_index]);

        if (_index < WayPoints.Length - 1)
            _index++;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.HasGameStart)
        {
            if (_agent.remainingDistance < 10f)
                SetNextDestination();

            if (_rigidbody.velocity.magnitude > 0f)
                _animator.SetBool("isRunning", true);
            else
                _animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
            _agent.isStopped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MobileObstacle" || collision.gameObject.tag == "DropZone")
            GameManager.Instance.Respawn(gameObject);
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

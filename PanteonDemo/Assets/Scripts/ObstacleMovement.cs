using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private bool IsHorizontal;

    [SerializeField]
    [Tooltip("Give the positive or negative value for initial movement direction.")]
    private float MovementDistance;

    private Vector3 _target;
    private Vector3 _firstPosition;
    private Vector3 _lastPosition;

    private void Start()
    {
        _firstPosition = transform.position;

        if (IsHorizontal)
            _target = new Vector3(transform.position.x + MovementDistance, transform.position.y, transform.position.z);
        else
            _target = new Vector3(transform.position.x, transform.position.y, transform.position.z + MovementDistance);

        _lastPosition = _target;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * Speed);
        }
        else // Change Target
        {
            if (Vector3.Distance(_target, _firstPosition) < 0.2f)
                _target = _lastPosition;
            else
                _target = _firstPosition;
        }
    }
}

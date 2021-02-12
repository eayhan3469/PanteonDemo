using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform TargetTransform;

    [SerializeField]
    private float SmoothFactor;

    private Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = transform.position - TargetTransform.position;
    }

    private void LateUpdate()
    {
        if (TargetTransform == null)
        {
            TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Vector3 newPosition = TargetTransform.position + _cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPosition, SmoothFactor);
        }
    }
}

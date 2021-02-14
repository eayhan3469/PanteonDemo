using Es.InkPainter.Sample;
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
    private PlayerMovement _playerMovement;
    private GameObject _wall;

    private void Start()
    {
        _cameraOffset = transform.position - TargetTransform.position;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _wall = GameObject.Find("Wall");
    }

    private void LateUpdate()
    {
        if (!_playerMovement.IsRunOver)
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
        else
        {
            Vector3 cameraWallPosition = new Vector3(_wall.transform.position.x, transform.position.y, transform.position.z);
            Vector3 direction = _wall.transform.position - transform.position;
            direction.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), (Time.deltaTime) * 50f);
            transform.position = Vector3.MoveTowards(transform.position, cameraWallPosition, 7.5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, cameraWallPosition) < 0.1f && !gameObject.GetComponent<MousePainter>().enabled)
            {
                gameObject.GetComponent<MousePainter>().enabled = true;
            }

        }
    }
}

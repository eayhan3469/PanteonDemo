using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _dragging;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _dragging = true;
        else if (Input.GetMouseButtonUp(0))
            _dragging = false;

        if (_dragging)
        {
            _rigidBody.AddForce(Vector3.forward, ForceMode.Acceleration);
        }
    }
}

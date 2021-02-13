using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    [SerializeField]
    private float RotationSpeed;

    [HideInInspector]
    public int AxisIndex;

    [HideInInspector]
    public string[] Axises = new string[] { "X - Axis", "Y - Axis", "Z - Axis" };

    void Update()
    {
        switch (AxisIndex)
        {
            case 0:
                transform.Rotate(new Vector3(Time.deltaTime * RotationSpeed, 0, 0));
                break;
            case 1:
                transform.Rotate(new Vector3(0, Time.deltaTime * RotationSpeed, 0));
                break;
            case 2:
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * RotationSpeed));
                break;
            default:
                transform.Rotate(new Vector3(Time.deltaTime * RotationSpeed, 0, 0));
                break;
        }
    }
}

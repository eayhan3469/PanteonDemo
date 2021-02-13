using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SelfRotation))]
public class SelfRotationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SelfRotation selfRotation = (SelfRotation)target;

        GUIContent arrayLabel = new GUIContent("Axises"); 
        selfRotation.AxisIndex = EditorGUILayout.Popup(arrayLabel, selfRotation.AxisIndex, selfRotation.Axises);
    }
}

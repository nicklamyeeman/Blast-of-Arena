﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (DetectDirection))]
public class DetectDirectionEditor : Editor
{
    void OnSceneGUI()
    {
        DetectDirection fow = (DetectDirection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle/2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle/2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        // if (fow.getVisibleTarget() != null)
            // Handles.DrawLine(fow.transform.position, fow.getMousePos());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(EnemyFieldOfView))]
//public class FieldOfViewEditor : Editor
//{
//    void OnSceneGUI ()
//    {
//        EnemyFieldOfView fow = (EnemyFieldOfView)target;
//        Handles.color = UnityEngine.Color.white;
//        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
//        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
//        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

//        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
//        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

//        if (fow.visibleTarget != null)
//        {
//            Handles.color = Color.red;
//            Handles.DrawLine(fow.transform.position, fow.visibleTarget.position);
//        }
//    }
//}

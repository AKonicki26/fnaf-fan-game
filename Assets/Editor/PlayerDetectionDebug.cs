using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LionessManager))]
public class LionessPlayerDetectionDebug : Editor
{
    private void OnSceneGUI() {
        LionessManager animatronic = (LionessManager)target;
        Handles.color = Color.black;
        // Forward Cirlce
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, animatronic.DetectionAngleDegrees / 2, animatronic.DetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, -(animatronic.DetectionAngleDegrees / 2), animatronic.DetectionRadius);
        // Backward Circle
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, (360 - animatronic.DetectionAngleDegrees) / 2, animatronic.InnerDetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, -((360 - animatronic.DetectionAngleDegrees) / 2), animatronic.InnerDetectionRadius);
        // Jumpscare Circle
        Handles.color = Color.red;
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, Vector3.forward, 360, animatronic.GetComponent<CapsuleCollider>().radius + (animatronic.Player.GetComponentInChildren<CapsuleCollider>().radius * 2));

        Vector3 viewAngle01 = DirectionFromAngle(animatronic.transform.eulerAngles.y, -animatronic.DetectionAngleDegrees / 2);
        Vector3 viewAngle02 = DirectionFromAngle(animatronic.transform.eulerAngles.y, animatronic.DetectionAngleDegrees / 2);

        Handles.color = Color.green;
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle01 * animatronic.DetectionRadius);
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle02 * animatronic.DetectionRadius);

        if (animatronic.CanSeePlayer) {
            Handles.color = Color.yellow;
            Handles.DrawLine(animatronic.transform.position, animatronic.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

[CustomEditor(typeof(Criket_AI))]
public class CriketPlayerDetectionDebug : Editor {
    private void OnSceneGUI() {
        Criket_AI animatronic = (Criket_AI)target;
        Handles.color = Color.black;
        // Forward Cirlce
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, animatronic.DetectionAngleDegrees / 2, animatronic.DetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, -(animatronic.DetectionAngleDegrees / 2), animatronic.DetectionRadius);
        // Backward Circle
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, (360 - animatronic.DetectionAngleDegrees) / 2, animatronic.InnerDetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, -((360 - animatronic.DetectionAngleDegrees) / 2), animatronic.InnerDetectionRadius);
        // Jumpscare Circle
        Handles.color = Color.red;
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, Vector3.forward, 360, animatronic.GetComponent<CapsuleCollider>().radius + (animatronic.Player.GetComponentInChildren<CapsuleCollider>().radius * 2));

        Vector3 viewAngle01 = DirectionFromAngle(animatronic.transform.eulerAngles.y, -animatronic.DetectionAngleDegrees / 2);
        Vector3 viewAngle02 = DirectionFromAngle(animatronic.transform.eulerAngles.y, animatronic.DetectionAngleDegrees / 2);

        Handles.color = Color.green;
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle01 * animatronic.DetectionRadius);
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle02 * animatronic.DetectionRadius);

        if (animatronic.CanSeePlayer) {
            Handles.color = Color.yellow;
            Handles.DrawLine(animatronic.transform.position, animatronic.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

[CustomEditor(typeof(Dune_AI))]
public class CDunePlayerDetectionDebug : Editor {
    private void OnSceneGUI() {
        Dune_AI animatronic = (Dune_AI)target;
        Handles.color = Color.black;
        // Forward Cirlce
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, animatronic.DetectionAngleDegrees / 2, animatronic.DetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, animatronic.GetComponent<Transform>().forward, -(animatronic.DetectionAngleDegrees / 2), animatronic.DetectionRadius);
        // Backward Circle
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, (360 - animatronic.DetectionAngleDegrees) / 2, animatronic.InnerDetectionRadius);
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, -animatronic.GetComponent<Transform>().forward, -((360 - animatronic.DetectionAngleDegrees) / 2), animatronic.InnerDetectionRadius);
        // Jumpscare Circle
        Handles.color = Color.red;
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, Vector3.forward, 360, animatronic.GetComponent<CapsuleCollider>().radius + (animatronic.Player.GetComponentInChildren<CapsuleCollider>().radius * 2));

        Vector3 viewAngle01 = DirectionFromAngle(animatronic.transform.eulerAngles.y, -animatronic.DetectionAngleDegrees / 2);
        Vector3 viewAngle02 = DirectionFromAngle(animatronic.transform.eulerAngles.y, animatronic.DetectionAngleDegrees / 2);

        Handles.color = Color.green;
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle01 * animatronic.DetectionRadius);
        Handles.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle02 * animatronic.DetectionRadius);

        if (animatronic.CanSeePlayer) {
            Handles.color = Color.yellow;
            Handles.DrawLine(animatronic.transform.position, animatronic.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}



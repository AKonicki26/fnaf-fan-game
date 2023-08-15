using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player_Manager))]
public class PlayerEditor : Editor
{
    private void OnSceneGUI() {
        Player_Manager player = (Player_Manager)target;
        Handles.color = Color.black;
        Handles.DrawLine(player.GetComponentInChildren<Camera>().transform.position, player.FlashlightEndPosition, 1f);
        Handles.color = Color.white;
        if (player.SomethingToInteractWith)
            Handles.DrawDottedLine(player.GetComponentInChildren<Camera>().transform.position, player.Interactable.transform.position, 1f);
    }
}

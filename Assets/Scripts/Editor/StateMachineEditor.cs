using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateMachine))]
public class StateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        StateMachine gameEvent = target as StateMachine;
        if (GUILayout.Button("Set State"))
            gameEvent.SetState(gameEvent.testState);
    }
}

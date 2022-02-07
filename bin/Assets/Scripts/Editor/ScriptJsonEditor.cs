using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptObjDataManager))]
public class ScriptJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Json", GUILayout.Width(120), GUILayout.Height(20)))
        {
            ScriptObjDataManager.Instance.LoadJson();
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}

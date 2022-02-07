using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDataManager))]
public class ItemJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Load Json", GUILayout.Width(150), GUILayout.Height(20)))
        {
            ItemDataManager.Instance.LoadJson();
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}

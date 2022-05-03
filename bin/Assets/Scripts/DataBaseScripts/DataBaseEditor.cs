using UnityEditor;
using UnityEngine;

public class DataBaseEditor<T> : Editor where T : DataBase
{
    public T scriptDataManager;

    public void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            scriptDataManager = (T)target;
        }
        else
        {
            scriptDataManager = null;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Json", GUILayout.Width(120), GUILayout.Height(20)))
        {
            scriptDataManager.LoadJson();
        }
        if (GUILayout.Button("Save Json", GUILayout.Width(120), GUILayout.Height(20)))
        {
            scriptDataManager.SaveJson();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }
}
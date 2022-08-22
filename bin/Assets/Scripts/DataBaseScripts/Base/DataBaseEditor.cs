using UnityEditor;
using UnityEngine;

namespace DataBaseScripts.Base
{
#if UNITY_EDITOR
    public class DataBaseEditor<T1, T2> : Editor where T1 : DataBase<T2> where T2 : DataType
    {
        public T1 scriptDataManager;

        public void OnEnable()
        {
            if (AssetDatabase.Contains(target))
            {
                scriptDataManager = (T1)target;
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
#endif
}
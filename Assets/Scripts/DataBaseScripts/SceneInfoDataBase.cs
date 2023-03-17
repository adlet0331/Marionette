using DataBaseScripts.Base;
using Managers;
using UnityEditor;
using UnityEngine;

/*
 * 
 * 씬 정보 미리 넣어놓는 데이터 베이스
 * 
 */
namespace DataBaseScripts
{
    [CreateAssetMenu(fileName = "SceneInfoDataBase", menuName = "ScriptableObjects/SceneInfoDataBase", order = 200)]
    public class SceneInfoDataBase : DataBase<SceneInfo> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(SceneInfoDataBase))]
    public class SceneInfoDataBaseEditor : DataBaseEditor<SceneInfoDataBase, SceneInfo> { }
#endif
}
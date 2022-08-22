using System;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class SLData : DataType
    {
    }

    [CreateAssetMenu(fileName = "SLDataBase", menuName = "ScriptableObjects/SLDataBase", order = 1)]
    public class SLDataBase : DataBase<SLData> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(SLDataBase))]
    public class SLDataBasesEditor : DataBaseEditor<SLDataBase, SLData> { }
#endif
}
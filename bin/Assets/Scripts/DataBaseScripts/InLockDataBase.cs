using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class InLockData : DataType
    {
        public List<string> needType;
        public List<int> needItemArray;
        public List<string> noItemString;
    };

    [CreateAssetMenu(fileName = "InLockDataBase", menuName = "ScriptableObjects/InLockDataBase", order = 1)]
    public class InLockDataBase : DataBase<InLockData>
    {
    }

    [CustomEditor(typeof(InLockDataBase))]
    public class InLockDataBaseEditor : DataBaseEditor<InLockDataBase, InLockData>
    {
    }
}
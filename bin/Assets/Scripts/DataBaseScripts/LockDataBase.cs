using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class LockData : DataType
    {
        public int initStatus;
        public string interactString;
        public List<int> needItemIdxArray;
        public List<string> noItemString;
    };

    [CreateAssetMenu(fileName = "LockDataBase", menuName = "ScriptableObjects/LockDataBase", order = 1)]
    public class LockDataBase : DataBase<LockData>
    {
    }

    [CustomEditor(typeof(LockDataBase))]
    public class LockDataBaseEditor : DataBaseEditor<LockDataBase, LockData>
    {
    }
}
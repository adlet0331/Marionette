using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LockData : DataType
{
    public string interactString;
    public List<int> needItemIdxArray;
    public List<string> noItemString;
};
[CreateAssetMenu(fileName = "LockDataBase", menuName = "ScriptableObjects/LockDataBase", order = 1)]
public class LockDataBase : DataBase<LockData> { }
[CustomEditor(typeof(LockDataBase))]
public class LockDataBaseEditor : DataBaseEditor<LockDataBase, LockData>{ }
using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class LockData : DataType
    {
        // 0: Item, 1: AnimaAbility
        public List<int> needTypeList;
        public List<int> needItemList;
        public List<int> needItemNumList;
        public string lockedString;
        public string unLockString;
    };

    [CreateAssetMenu(fileName = "10_LockDataBase", menuName = "ScriptableObjects/10.LockDataBase", order = 10)]
    public class LockDataBase : DataBase<LockData>
    {
    }

    [CustomEditor(typeof(LockDataBase))]
    public class LockDataBaseEditor : DataBaseEditor<LockDataBase, LockData>
    {
    }
}
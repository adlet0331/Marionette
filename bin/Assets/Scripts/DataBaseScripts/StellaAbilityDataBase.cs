using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class StellaAbilityData : DataType
    {
        public int maxLevel;
        public List<int> levelUpCount;
        public int initStatus;
        public List<string> descriptionList;
    }
    [CreateAssetMenu(fileName = "StellaAbilityDataBase", menuName = "ScriptableObjects/StellaAbilityDataBase", order = 1)]
    public class StellaAbilityDataBase: DataBase<StellaAbilityData> { }
    [CustomEditor(typeof(StellaAbilityDataBase))]
    public class StellaAbilityDataBaseEditor : DataBaseEditor<StellaAbilityDataBase, StellaAbilityData>{ }
}
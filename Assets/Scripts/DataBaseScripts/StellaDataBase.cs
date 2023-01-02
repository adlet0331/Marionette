using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class StellaData : DataType
    {
        public int maxLevel;
        public List<int> levelUpCountList;
        public List<string> descriptionList;
        public string spriteName;
    }
    [CreateAssetMenu(fileName = "StellaDataBase", menuName = "ScriptableObjects/StellaDataBase", order = 100)]
    public class StellaDataBase: DataBase<StellaData> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(StellaDataBase))]
    public class StellaAbilityDataBaseEditor : DataBaseEditor<StellaDataBase, StellaData>{ }
#endif
}
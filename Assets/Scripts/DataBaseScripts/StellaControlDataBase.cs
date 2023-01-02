using System;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class StellaControlData : DataType
    {
        public int stellaIdx;
        public int getNum;
        public string getDescription;
    }

    [CreateAssetMenu(fileName = "11_StellaControlDataBase", menuName = "ScriptableObjects/11.StellaControlDataBase", order = 11)]
    public class StellaControlDataBase : DataBase<StellaControlData> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(StellaControlDataBase))]
    public class StellaControlDataBaseEditor : DataBaseEditor<StellaControlDataBase, StellaControlData> { }
#endif
}
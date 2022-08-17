using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using InGameObjects.Interaction;
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

    [CreateAssetMenu(fileName = "11_StellaControlDataBase", menuName = "ScriptableObjects/11.StellaControlDataBase", order = 4)]
    public class StellaControlDataBase : DataBase<StellaControlData>
    {
    }

    [CustomEditor(typeof(StellaControlDataBase))]
    public class StellaControlDataBaseEditor : DataBaseEditor<StellaControlDataBase, StellaControlData>
    {
    }
}
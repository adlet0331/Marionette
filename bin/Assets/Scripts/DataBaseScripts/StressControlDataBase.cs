using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class StressControlData : DataType
    {
        public int stressAdd;
    };

    [CreateAssetMenu(fileName = "9_StressControlDataBase", menuName = "ScriptableObjects/9.StressControlDataBase", order = 9)]
    public class StressControlDataBase : DataBase<StressControlData>
    {
    }

    [CustomEditor(typeof(StressControlDataBase))]
    public class StressControlDataBaseEditor : DataBaseEditor<StressControlDataBase, StressControlData>
    {
    }
}
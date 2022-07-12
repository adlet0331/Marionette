using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class InChooseData : DataType
    {
        public List<string> scriptList;
        public List<int> interactionList;
    }

    [CreateAssetMenu(fileName = "InChooseDataDataBase", menuName = "ScriptableObjects/InChooseDataDataBase", order = 1)]
    public class InChooseDataBase : DataBase<InChooseData>
    {
    }

    [CustomEditor(typeof(InChooseDataBase))]
    public class InChooseDataBaseEditor : DataBaseEditor<InChooseDataBase, InChooseData>
    {
    }
}
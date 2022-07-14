using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class ChooseData : DataType
    {
        public List<string> scriptList;
        public List<int> interactionList;
    }

    [CreateAssetMenu(fileName = "4_ChooseDataDataBase", menuName = "ScriptableObjects/4.ChooseDataDataBase", order = 4)]
    public class ChooseDataBase : DataBase<ChooseData>
    {
    }

    [CustomEditor(typeof(ChooseDataBase))]
    public class ChooseDataBaseEditor : DataBaseEditor<ChooseDataBase, ChooseData>
    {
    }
}
using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using InGameObjects.Interaction;
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
        public List<InteractingObject> interactionGameObjectList;
    }

    [CreateAssetMenu(fileName = "4_ChooseDataBase", menuName = "ScriptableObjects/4.ChooseDataBase", order = 4)]
    public class ChooseDataBase : DataBase<ChooseData>
    {
    }

    [CustomEditor(typeof(ChooseDataBase))]
    public class ChooseDataBaseEditor : DataBaseEditor<ChooseDataBase, ChooseData>
    {
    }
}
using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class InteractionData : DataType
    {
        public List<int> typeList;
        public List<int> idxList;
        public List<bool> goNextImmediately;
        public bool initStatus;
        public bool disableAfterInteract;
    }
    
    [CreateAssetMenu(fileName = "InteractionDataBase", menuName = "ScriptableObjects/InteractionDataBase", order = 1)]
    public class InteractionDataBase : DataBase<InteractionData> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(InteractionDataBase))]
    public class InteractionDataBaseEditor : DataBaseEditor<InteractionDataBase, InteractionData> { }
#endif
}
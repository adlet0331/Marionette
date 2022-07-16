﻿using System;
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
        public bool initStatus;
    }
    
    [CreateAssetMenu(fileName = "InteractionDataBase", menuName = "ScriptableObjects/InteractionDataBase", order = 1)]
    public class InteractionDataBase : DataBase<InteractionData>
    {
    }

    [CustomEditor(typeof(InteractionDataBase))]
    public class InteractionDataBaseEditor : DataBaseEditor<InteractionDataBase, InteractionData>
    {
    }
}
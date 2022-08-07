﻿using System;
using DataBaseScripts.Base;
using Managers;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class MoveControlData : DataType
    {
        public SceneSwitchManager.SceneName destinationScene;
    };

    [CreateAssetMenu(fileName = "5_MoveControlDataBase", menuName = "ScriptableObjects/5.MoveControlDataBase", order = 5)]
    public class MoveControlDataBase : DataBase<MoveControlData>
    {
    }

    [CustomEditor(typeof(MoveControlDataBase))]
    public class MoveControlDataBaseEditor : DataBaseEditor<MoveControlDataBase, MoveControlData>
    {
    }
}
using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class CutSceneData : DataType
    {
        public List<string> spriteList;
        public List<int> playTimeList;
        public List<bool> goNextImmediately;
    }
    [CreateAssetMenu(fileName = "12_CutSceneDataBase", menuName = "ScriptableObjects/12.CutSceneDataBase", order = 12)]
    public class CutSceneDataBase: DataBase<CutSceneData> { }
    [CustomEditor(typeof(CutSceneDataBase))]
    public class CutSceneDataBaseEditor : DataBaseEditor<CutSceneDataBase, CutSceneData>{ }
}
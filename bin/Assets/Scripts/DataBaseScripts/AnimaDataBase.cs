using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class AnimaData : DataType
    {
        public List<int> animaIndexList;
        public List<int> stellaIdxList;
        public List<int> stellaAmountList;
        public string description;
        public string image;
    }
    [CreateAssetMenu(fileName = "AnimaDataBase", menuName = "ScriptableObjects/AnimaDataBase", order = 1)]
    public class AnimaDataBase: DataBase<AnimaData> { }
#if UNITY_EDITOR
    [CustomEditor(typeof(AnimaDataBase))]
    public class AnimaDataBaseEditor : DataBaseEditor<AnimaDataBase, AnimaData>{ }
#endif
}
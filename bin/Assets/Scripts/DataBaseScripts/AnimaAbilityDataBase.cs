using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class AnimaAbilityData : DataType
    {
        public int maxLevel;
        public List<int> levelUpCount;
        public int initStatus;
    }
    [CreateAssetMenu(fileName = "AnimaAbilityDataBase", menuName = "ScriptableObjects/AnimaAbilityDataBase", order = 1)]
    public class AnimaAbilityDataBase: DataBase<AnimaAbilityData> { }
    [CustomEditor(typeof(AnimaAbilityDataBase))]
    public class AnimaAbilityDataBaseEditor : DataBaseEditor<AnimaAbilityDataBase, AnimaAbilityData>{ }
}
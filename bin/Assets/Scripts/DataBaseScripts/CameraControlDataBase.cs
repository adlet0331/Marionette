using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public enum CameraWalkType
    {
        FadeOut = 0,
        FadeIn = 1,
        FollowGameObject = 2,
    }
    [Serializable]
    public class CameraControlData : DataType
    {
        // 0: Item, 1: AnimaAbility
        public CameraWalkType type;
    };

    [CreateAssetMenu(fileName = "7_CameraControlDataBase", menuName = "ScriptableObjects/7.CameraControlDataBase", order = 7)]
    public class CameraControlDataBase : DataBase<CameraControlData>
    {
    }

    [CustomEditor(typeof(CameraControlDataBase))]
    public class CameraControlDataBaseEditor : DataBaseEditor<CameraControlDataBase, CameraControlData>
    {
    }
}
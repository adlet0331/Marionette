using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class SLData : DataType
    {
    }

    [CreateAssetMenu(fileName = "SLDataBase", menuName = "ScriptableObjects/SLDataBase", order = 1)]
    public class SLDataBase : DataBase<SLData>
    {
    }

    [CustomEditor(typeof(SLDataBase))]
    public class SLDataBasesEditor : DataBaseEditor<SLDataBase, SLData>
    {
    }
}
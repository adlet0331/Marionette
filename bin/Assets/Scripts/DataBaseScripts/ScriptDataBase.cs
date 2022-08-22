using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

/* 게임 내 Script를 가지는 오브젝트의 데이터 (idx로 사용)
 * -> 이름, 이미지, scriptableScript, Scripts
 * 
 */
namespace DataBaseScripts
{
    [Serializable]
    public class ScriptData : DataType
    {
        public List<string> titleList;
        public List<string> scriptList;
        public List<string> leftSpriteList;
        public List<string> rightSpriteList;
    }

    [CreateAssetMenu(fileName = "3_ScriptDataBase", menuName = "ScriptableObjects/3.ScriptDataBase", order = 3)]
    public class ScriptDataBase : DataBase<ScriptData>
    {
    }

    [CustomEditor(typeof(ScriptDataBase))]
    public class ScriptDataBaseEditor : DataBaseEditor<ScriptDataBase, ScriptData>
    {
    }
}
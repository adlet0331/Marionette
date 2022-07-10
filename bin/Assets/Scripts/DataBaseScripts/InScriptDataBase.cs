using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/* 게임 내 Script를 가지는 오브젝트의 데이터 (idx로 사용)
 * -> 이름, 이미지, scriptableScript, Scripts
 * 
 */
namespace DataBaseScripts
{
    [Serializable]
    public class InScriptData : DataType
    {
        public string interactString;
        public List<string> scriptList;
        public List<string> leftSpriteList;
        public List<string> rightSpriteList;
    }

    [CreateAssetMenu(fileName = "InScriptDataBase", menuName = "ScriptableObjects/InScriptDataBase", order = 1)]
    public class InScriptDataBase : DataBase<InScriptData>
    {
    }

    [CustomEditor(typeof(InScriptDataBase))]
    public class InScriptDataBaseEditor : DataBaseEditor<InScriptDataBase, InScriptData>
    {
    }
}
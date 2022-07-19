using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DollTalkData : DataType
    {
        public List<string> scriptList;
        public List<string> leftSpriteList;
        public List<string> rightSpriteList;
        public List<bool> isGirlTalking;
    }

    [CreateAssetMenu(fileName = "DollTalkDataBase", menuName = "ScriptableObjects/DollTalkDataBase", order = 1)]
    public class DollTalkDataBase : DataBase<DollTalkData>
    {
    }

    [CustomEditor(typeof(DollTalkDataBase))]
    public class DollTalkDataBaseEditor : DataBaseEditor<DollTalkDataBase, DollTalkData>
    {
    }
}
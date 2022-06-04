using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/* 게임 내 Script를 가지는 오브젝트의 데이터 (idx로 사용)
 * -> 이름, 이미지, scriptableScript, Scripts
 * 
 */
[Serializable]
public class ScriptData : DataType
{
    public string interactString;
    public List<string> scripts;
}

[CreateAssetMenu(fileName = "ScriptDataBase", menuName = "ScriptableObjects/ScriptDataBase", order = 1)]
public class ScriptDataBase : DataBase <ScriptData> { }

[CustomEditor(typeof(ScriptDataBase))]
public class ScriptDataBaseEditor : DataBaseEditor<ScriptDataBase, ScriptData> { }

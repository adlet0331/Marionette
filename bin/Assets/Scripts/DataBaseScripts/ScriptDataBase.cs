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
public class ScriptableObjData
{
    public int idx;
    public string name;
    public string scriptableScript;
    public List<string> scripts;
    public ScriptableObjData(int idx, string name, string scriptableScript, List<string> scripts)
    {
        this.idx = idx;
        this.name = name;
        this.scriptableScript = scriptableScript;
        this.scripts = new List<string>();
        foreach (string script in scripts)
        {
            this.scripts.Add(script);
        }
    }
}

[Serializable]
public class ScriptObjDataStruct
{
    public int idx;
    public string name;
    public string scriptableScript;
    public string[] scripts;
}

[CreateAssetMenu(fileName = "ScriptDataBase", menuName = "ScriptableObjects/ScriptDataBase", order = 1)]
public class ScriptDataBase : DataBase
{
    public List<ScriptableObjData> ScriptObjDataList;

    public override void LoadJson()
    {
        ScriptObjDataList = new List<ScriptableObjData>();

        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/Scripts");
        ScriptObjDataStruct[] Datas = JsonHelper.FromJson<ScriptObjDataStruct>("{\"resources\":" + jsonData.text + "}");
        foreach (ScriptObjDataStruct scriptDataStruct in Datas)
        {
            ScriptObjDataList.Add(new ScriptableObjData(scriptDataStruct.idx, scriptDataStruct.name, scriptDataStruct.scriptableScript, scriptDataStruct.scripts.ToList()));
        }
        return;
    }
}

[CustomEditor(typeof(ScriptDataBase))]
public class ScriptDataBaseEditor : DataBaseEditor<ScriptDataBase> { }

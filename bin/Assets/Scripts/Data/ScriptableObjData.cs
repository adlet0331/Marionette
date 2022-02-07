using System;
using System.Collections;
using System.Collections.Generic;
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

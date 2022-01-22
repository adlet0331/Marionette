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
    public string name;
    public Sprite sprite;
    public string scriptableScript;
    public List<string> scripts;
    public ScriptableObjData()
    {
        name = "더미";
        sprite = null;
        scriptableScript = "이건 더미입니다";
        scripts = new List<string>();
        scripts.Add("더미 대사 1입니다");
        scripts.Add("더미 대사 2 \n하하 줄을 바꿀줄은 몰랐겠지!");
    }
    public ScriptableObjData(string name, string scriptableScript, List<string> scripts)
    {
        this.name = name;
        this.scriptableScript = scriptableScript;
        this.scripts = new List<string>();
        foreach (string script in scripts)
        {
            this.scripts.Add(script);
        }
    }
}

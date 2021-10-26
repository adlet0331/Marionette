using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ���� �� Script�� ������ ������Ʈ�� ������ (idx�� ���)
 * -> �̸�, �̹���, scriptableScript, Scripts
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
        name = "����";
        sprite = null;
        scriptableScript = "�̰� �����Դϴ�";
        scripts = new List<string>();
        scripts.Add("���� ��� 1�Դϴ�");
        scripts.Add("���� ��� 2\n���� ���� �ٲ����� ��������!");
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

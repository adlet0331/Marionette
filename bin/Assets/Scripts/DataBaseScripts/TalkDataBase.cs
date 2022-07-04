using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class TalkData : DataType
{
    public int length;
    public List<int> talkType;
    public List<string> talkSprite;
    public List<string> talkScript;
    public List<int> selectIndex;
    public List<int> selectNum;
    public List<string> selectScript;
    public List<int> nextTalkIdx;
}
[CreateAssetMenu(fileName = "TalkDataBase", menuName = "ScriptableObjects/TalkDataBase", order = 1)]
public class TalkDataBase : DataBase<TalkData> { }
[CustomEditor(typeof(TalkDataBase))]
public class TalkDataBaseEditor : DataBaseEditor<TalkDataBase, TalkData> { }


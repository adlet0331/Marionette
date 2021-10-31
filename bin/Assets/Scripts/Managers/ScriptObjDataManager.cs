using System.Collections.Generic;

/* 게임 내 Script를 가지는 오브젝트의 데이터 List
 * 
 * ScriptableObjectData의 List
 * 데이터베이스 비슷하게 사용
 * 
 */
public class ScriptObjDataManager : Singleton<ScriptObjDataManager>
{
    public List<ScriptableObjData> ScriptObjDataList;
}

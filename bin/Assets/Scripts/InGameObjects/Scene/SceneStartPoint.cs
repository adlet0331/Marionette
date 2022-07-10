using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneSwitchManager;

/*
 * 플레이어가 시작하는 좌표를 지정해주는 오브젝트
 * 
 */

public class SceneStartPoint : MonoBehaviour
{
    [SerializeField] private SceneName beforeSceneName;
    public enum StartPointType
    {
        Player = 0,
        Doll1 = 1,
    }
    [SerializeField] private StartPointType thisType;
    public StartPointType GetObjType()
    {
        return thisType;
    }
    private void Start()
    {
        if (beforeSceneName == SceneSwitchManager.Instance.beforeScene)
        {
            SceneObjManager.Instance.UpdatePlayerPos(this.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어가 시작하는 좌표를 지정해주는 오브젝트
 * 
 * 한 씬에 하나만
 * 
 */

public class SceneStartPoint : MonoBehaviour
{
    public enum ObjectType
    {
        Player = 0,
        Doll1 = 1,
    }
    [SerializeField] private ObjectType thisType;
    private void Start()
    {
        if (thisType == ObjectType.Player)
        {
            SceneObjManager.Instance.UpdatePlayerPos(this.transform.position);
        }
    }
}

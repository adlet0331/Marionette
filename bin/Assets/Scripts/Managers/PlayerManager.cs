using System.Collections.Generic;
using UnityEngine;
/* 플레이어 관리
 * 
 * 범위내에 있는 Scriptable Object를 List로 가지고 있음 
 * -> 가장 가까운 애가 Scriptable Window로 뜸
 * 
 */
public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject moveablePlayerObject;
    public BoxCollider2D moveablePlayerCollider;
    public InteractObject playerInteractObject;

    public void UpdatePlayerManager(GameObject mPO)
    {
        moveablePlayerObject = mPO;
        moveablePlayerCollider = moveablePlayerObject.GetComponent<BoxCollider2D>();
        InputManager.Instance.SetMovingComponent(mPO.GetComponent<MovingObject>());
    }
}

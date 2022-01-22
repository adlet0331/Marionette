﻿using static Assets.Scripts.InGameObjects.DontDestroyObject;

/*
 * 플레이어가 조종해서 움직이는 캐릭터
 * 
 * 한 씬에 하나만 존재해야함
 * 
 */

public class PlayableMovableObjejct : MovingObject
{
    private void Start()
    {
        base.MovingObjectStart();
        PlayerManager.Instance.UpdatePlayerManager(this.gameObject);
        CameraManager.Instance.SetFollowingObject(this.gameObject);
        SceneObjManager.Instance.AddObject(ObjectType.PlayerObject, this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �÷��̾ �����ؼ� �����̴� ĳ����
 * 
 * �� ���� �ϳ��� �����ؾ���
 * 
 */

public class PlayableMovableObjejct : MovingObject
{
    private void Start()
    {
        base.MovingObjectStart();
        PlayerManager.Instance.UpdatePlayerManager(this.gameObject);
        CameraManager.Instance.SetFollowingObject(this.gameObject);
    }
}

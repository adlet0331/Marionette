using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Map ������Ʈ
 * Camera ��� ������ ���ϴ� ������Ʈ��
 * 1 ���� 1 ��
 * 
 */

public class MapObject : MonoBehaviour
{
    void Start()
    {
        CameraManager.Instance.SetMap(this.gameObject);
    }
}

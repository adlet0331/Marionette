using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Map 오브젝트
 * Camera 경계 범위를 정하는 오브젝트임
 * 1 씬에 1 개
 * 
 */

public class MapObject : MonoBehaviour
{
    void Start()
    {
        CameraManager.Instance.SetMap(this.gameObject);
    }
}

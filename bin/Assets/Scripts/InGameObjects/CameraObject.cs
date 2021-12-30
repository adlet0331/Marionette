using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 씬에 있는 메인 카메라
 */
public class CameraObject : MonoBehaviour
{
    [SerializeField] private bool isMainCamera;
    private void Start()
    {
        if (isMainCamera)
        {
            CameraManager.Instance.SetCamera(this.GetComponent<Camera>());
        }
    }
}

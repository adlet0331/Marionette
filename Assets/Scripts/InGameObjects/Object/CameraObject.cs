﻿using Managers;
using UnityEngine;

/*
 * 씬에 있는 메인 카메라
 */
namespace InGameObjects.Object
{
    public class CameraObject : MonoBehaviour
    {
        [SerializeField] private bool isMainCamera;
        private void Start()
        {
            if (isMainCamera)
            {
                CameraManager.Instance.SetCamera(this.GetComponent<Camera>());
                if (SceneObjManager.Instance.canvas)
                    SceneObjManager.Instance.canvas.GetComponent<Canvas>().worldCamera = this.GetComponent<Camera>();
            }
        }
    }
}
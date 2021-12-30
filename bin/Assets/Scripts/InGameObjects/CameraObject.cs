using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� �ִ� ���� ī�޶�
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

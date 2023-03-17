using Managers;
using UnityEngine;

/*
 * Map 오브젝트
 * Camera 경계 범위를 정하는 오브젝트임
 * 1 씬에 1 개
 * 
 */
namespace InGameObjects.Object
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CameraBorderWithCollider : MonoBehaviour
    {
        private void Start()
        {
            GamePlayManager.Instance.SetMapCameraBorder(this.gameObject);
        }
    }
}
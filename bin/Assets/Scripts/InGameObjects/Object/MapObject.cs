using UnityEngine;

/*
 * Map 오브젝트
 * Camera 경계 범위를 정하는 오브젝트임
 * 1 씬에 1 개
 * 
 */

namespace InGameObjects.Object
{
    public class MapObject : MonoBehaviour
    {
        private void Start()
        {
            CameraManager.Instance.SetMap(this.gameObject);
        }
    }
}

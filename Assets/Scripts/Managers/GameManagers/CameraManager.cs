using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/* Camera 제어
 * - 캐릭터 따라가기
 * 
 */
namespace Managers
{
    [Serializable]
    public class CameraManager : AGameManager
    {
        [SerializeField] private Camera currentGameCamera;
        public void SetGameCamera(Camera camera)
        {
            currentGameCamera = camera;
        }

        public Vector2 MouseCursorWorldPosition
        {
            get
            {
                if (!currentGameCamera)
                    return new Vector2(0, 0);
                var mouseCursorPos = Input.mousePosition;
                return (Vector2) currentGameCamera.ScreenToWorldPoint(mouseCursorPos);
            }
        }
        
        [SerializeField] private GameObject currentMap;
        private BoxCollider2D currentMapCollider;
        public void SetMapCameraBorder(GameObject map)
        {
            currentMap = map;
            // 첫번째 콜라이더만. Map의 사이즈를 위함 - Istrigger = true
            currentMapCollider = map.GetComponent<BoxCollider2D>();
        }

        [SerializeField] private GameObject followingObject;
        public void SetFollowingObject(GameObject go)
        {
            followingObject = go;
        }

        [SerializeField] private int currentMode;
        public int CurrentMode
        {
            get => currentMode;
            set
            {
                currentMode = value;
                
                if (!currentGameCamera)
                    return;
                
                if (currentMode == 0)
                    return;
                if (currentMode == 1)
                {
                    currentGameCamera.transform.position = CameraMode1GetPosition();
                }
            }
        }
        
        [SerializeField] private string currentModeString;
        [SerializeField] private string[] modeList;
        private Resolution currentResolution;
        public void SetCameraMode(int mode)
        {
            if (modeList[mode] == null)
            {
                return;
            }

            CurrentMode = mode;
            currentModeString = modeList[mode];
            return;
        }

        public async UniTask CameraMoveTargetAsync(Vector3 startVector3, Vector3 destinationVector3, float totalTimeMiliSecond, float timeIntervalSecond)
        {
            int beforeMode = CurrentMode;
            CurrentMode = 2;

            float milisecondPassed = 0.0f;
            while (milisecondPassed <= totalTimeMiliSecond)
            {
                currentGameCamera.transform.localPosition = Vector3.Lerp(startVector3, destinationVector3,
                    milisecondPassed / totalTimeMiliSecond);
                await UniTask.Delay(TimeSpan.FromSeconds(timeIntervalSecond));
                milisecondPassed += timeIntervalSecond * 1000.0f;
            }
            
            CurrentMode = beforeMode;
        }

        public void UpdateCameraPosition()
        {
            if (currentMode == 1)
            {
                currentGameCamera.transform.position = CameraMode1GetPosition();
            }
        }
        
        [SerializeField] private double CameraSize_X;
        [SerializeField] private double CameraSize_Y;
        [SerializeField] private double ScreenSize_X;
        [SerializeField] private double ScreenSize_Y;

        private void _debug() {
            CameraSize_X = currentGameCamera.orthographicSize * Screen.width / Screen.height;
            CameraSize_Y = currentGameCamera.orthographicSize;
            ScreenSize_X = Screen.width;
            ScreenSize_Y = Screen.height;
        }

        private Vector3 CameraMode1GetPosition() {
            if (!followingObject || !currentMap )
                return new Vector3(0,0,-1);
            double posx = followingObject.transform.position.x;
            double posy = followingObject.transform.position.y;
            double cameraWidth = currentGameCamera.orthographicSize * Screen.width / Screen.height;
            double cameraHeight = currentGameCamera.orthographicSize;

            Vector3 mapScale = currentMap.transform.localScale;
            double colliderx = currentMapCollider.transform.position.x;
            double collidery = currentMapCollider.transform.position.y;
            double colliderxWidth = currentMapCollider.size.x * mapScale.x * 0.5;
            double collideryWidth = currentMapCollider.size.y * mapScale.y * 0.5;

            if (posx + cameraWidth > colliderx + colliderxWidth) {
                posx = colliderx + colliderxWidth - cameraWidth;
            }
            else if(posx - cameraWidth < colliderx - colliderxWidth) {
                posx = colliderx - colliderxWidth + cameraWidth;
            }

            if (posy + cameraHeight > collidery + collideryWidth) {
                posy = collidery + collideryWidth - cameraHeight;
            }
            else if (posy - cameraHeight < collidery - collideryWidth) {
                posy = collidery - collideryWidth + cameraHeight;
            }

            return new Vector3((float)posx, (float)posy, -1);
        }

        public override void Start()
        {
            modeList = new string[]
            {
                "Static", "FollowingCharacter"
            };
            SetCameraMode(0);
        }
    }
}

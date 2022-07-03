using UnityEngine;

/* Camera 제어
 * - 캐릭터 따라가기
 * 
 */
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Camera currentCamera;
    public void SetCamera(Camera camera)
    {
        currentCamera = camera;
    }

    public Vector2 GetMouseCursorWorldPointVec2()
    {
        if (!currentCamera)
            return new Vector2(0, 0);
        var mouseCursorPos = Input.mousePosition;
        return  (Vector2) currentCamera.ScreenToWorldPoint(mouseCursorPos);
    }

    [SerializeField] private GameObject currentMap;
    private BoxCollider2D currentMapCollider;
    public void SetMap(GameObject map)
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
    [SerializeField] private string currentModeString;
    [SerializeField] private string[] modeList;
    private Resolution currentResolution;
    public int GetCameraMode()
    {
        return currentMode;
    }
    public void SetCameraMode(int mode)
    {
        if (modeList[mode] == null)
        {
            return;
        }

        currentMode = mode;
        currentModeString = modeList[mode];
        return;
    }

    private void Start() {
        SetCameraMode(1);
    }
    private void LateUpdate()
    {
        if (currentCamera == null)
            return;
        if (currentMode == 0)
        {
            return;
        }
        if (currentMode == 1)
        {
            currentCamera.transform.position = CameraMode1GetPosition();
            return;
        }
    }
    [SerializeField] private double CameraSize_X;
    [SerializeField] private double CameraSize_Y;
    [SerializeField] private double ScreenSize_X;
    [SerializeField] private double ScreenSize_Y;

    private void _debug() {
        CameraSize_X = currentCamera.orthographicSize * Screen.width / Screen.height;
        CameraSize_Y = currentCamera.orthographicSize;
        ScreenSize_X = Screen.width;
        ScreenSize_Y = Screen.height;
    }

    private Vector3 CameraMode1GetPosition() {
        if (followingObject == null || currentMap == null)
            return new Vector3(0,0,-1);
        double posx = followingObject.transform.position.x;
        double posy = followingObject.transform.position.y;
        double cameraWidth = currentCamera.orthographicSize * Screen.width / Screen.height;
        double cameraHeight = currentCamera.orthographicSize;

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
}

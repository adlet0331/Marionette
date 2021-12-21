using UnityEngine;

/* Camera 제어
 * - 캐릭터 따라가기
 * 
 */
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private string currentModeString;

    [SerializeField] private Camera currentCamera;
    [SerializeField] private GameObject currentMap;
    private BoxCollider2D currentMapCollider;
    [SerializeField] private GameObject followingObject;

    [SerializeField] private string[] modeList;
    private int currentMode;
    private Resolution currentResolution;

    public void SetCameraMode(int mode)
    {
        if (modeList[mode] == null)
            return;

        currentMode = mode;
        currentModeString = modeList[mode];
        return;
    }

    public void UpdateMap()
    {
        currentMap = GameObject.FindWithTag("Map");
        currentMapCollider = currentMap.GetComponent<BoxCollider2D>();
    }

    private void Start() {
        SetCameraMode(0);
        UpdateMap();
    }
    private void Update() {
        //_debug();
        if (currentMode == 0)
        {
            return;
        }
        if (currentMode == 1) {
            if (currentMap == null)
            {
                MapManager.Instance.UpdateMapCollection();
                currentMap = MapManager.Instance.GetMap(0);
                currentMapCollider = currentMap.GetComponent<BoxCollider2D>();
            }
            currentCamera.transform.position = GetFinalPosition();
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

    private Vector3 GetFinalPosition() {
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

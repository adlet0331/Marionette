using UnityEngine;
/* 해상도 관리
 * 
 */
public class ScreenManager : Singleton<ScreenManager>
{
    [System.Serializable]
    private class Resolution {
        public string name;
        public int x;
        public int y;
        public Resolution(Resolution resolution) {
            name = resolution.name;
            x = resolution.x;
            y = resolution.y;
        }
    }

    [SerializeField] private Resolution[] resolutionList;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private Resolution currentResolution;
    private void Start() {
        //SetResolution(0);
    }
    public void SetResolution(int index) {
        currentResolution = resolutionList[index];
        Screen.SetResolution(currentResolution.x, currentResolution.y, true);
        currentCamera.orthographicSize = 360;
    }
}

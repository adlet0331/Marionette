using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    ResolutionManager instance;
    #region Singleton
    private void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton

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
        SetResolution(0);
    }
    public void SetResolution(int index) {
        currentResolution = resolutionList[index];
        Screen.SetResolution(currentResolution.x, currentResolution.y, true);
        currentCamera.orthographicSize = 360;
    }
}

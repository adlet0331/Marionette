using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    #region Singleton
    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    [System.Serializable]
    private class CameraMode {
        public string name;
    }

    [SerializeField] private string currentModeString;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private GameObject followingObject;
    [SerializeField] private GameObject currentMap;

    private BoxCollider2D currentMapCollider;

    [SerializeField] private CameraMode[] modeList;
    private int currentMode;
    private Resolution currentResolution;

    private void Start() {
        SetCameraMode(0);
        SetMap(0);
    }
    private void Update() {
        if(currentMode == 0) {
            currentCamera.transform.position = GetFinalPosition();
            return;
        }
    }

    public void SetCameraMode(int mode) {
        if (modeList[mode] == null)
            return;

        currentMode = mode;
        currentModeString = modeList[mode].name;
        return;
    }

    public void SetMap(int index) {
        currentMap = MapManager.instance.GetMap(index);
        currentMapCollider = currentMap.GetComponent<BoxCollider2D>();
    }

    private Vector3 GetFinalPosition() {
        double posx = followingObject.transform.position.x;
        double posy = followingObject.transform.position.y;
        double cameraWidth = currentCamera.scaledPixelWidth * 0.5;
        double cameraHeight = currentCamera.scaledPixelHeight * 0.5;

        Vector3 mapScale = currentMap.transform.localScale;
        double colliderx = currentMapCollider.transform.position.x;
        double collidery = currentMapCollider.transform.position.y;
        double colliderxWidth = currentMapCollider.size.x * mapScale.x * 0.5;
        double collideryWidth = currentMapCollider.size.y * mapScale.y * 0.5;

        Debug.Log("posx, posy" + posx + " " + posy);
        Debug.Log("cameraWidth, cameraHeight" + cameraWidth + " " + cameraHeight);

        Debug.Log("colliderx, collidery" + colliderx + " " + collidery);
        Debug.Log("colliderxWidth, collideryWidth" + colliderxWidth + " " + collideryWidth);

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

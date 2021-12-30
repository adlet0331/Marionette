using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : Singleton<SceneSwitchManager>
{
    [Serializable]
    public class SceneInfo
    {
        public string SceneName;
        public bool isMovable;
        public bool isInputAvaliable;
        public int CameraMode;
    }

    [SerializeField] private List<SceneInfo> sceneInfoList;
    [SerializeField] string currentScene;

    private void Start()
    {
        currentScene = "StartScene";
    }

    public string GetScene()
    {
        return currentScene;
    }

    public void SwitchScene(string sceneName)
    {
        foreach (SceneInfo sceneInfo in sceneInfoList)
        {
            if (sceneInfo.SceneName == sceneName)
            {
                InputManager.Instance.SetOptions(sceneInfo.isMovable, sceneInfo.isInputAvaliable);
                CameraManager.Instance.SetCameraMode(sceneInfo.CameraMode);
                goto success;
            }
        }
        Debug.Assert(false, "SceneName : " + sceneName + " is not Exist in this game");
        return;
    success:
        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;
        return;
    }
}

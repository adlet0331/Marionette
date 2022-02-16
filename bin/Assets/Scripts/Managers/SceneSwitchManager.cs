using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Scripts.InGameObjects.DontDestroyObject;

public class SceneSwitchManager : Singleton<SceneSwitchManager>
{
    public enum SceneName
    {
        StartScene = 0,
        Girl_room = 1,
        Lab_1_1 = 2,
        Lab_1_2 = 3,
        RestArea = 4,
        Prison1 = 5,
        Control1 = 6,
        Corrider = 7,
    }
    [Serializable]
    public class SceneInfo
    {
        public SceneName sceneName;
        public bool isCharacterExist;
        public bool isMovable;
        public bool isInputAvaliable;
        public int CameraMode;
    }

    [SerializeField] private SceneName currentScene;
    [SerializeField] public SceneName beforeScene;
    [ArrayElementTitle("sceneName")]
    [SerializeField] private List<SceneInfo> sceneInfoList;
    private void Start()
    {
        currentScene = SceneName.StartScene;
    }
    private SceneInfo FindSceneInfo(SceneName sceneName)
    {
        foreach (SceneInfo sceneInfo in sceneInfoList)
        {
            if (sceneInfo.sceneName == sceneName)
            {
                return sceneInfo;
            }
        }
        return null;
    }
    private void setSceneOptions(SceneInfo sceneInfo)
    {
        // Input Manager 옵션 설정
        InputManager.Instance.SetOptions(sceneInfo.isMovable, sceneInfo.isInputAvaliable);
        // 카메라 매니저 모드 설정
        CameraManager.Instance.SetCameraMode(sceneInfo.CameraMode);
        SceneObjManager.Instance.PlayerObjectExist = sceneInfo.isCharacterExist;
    }
    public void NewGameButton()
    {
        SwitchScene(SceneName.Girl_room);
    }
    public void SwitchScene(SceneName sceneName)
    {
        SceneInfo sceneInfo = FindSceneInfo(sceneName);
        Debug.Assert(sceneInfo != null, "SceneName : " + sceneName.ToString() + " is not Exist in this game");
        beforeScene = currentScene;
        currentScene = sceneName;
        SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
        setSceneOptions(sceneInfo);
        return;
    }
}

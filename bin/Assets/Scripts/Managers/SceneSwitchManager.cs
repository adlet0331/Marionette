using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitchManager : Singleton<SceneSwitchManager>
{
    public enum SceneName
    {
        P_StartScene = 0,
        P_Girl_room = 1,
        P_Lab_1_1 = 2,
        P_Lab_1_2 = 3,
        P_RestArea = 4,
        P_Prison1 = 5,
        P_Control1 = 6,
        P_Corrider = 7,
    }
    [Serializable]
    public class SceneInfo
    {
        public SceneName sceneName;
        public bool isProfileActivate;
        public bool isCharacterExist;
        public bool isMovable;
        public bool isInputAvaliable;
        public int CameraMode;
    }

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private SceneName currentScene;
    [SerializeField] public SceneName beforeScene;
    [ArrayElementTitle("sceneName")]
    [SerializeField] private List<SceneInfo> sceneInfoList;
    private void Start()
    {
        currentScene = SceneName.P_StartScene;
        newGameButton.onClick.AddListener(NewGame);
        loadButton.onClick.AddListener(LoadGame);
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
        // 플레이어 캐릭터 생성
        SceneObjManager.Instance.PlayerObjectExist = sceneInfo.isCharacterExist;
        // 플레이어 프로필 On / Off
        PlayerManager.Instance.SetProfileShowing(sceneInfo.isProfileActivate);
    }
    public void NewGame()
    {
        SwitchScene(SceneName.P_Girl_room);
        SLManager.Instance.InitSaveData(true);
    }

    public void LoadGame()
    {
        SwitchScene(SceneName.P_Girl_room);
        SLManager.Instance.Load(1);
    }
    public void SwitchScene(SceneName sceneName)
    {
        SceneInfo sceneInfo = FindSceneInfo(sceneName);
        Debug.Assert(sceneInfo != null, "SceneName : " + sceneName.ToString() + " is not Exist in this game");
        beforeScene = currentScene;
        currentScene = sceneName;
        SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
        setSceneOptions(sceneInfo);
    }
}

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
        SampleScene = 1,
        AnotherScene = 2,
        Girl_room = 3,
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
        if (sceneInfo.isCharacterExist)
        {
            // 캐릭터가 존재 X, 생성해줌
            if (!SceneObjManager.Instance.getPlayerExist())
            {
                // SceneObjManager에서 PlayerObj가 없으면 생성해줌
                GameObject prefab = Resources.Load("Moving Character") as GameObject;
                GameObject movingCharacter = MonoBehaviour.Instantiate(prefab) as GameObject;
                movingCharacter.name = "Moving Character";
                // SceneManager에 넣어줌
                SceneObjManager.Instance.AddObject(ObjectType.PlayerObject, movingCharacter);
            }
        }
    }
    public void NewGameButton()
    {
        SwitchScene(SceneName.SampleScene);
    }
    public void SwitchScene(SceneName sceneName)
    {
        SceneInfo sceneInfo = FindSceneInfo(sceneName);
        if (sceneInfo == null)
            Debug.Assert(false, "SceneName : " + sceneName.ToString() + " is not Exist in this game");
        SceneManager.LoadScene(sceneName.ToString());
        setSceneOptions(sceneInfo);
        Debug.Log("SwitchScene : " + sceneName.ToString());
        currentScene = sceneName;
        return;
    }
}

﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EditorHelper;
using InGameObjects.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneSwitchManager : Singleton<SceneSwitchManager>
    {
        public enum SceneName
        {
            StartScene = 0,
            Girl_room = 1,
            W1_E2 = 2,
            W1_Hall = 3,
            W1_E1 = 4,
            W1_C = 5,
            W1_R2 = 6,
        }
        [Serializable]
        public class SceneInfo
        {
            public SceneName sceneName;
            public string sceneString;
            public bool isProfileActivate;
            public bool isCharacterExist;
            public bool isMovable;
            public bool isInputAvaliable;
            public int CameraMode;
        }
        
        [SerializeField] public SceneName currentScene;
        [SerializeField] public SceneName beforeScene;
#if UNITY_EDITOR
        [ArrayElementTitle("sceneString")]
#endif
        [SerializeField] public List<SceneInfo> sceneInfoList;
        public SceneInfo CurrentSceneInfo => FindSceneInfo(currentScene);
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
            // 플레이어 캐릭터 생성
            SceneObjManager.Instance.PlayerObjectExist = sceneInfo.isCharacterExist;
            // 플레이어 프로필 On / Off
            PlayerManager.Instance.SetProfileShowing(sceneInfo.isProfileActivate);
        }

        public void returnToOriginalSceneSetting()
        {
            setSceneOptions(FindSceneInfo(currentScene));
        }
        public async UniTask SwitchScene(SceneName sceneName, int idx)
        {
            SceneInfo sceneInfo = FindSceneInfo(sceneName);
            Debug.Assert(sceneInfo != null, "SceneName : " + sceneName.ToString() + " is not Exist in this game");
            beforeScene = currentScene;
            currentScene = sceneName;
                
            PlayerManager.Instance.interactingPlayer?.ClearScriptableObjList();
            await SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
            setSceneOptions(sceneInfo);

            await UniTask.WaitUntil(() => SceneObjManager.Instance.PlayerObjectExist);
            
            var sceneMovePoints = GameObject.FindGameObjectsWithTag("SceneMovePoint");
            Debug.Assert(sceneMovePoints.Length != 0, "sceneMovePoints.Length is 0");
            foreach (var sceneMovePointobj in sceneMovePoints)
            {
                if (sceneMovePointobj.GetComponent<SceneMovePoint>().sourceSceneName == beforeScene && sceneMovePointobj.GetComponent<SceneMovePoint>().idx == idx)
                {
                    PlayerManager.Instance.moveablePlayerObject.transform.localPosition = sceneMovePointobj.gameObject.transform.localPosition;
                    return;
                }
            }
        }
    }
}
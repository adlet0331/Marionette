using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEngine;

namespace Managers
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
    public class SceneInfo : DataType
    {
        public SceneName SceneName;
        public string sceneString
        {
            get => SceneName.ToString();
            set => SceneName = (SceneName)Enum.Parse(typeof(SceneName), value);
        }
        public bool isProfileActivate;
        public bool isCharacterExist;
        public bool isMovable;
        public bool isInputAvaliable;
        public int CameraMode;
    }
    [Serializable]
    public class SceneSwitchManager : AGameManager
    {
        [SerializeField] private SceneName currentSceneName;
        [SerializeField] public List<SceneInfo> sceneInfoList;
        public SceneInfo CurrentSceneInfo => FindSceneInfo(currentSceneName);
        public SceneName SetCurrentSceneNameAndReturnBeforeSceneName(SceneName newSceneName)
        {
            var beforeSceneName = currentSceneName;
            currentSceneName = newSceneName;

            return beforeSceneName;
        }
        public SceneInfo FindSceneInfo(SceneName sceneName)
        {
            foreach (SceneInfo sceneInfo in sceneInfoList)
            {
                if (sceneInfo.SceneName == sceneName)
                {
                    return sceneInfo;
                }
            }
            return null;
        }
        public override void Start()
        {
            currentSceneName = SceneName.StartScene;
            sceneInfoList = GamePlayManager.Instance.dataBaseCollection.sceneInfoDataBase.dataList;
        }
    }
}

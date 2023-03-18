using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using InGameObjects.Interaction;
using InGameObjects.Scene;
using InGameObjects.SceneObject.Player;
using UI;
using UnityEngine;
using UnityEngine.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GamePlayManager : Singleton<GamePlayManager>
    {
        /// <summary>
        /// Handle Scriptable Object Database
        /// </summary>
        public DataBaseCollection dataBaseCollection;
        
        /// <summary>
        /// Handle Game Save & Load
        /// </summary>
        [SerializeField] private SLManager slManager;
        public Vector3 PlayerStartVector => slManager.PlayerPosVec;
        public SaveInfo GetSaveDataInfo(int index) => slManager.GetSaveDataInfo(index);

        /// <summary>
        /// Handle Game's UI
        /// </summary>
        [SerializeField] private WindowManager windowManager;
        public WindowsInstances WindowsInstances => windowManager.WindowsInstances;
        public string CurrentOpenWindowType => windowManager.CurrentOpenWindowType;
        public void SetCurrentWindow(WindowObject windowObject) => windowManager.SetCurrentWindow(windowObject);
        public void RemoveWindow(WindowObject windowObject) => windowManager.RemoveWindow(windowObject);

        /// <summary>
        /// Handle Player's Input
        /// </summary>
        [SerializeField] private InputManager inputManager;
        public void SetPlayerControlComponent(PlayerWithHandLight playerComponent) => inputManager.SetPlayerControlComponent(playerComponent);
        public void SetInputOptions(bool movable, bool inputable) => inputManager.SetInputOptions(movable, inputable);
        public async UniTask PlayerInteraction() => inputManager.PlayerInteraction();

        /// <summary>
        /// Handle Camera
        /// </summary>
        [SerializeField] private CameraManager cameraManager;
        public Vector2 MouseCursorWorldPosition => cameraManager.MouseCursorWorldPosition;
        public async UniTask CameraMoveTargetAsync(Vector3 startVector3, Vector3 destinationVector3, float totalTimeMiliSecond, float timeIntervalSecond) =>
                cameraManager.CameraMoveTargetAsync(startVector3, destinationVector3, totalTimeMiliSecond, timeIntervalSecond);
        public void SetMapCameraBorder(GameObject map) => cameraManager.SetMapCameraBorder(map);
        public void ChangeCameraMode(int cameraMode) => cameraManager.SetCameraMode(cameraMode);

        /// <summary>
        /// Handle Scene Switching
        /// </summary>
        [SerializeField] private SceneSwitchManager sceneSwitchManager;
        public SceneInfo CurrentSceneInfo => sceneSwitchManager.CurrentSceneInfo;
        public SceneInfo GetSceneInfoWithName(SceneName sceneName) => sceneSwitchManager.FindSceneInfo(sceneName);

        /// <summary>
        /// Manage Player's Character
        /// </summary>
        [SerializeField] private CharacterManager characterManager;
        public Vector3 MoveablePlayerObjectPosition => characterManager.MoveablePlayerObjectPosition;
        public void MoveCharacterPosition(Vector3 position) => characterManager.MoveCharacterPosition(position);
        public InteractingObject CurrentInteractObj => characterManager.CurrentInteractObj;
        public void BlockInteract() => characterManager.BlockInteract();
        public void UnBlockInteract() => characterManager.UnBlockInteract();
        public void AddInteractingObject(InteractingObject interactingObject) => characterManager.AddInteractingObject(interactingObject);
        public void RemoveInteractionObject(InteractingObject interactionObject) => characterManager.RemoveInteractionObject(interactionObject);
        public void ClearInteractingObjList() => characterManager.ClearInteractingObjList();
        public void AddStress(float val) => characterManager.AddStress(val);

        /// <summary>
        /// Manage Player's Stella Ability
        /// </summary>
        [SerializeField] private StellaManager stellaManager;
        public void IncrementStella(int idx, int count) => stellaManager.IncrementStella(idx, count);
        public Dictionary<int, StellaInfo> StellaInfoDictionary => stellaManager.StellaInfoDictionary;

        /// <summary>
        /// Manage Player's Inventory
        /// </summary>
        [SerializeField] private InventoryManager inventoryManager;
        public List<ItemData> InventoryItemList => inventoryManager.InventoryItemList;
        public bool CheckItemInInventory(int idx) => inventoryManager.CheckItemInInventory(idx);
        public int GetItemNumInInventory(int itemIdx) => inventoryManager.GetItemNumInInventory(itemIdx);
        public ItemData GetItemDataWithIdx(int inventoryIdx) => inventoryManager.GetItemDataWithIdx(inventoryIdx);
        public void AddItem(int itemIdx, int num) => inventoryManager.AddItem(itemIdx, num);
        public bool DeleteItem(int itemIdx, int num) => inventoryManager.DeleteItem(itemIdx, num);
        
        /// <summary>
        /// Manage Scene's Object's Status. Apply DontDestroyOnLoad
        /// Canvas, EventSystem, Player, Interacting Object 
        /// </summary>
        [SerializeField] private SceneObjManager sceneObjManager;
        public UDictionary<int, InteractionStatus> CurrentInteractingObjectStatusDict => sceneObjManager.InteractingObjectStatusDictionary;
        public void InteractionStatusChangedNotify(bool activate, int idx) => sceneObjManager.InteractionStatusChangedNotify(activate, idx);

        
        
        
        // Functions Which Use Multiple AGameManagers
        public void InitNewData()
        {
            var gameStateDatas = new SaveData()
            {
                sceneString = sceneSwitchManager.CurrentSceneInfo.sceneString,
                sceneInfo = sceneSwitchManager.CurrentSceneInfo,
                playerLocalPosX = characterManager.MoveablePlayerObjectPosition.x,
                playerLocalPosY = characterManager.MoveablePlayerObjectPosition.y,
                timePassed = 0, // TODO 
                setting = "TODO", // TODO
                itemList = inventoryManager.InitInventoryList(),
                stellaInfoDictionary = stellaManager.InitStellaInfoList(),
                interactingObjectStatusDictionary = sceneObjManager.InitInteractionObjectStatusDictionary()
            };
            slManager.InitSaveData(gameStateDatas);
        }

        public void SaveCurrentData(int index)
        {
            var saveData = new SaveData()
            {
                sceneString = CurrentSceneInfo.sceneString,
                sceneInfo = CurrentSceneInfo,
                playerLocalPosX = characterManager.MoveablePlayerObjectPosition.x,
                playerLocalPosY = characterManager.MoveablePlayerObjectPosition.y,
                timePassed = 0, // TODO 
                setting = "TODO", // TODO
                itemList = inventoryManager.InventoryItemList,
                stellaInfoDictionary = stellaManager.StellaInfoDictionary,
                interactingObjectStatusDictionary = new Dictionary<int, InteractionStatus>(sceneObjManager.InteractingObjectStatusDictionary)
            };
            slManager.SaveData(index, saveData);
        }

        public void LoadSavedData(int index)
        {
            // Struct Deep Copy
            var loadedData = slManager.LoadSavedData(index);

            stellaManager.LoadStellaInfoDictionary(loadedData.stellaInfoDictionary);
            inventoryManager.LoadInventoryList(loadedData.itemList);
            sceneObjManager.LoadInteractingObjectStatusDictionary(loadedData.interactingObjectStatusDictionary);
            
            SwitchScene(loadedData.sceneInfo.SceneName, -1).Forget();
        }

        public void SetGameCamera(Camera camera)
        {
            cameraManager.SetGameCamera(camera);
            sceneObjManager.SetCanvasCamera(camera);
        }

        public void LoadPlayerAttachedMainCharacter(GameObject MainCharacter)
        {
            cameraManager.SetFollowingObject(MainCharacter);
            characterManager.SetCharacterObject(MainCharacter);
        }
        
        public async UniTask SwitchScene(SceneName sceneName, int movePointIdx)
        {
            SceneInfo sceneInfo = sceneSwitchManager.FindSceneInfo(sceneName);
            Debug.Assert(sceneInfo != null, "SceneName : " + sceneName.ToString() + " is not Exist in this game");
            var beforeSceneName = sceneSwitchManager.SetCurrentSceneNameAndReturnBeforeSceneName(sceneName);

            await SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);

            await ApplySceneSetting(sceneSwitchManager.FindSceneInfo(sceneName));
            characterManager.ClearInteractingObjList();
            
            var sceneMovePoints = GameObject.FindGameObjectsWithTag("SceneMovePoint");
            var findSceneMovePointSuccess = false;
            foreach (var sceneMovePointobj in sceneMovePoints)
            {
                if (sceneMovePointobj.GetComponent<SceneMovePoint>().sourceSceneName == beforeSceneName && 
                    sceneMovePointobj.GetComponent<SceneMovePoint>().idx == movePointIdx)
                {
                    findSceneMovePointSuccess = true;
                    characterManager.MoveCharacterPosition(sceneMovePointobj.gameObject.transform.localPosition);
                    break;
                }
            }

            // -1인 경우, LoadScene으로 태어난 경우
            if (!findSceneMovePointSuccess)
            {
                characterManager.MoveCharacterPosition(slManager.PlayerPosVec);
            }
            
        }

        public async UniTask ApplySceneSetting(SceneInfo sceneInfo = null)
        {
            if (sceneInfo == null) sceneInfo = sceneSwitchManager.CurrentSceneInfo;
            
            // This Spawn Player's Character
            sceneObjManager.PlayerObjectExist = true;
            inputManager.SetInputOptions(sceneInfo.isMovable, sceneInfo.isInputAvaliable);
            cameraManager.SetCameraMode(sceneInfo.CameraMode);
            windowManager.WindowsInstances.profileWindow.gameObject.SetActive(sceneInfo.isProfileActivate);
            await UniTask.WaitUntil(() => sceneObjManager.PlayerObjectExist);
        }
        
        private void Start()
        {
            dataBaseCollection.LoadAllDataBase();
            
            slManager.Start();
            windowManager.Start();
            inputManager.Start();
            cameraManager.Start();
            sceneSwitchManager.Start();
            characterManager.Start();
            stellaManager.Start();
            inventoryManager.Start();
            sceneObjManager.Start();
        }

        private void FixedUpdate()
        {
            inputManager.HandlePlayerMoveInput();
            cameraManager.UpdateCameraPosition();
        }

        private void Update()
        {
            inputManager.HandlePlayerInteractInput();
        }
    }
}
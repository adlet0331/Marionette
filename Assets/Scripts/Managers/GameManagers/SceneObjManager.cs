using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Managers
{
    [Serializable]
    public struct InteractionStatus
    {
        public bool CurrentStatus;
    }
    [Serializable]
    public class SceneObjManager : AGameManager
    {
        [SerializeField] private Canvas canvas;
        public void SetCanvasCamera(Camera camera)
        {
            canvas.worldCamera = camera;
        }
        [SerializeField] private EventSystem eventSystem;
        
        [SerializeField] private bool playerExist;
        [SerializeField] private GameObject playerObj;
        [SerializeField] private Dictionary<int, InteractionStatus> interactingObjectStatusDictionary;
        public Dictionary<int, InteractionStatus> InteractingObjectStatusDictionary => new Dictionary<int, InteractionStatus>(interactingObjectStatusDictionary);

        public Dictionary<int, InteractionStatus> InitInteractionObjectStatusDictionary()
        {
            interactingObjectStatusDictionary = 
                    GamePlayManager.Instance.dataBaseCollection.interactionDataBase.dataList.ToDictionary(
                        keySelector: interactionData => interactionData.idx, 
                        elementSelector: interactionData => new InteractionStatus()
                        {
                            CurrentStatus = interactionData.initStatus,
                        }
                    );
            return new Dictionary<int, InteractionStatus>(interactingObjectStatusDictionary);
        }

        public void LoadInteractingObjectStatusDictionary(Dictionary<int, InteractionStatus> dictionary)
        {
            interactingObjectStatusDictionary = dictionary;
        }
        
        public void InteractionStatusChangedNotify(bool activate, int idx)
        {
            interactingObjectStatusDictionary[idx] = new InteractionStatus()
            {
                CurrentStatus = activate,
            };
        }
        
        public bool PlayerObjectExist 
        {
            get 
            {
                return playerExist;
            }
            set 
            {
                if (!value) 
                {
                    Object.Destroy(playerObj);
                    playerExist = false;
                }
                else 
                {
                    if (!playerExist)
                    {
                        // Spawn Player Prefab
                        GameObject prefab = Resources.Load("Prefabs/Girl") as GameObject;
                        GameObject movingCharacter = MonoBehaviour.Instantiate(prefab) as GameObject;
                        movingCharacter.name = "Moving Character";
                        playerObj = movingCharacter;
                        Object.DontDestroyOnLoad(playerObj);
                    }
                    playerExist = true;
                }

            }
        }
        public override void Start()
        {
            // 시작 씬
            playerExist = false;

            var canvasObject = GameObject.Find("Canvas");
            canvas = canvasObject.GetComponent<Canvas>();
            Object.DontDestroyOnLoad(canvasObject);
            
            var eventSystemObject = GameObject.Find("EventSystem");
            eventSystem = eventSystemObject.GetComponent<EventSystem>();
            Object.DontDestroyOnLoad(eventSystemObject);
        }
    }
}
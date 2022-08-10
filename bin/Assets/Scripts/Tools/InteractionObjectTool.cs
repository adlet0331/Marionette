using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using DataBaseScripts.Base;
using InGameObjects.Interaction;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class InteractionObjectTool : ScriptableWizard
    {
        [Serializable]
        public enum InteractionObjectType
        {
            InteractionObject = 0,
            ColliderObject = 1,
            InteractingObject = 2
        }

        public Dictionary<int, string> interactionObjectPrefabNameDictionary = new Dictionary<int, string>()
        {
            {0, "InteractionObject"},
            {1, "ColliderObject"},
            {2, "InteractingObject"}
        };

        public Dictionary<int, string> typeNameDictionary = new Dictionary<int, string>()
        {
            { 2, "2.ObjectControl" },
            { 3, "3.Script" },
            { 4, "4.Choose" },
            { 5, "5.SceneMove" },
            { 6, "6.Animation" },
            { 7, "7.CameraWalk" },
            { 8, "8.ItemControl" },
            { 9, "9.Stress" },
            { 10, "10.Lock" },
        };

        public ScriptDataBase ScriptDataBase;
        public ChooseDataBase ChooseDataBase;
        public MoveControlDataBase MoveControlDataBase;
        public CameraControlDataBase CameraControlDataBase;
        public ItemControlDataBase ItemControlDataBase;
        public StressControlDataBase StressControlDataBase;
        public LockDataBase LockDataBase;
        public InteractionDataBase InteractionDataBase;

        public int idx = 0;
        public InteractionObjectType type;
        
        // For Showing
        [SerializeField] private string interactionObjectName;
        [SerializeField] private List<string> ControlObjectNameList;

        [MenuItem("Marionette/CreateInteractionObject")]
        static void Open()
        {
            DisplayWizard<InteractionObjectTool>("Create InteractionObject", "Create", "Get DataName");
        }

        private string prefabPath(string name)
        {
            return Path.Combine("Prefabs", "InteractObject", name);
        }
        private string getInteractingObjectName(int typeInt, int index)
        {
            // 대사 Script
            if (typeInt == 3)
            {
                return ScriptDataBase.dataList[index].name;
            }
            // 선택지 Choose
            else if (typeInt == 4)
            {
                return ChooseDataBase.dataList[index].name;
            }
            // 이동 (씬 이동, 씬 내 이동)
            else if (typeInt == 5)
            {
                return MoveControlDataBase.dataList[index].name;
            }
            else if (typeInt == 7)
            {
                return CameraControlDataBase.dataList[index].name;
            }
            // ItemControl
            else if (typeInt == 8)
            {
                return ItemControlDataBase.dataList[index].name;
            }
            // Stress Control
            else if (typeInt == 9)
            {
                return StressControlDataBase.dataList[index].name;
            }
            // 잠김 Lock
            else if (typeInt == 10)
            {
                return LockDataBase.dataList[index].name;
            }
            else
            {
                return null;
            }
        }
        private void setInteractingObject(GameObject gameObject, int typeInt, int index)
        {
            InteractionData interactionData = InteractionDataBase.dataList[idx];
            // 대사 Script
            if (typeInt == 3)
            {
                gameObject.GetComponent<ScriptControl>().data = ScriptDataBase.dataList[index];
            }
            // 선택지 Choose
            else if (typeInt == 4)
            {
                gameObject.GetComponent<ChooseControl>().data = ChooseDataBase.dataList[index];
            }
            // 이동 (씬 이동, 씬 내 이동)
            else if (typeInt == 5)
            {
                gameObject.GetComponent<MoveControl>().data = MoveControlDataBase.dataList[index];
            }
            else if (typeInt == 7)
            {
                gameObject.GetComponent<CameraControl>().data = CameraControlDataBase.dataList[index];
            }
            // ItemControl
            else if (typeInt == 8)
            {
                gameObject.GetComponent<ItemControl>().data = ItemControlDataBase.dataList[index];
            }
            // Stress Control
            else if (typeInt == 9)
            {
                gameObject.GetComponent<StressControl>().data = StressControlDataBase.dataList[index];
            }
            // 잠김 Lock
            else if (typeInt == 10)
            {
                gameObject.GetComponent<LockControl>().data = LockDataBase.dataList[index];
            }
        }

        private void initializeDataBase()
        {
            ScriptDataBase = Resources.Load(Path.Combine("DataBase", "3_ScriptDataBase"), typeof(ScriptDataBase)) as ScriptDataBase;
            ChooseDataBase = Resources.Load(Path.Combine("DataBase", "4_ChooseDataBase"), typeof(ChooseDataBase)) as ChooseDataBase;
            MoveControlDataBase = Resources.Load(Path.Combine("DataBase", "5_MoveControlDataBase"), typeof(MoveControlDataBase)) as MoveControlDataBase;
            CameraControlDataBase = Resources.Load(Path.Combine("DataBase", "7_CameraControlDataBase"), typeof(CameraControlDataBase)) as CameraControlDataBase;
            ItemControlDataBase = Resources.Load(Path.Combine("DataBase", "8_ItemControlDataBase"), typeof(ItemControlDataBase)) as ItemControlDataBase;
            StressControlDataBase = Resources.Load(Path.Combine("DataBase", "9_StressControlDataBase"), typeof(StressControlDataBase)) as StressControlDataBase;
            LockDataBase = Resources.Load(Path.Combine("DataBase", "10_LockDataBase"), typeof(LockDataBase)) as LockDataBase;
            InteractionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
        }

        private void OnWizardOtherButton()
        {
            initializeDataBase();
            InteractionData interactionData = InteractionDataBase.dataList[idx];
            
            ControlObjectNameList = new List<string>();
            ControlObjectNameList.Clear();
            interactionObjectName = interactionData.name;
            for (int i = 0; i < interactionData.typeList.Count; i++)
            {
                int type = interactionData.typeList[i];
                int index = interactionData.idxList[i];

                string name = type + "." + (getInteractingObjectName(type, index) != null ? getInteractingObjectName(type, index) : interactionData.name) + "_" + index;
                ControlObjectNameList.Add(name);
            }
        }

        private GameObject CreateObject(InteractionObjectType interactionObjectType, int interactionIdx)
        {
            Transform interactionGroupsTransform = GameObject.FindGameObjectWithTag("GroupInteraction")?.GetComponent<Transform>();
            if (!interactionGroupsTransform)
            {
                var groupInteractionObj = new GameObject("GroupInteraction");
                groupInteractionObj.tag = "GroupInteraction";
                interactionGroupsTransform = GameObject.FindGameObjectWithTag("GroupInteraction")?.GetComponent<Transform>();
            }
            
            string interactingObjectName = interactionObjectType.ToString();
            GameObject interactingObject = Instantiate(Resources.Load<GameObject>(prefabPath(interactingObjectName)), interactionGroupsTransform, true);

            InteractionData interactionData = InteractionDataBase.dataList[interactionIdx];
            
            interactingObject.name = interactionData.name;

            InteractingObject interactionObjectScript = interactingObject.GetComponent<InteractingObject>();
            interactionObjectScript.Initiate(interactionIdx, interactionData.typeList, interactionData.goNextImmediately, interactionData.disableAfterInteract);

            GameObject prefabObject;
            for (int i = 0; i < interactionData.typeList.Count; i++)
            {
                int type = interactionData.typeList[i];
                int index = interactionData.idxList[i];

                prefabObject = Instantiate(Resources.Load<GameObject>(prefabPath(typeNameDictionary[type])), 
                    interactingObject.transform, true);
                prefabObject.name = type + "." + (getInteractingObjectName(type, index) != null ? getInteractingObjectName(type, index) : interactionData.name) + "_" + index;
                
                setInteractingObject(prefabObject, type, index);
                interactionObjectScript.AddInteractingObject(prefabObject);
                
                // 선택지
                if (type == 4)
                {
                    List<int> interactIdxList = ChooseDataBase.dataList[index].interactionList;
                    prefabObject.GetComponent<ChooseControl>().data.interactionGameObjectList = new List<InteractingObject>();
                    for (int j = 0; j < interactIdxList.Count; j++)
                    {
                        GameObject selectObject = CreateObject(InteractionObjectType.InteractingObject, interactIdxList[j]);
                        selectObject.name = "선택지_" + j + "_" + selectObject.name;
                        selectObject.transform.SetParent(prefabObject.transform);
                        prefabObject.GetComponent<ChooseControl>().data.interactionGameObjectList.Add(selectObject.GetComponent<InteractingObject>());
                    }
                }
                // 카메라 워크
                if (type == 7 && CameraControlDataBase.dataList[index].type == CameraWalkType.CameraWalk)
                {
                    GameObject startGameObject = new GameObject("카메라워크_시작지점_" + index);
                    GameObject endGameObject = new GameObject("카메라워크_엔드지점_" + index);
                    prefabObject.GetComponent<CameraControl>().data.startPoint = startGameObject.transform;
                    prefabObject.GetComponent<CameraControl>().data.endPoint = endGameObject.transform;
                }
            }

            return interactingObject;
        }

        private void OnWizardCreate()
        {
            initializeDataBase();

            OnWizardOtherButton();
            
            CreateObject(this.type, this.idx);
        }
    }
}
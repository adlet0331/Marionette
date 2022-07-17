﻿using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using InGameObjects.Interaction;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class InteractionObjectTool : ScriptableWizard
    {
        [Serializable]
        public enum InteractionObjectType
        {
            InteractionObject = 0,
            ColliderObject = 1
        }

        public Dictionary<int, string> interactionObjectPrefabNameDictionary = new Dictionary<int, string>()
        {
            {0, "InteractionObject"},
            {1, "ColliderObject"}
        };

        public Dictionary<int, string> typeNameDictionary = new Dictionary<int, string>()
        {
            { 2, "2.ObjectControl" },
            { 3, "3.Script" },
            { 4, "4.Choose" },
            { 8, "8.ItemControl" },
            { 9, "9.Stress" },
            { 10, "10.Lock" },
        };
        
        public InteractionDataBase InteractionDataBase;
        public ScriptDataBase ScriptDataBase;
        public ChooseDataBase ChooseDataBase;
        public ItemControlDataBase ItemControlDataBase;
        public StressControlDataBase StressControlDataBase;
        public LockDataBase LockDataBase;
        
        public int idx = 0;
        public InteractionObjectType type;
        [SerializeField] private string interactionObjectName;
        
        [MenuItem("CustomTools/CreateInteractionObject")]
        static void Open()
        {
            DisplayWizard<InteractionObjectTool>("Create InteractionObject", "Create", "Get DataName");
        }

        private string prefabPath(string name)
        {
            return Path.Combine("Prefabs", "InteractObject", name);
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
                gameObject.GetComponent<LockObject>().data = LockDataBase.dataList[index];
            }
        }

        private void initializeDataBase()
        {
            InteractionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
            ScriptDataBase = Resources.Load(Path.Combine("DataBase", "3_ScriptDataBase"), typeof(ScriptDataBase)) as ScriptDataBase;
            ChooseDataBase = Resources.Load(Path.Combine("DataBase", "4_ChooseDataBase"), typeof(ChooseDataBase)) as ChooseDataBase;
            ItemControlDataBase = Resources.Load(Path.Combine("DataBase", "8_ItemControlDataBase"), typeof(ItemControlDataBase)) as ItemControlDataBase;
            StressControlDataBase = Resources.Load(Path.Combine("DataBase", "9_StressControlDataBase"), typeof(StressControlDataBase)) as StressControlDataBase;
            LockDataBase = Resources.Load(Path.Combine("DataBase", "10_LockDataBase"), typeof(LockDataBase)) as LockDataBase;
        }

        private void OnWizardOtherButton()
        {
            initializeDataBase();
            InteractionData interactionData = InteractionDataBase.dataList[idx];

            interactionObjectName = interactionData.name;
        }

        private void OnWizardCreate()
        {
            initializeDataBase();
            
            string interactingObjectName = type.ToString();
            GameObject interactingObject = Instantiate(Resources.Load<GameObject>(prefabPath(interactingObjectName)));

            InteractionData interactionData = InteractionDataBase.dataList[idx];
            
            interactingObject.name = interactionData.name;
            
            InteractingObject interactionObjectScript = interactingObject.GetComponent<InteractingObject>();
            interactionObjectScript.Initiate(idx, interactionData.typeList, interactionData.idxList);

            GameObject prefabObject;
            for (int i = 0; i < interactionData.typeList.Count; i++)
            {
                int type = interactionData.typeList[i];
                int index = interactionData.idxList[i];
                prefabObject = Instantiate(Resources.Load<GameObject>(prefabPath(typeNameDictionary[type])), interactingObject.transform, true);

                prefabObject.name = type + "." + interactionData.name + "_" + interactionData.idx;
                setInteractingObject(prefabObject, type, index);
                interactionObjectScript.AddInteractingObject(prefabObject); 
            }
        }
    }
}
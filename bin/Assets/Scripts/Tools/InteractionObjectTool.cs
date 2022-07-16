using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using InGameObjects.Interaction;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class InteractionObjectTool : ScriptableWizard
    {
        public InteractionDataBase InteractionDataBase;
        public int idx = 0;

        [SerializeField] private Dictionary<int, string> typeNameDictionary = new Dictionary<int, string>()
        {
            { 2, "2.ObjectControl" },
            { 3, "3.Script" },
            { 4, "4.Choose" },
            { 8, "8.ItemControl" },
            { 9, "9.Stress" },
            { 10, "10.Lock" },
        };

        [MenuItem("CustomTools/CreateInteractionObject")]
        static void Open()
        {
            DisplayWizard<InteractionObjectTool>("Create InteractionObject", "Create", "SetDataBase");
        }

        private string prefabPath(string name)
        {
            return Path.Combine("Prefabs", "InteractObject", name);
        }

        private void OnWizardOtherButton()
        {
            InteractionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
        }

        private void OnWizardCreate()
        {
            //GameObject interactionGameObject = new GameObject("InteractionObject_" + idx);
            GameObject interactionGameObject = Instantiate(Resources.Load<GameObject>(prefabPath("InteractionObject")));
            interactionGameObject.name = "InteractionObject_" + idx;

            InteractionData interactionData = InteractionDataBase.dataList[idx];
            
            InteractionObject interactionObjectScript = interactionGameObject.GetComponent<InteractionObject>();
            interactionObjectScript.Initiate(idx, interactionData.typeList, interactionData.idxList);

            GameObject prefabObject;
            for (int i = 0; i < interactionData.typeList.Count; i++)
            {
                int type = interactionData.typeList[i];
                prefabObject = Instantiate(Resources.Load<GameObject>(prefabPath(typeNameDictionary[type])));

                prefabObject.name = interactionData.name;
                prefabObject.transform.SetParent(interactionGameObject.transform);
            }
        }
    }
}
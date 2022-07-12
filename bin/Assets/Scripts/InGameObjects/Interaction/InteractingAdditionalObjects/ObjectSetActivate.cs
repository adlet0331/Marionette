using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class GameObjectSetActivate
    {
        public GameObject gameObject;
        public bool isActivating;
    }
    [Serializable]
    public class ObjectSetActivate : AbstractInteractionObject
    {
        [SerializeField] private List<GameObjectSetActivate> dataList;
        public override void Interact()
        {
            foreach (var gameObjectCreateDelete in dataList)
            {
                gameObjectCreateDelete.gameObject.SetActive(gameObjectCreateDelete.isActivating);
            }
        }
    }
}
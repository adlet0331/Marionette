using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
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
    public class GameObjectSetActiveList : DataType
    {
        public List<GameObjectSetActivate> gameObjectSetActivateList;
    }
    [Serializable]
    public class ObjectSetActivate : IInteractionObject<GameObjectSetActiveList>
    {
        public override void Interact()
        {
            foreach (var gameObjectCreateDelete in data.gameObjectSetActivateList)
            {
                gameObjectCreateDelete.gameObject.SetActive(gameObjectCreateDelete.isActivating);
            }
        }
    }
}
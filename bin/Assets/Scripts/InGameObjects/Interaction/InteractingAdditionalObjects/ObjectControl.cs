﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    public class GameObjectSetActiveList
    {
        public List<GameObjectSetActivate> gameObjectSetActivateList;
    }
    [Serializable]
    public class ObjectControl : IInteractionObject<GameObjectSetActiveList>
    {
        public override async UniTask<bool> Interact()
        {
            foreach (var gameObjectCreateDelete in data.gameObjectSetActivateList)
            {
                gameObjectCreateDelete.gameObject.SetActive(gameObjectCreateDelete.isActivating);
            }

            return true;
        }
    }
}
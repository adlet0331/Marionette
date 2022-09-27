using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class GameObjectSetActivate
    {
        public InteractingObject interactingObject;
        public bool isActivating;
    }

    [Serializable]
    public class GameObjectSetActiveList
    {
        public List<GameObjectSetActivate> gameObjectSetActivateList;
    }
    [Serializable]
    public class ObjectControl : ADataInteractionObject<GameObjectSetActiveList>
    {
        public override async UniTask<bool> Interact()
        {
            foreach (var gameObjectCreateDelete in data.gameObjectSetActivateList)
            {
                gameObjectCreateDelete.interactingObject.SetActiveNotify(gameObjectCreateDelete.isActivating);
            }

            return true;
        }
    }
}
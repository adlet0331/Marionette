using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectCreateDelete
{
    public GameObject gameObject;
    public bool isActivating;
}
[Serializable]
public class ObjectCreateDelete : AbstractInteractionObject
{
    [SerializeField] private List<GameObjectCreateDelete> dataList;
    public override void Interact()
    {
        foreach (var gameObjectCreateDelete in dataList)
        {
            gameObjectCreateDelete.gameObject.SetActive(gameObjectCreateDelete.isActivating);
        }
    }
}
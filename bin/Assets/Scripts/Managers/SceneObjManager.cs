using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.InGameObjects.DontDestroyObject;
using static SceneStartPoint;

public class SceneObjManager : Singleton<SceneObjManager>
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private bool playerExist;
    [SerializeField] private GameObject playerObj;
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
                Destroy(playerObj);
                playerExist = false;
            }
            else 
            {
                if (!playerExist)
                {
                    createPlayerObject();
                }
                playerExist = true;
            }

        }
    }
    
    public void UpdatePlayerPos(Vector3 pos)
    {
        playerObj.transform.position = pos;
        return;
    }
    public void AddObject(ObjectType type, GameObject gameObject)
    {
        if (type == ObjectType.CanvasObject)
        {
            canvasObj = gameObject;
            DontDestroyOnLoad(canvasObj);
        }
        else if (type == ObjectType.PlayerObject)
        {
            playerObj = gameObject;
            DontDestroyOnLoad(playerObj);
        }
    }
    private void createPlayerObject()
    {
        // SceneObjManager에서 PlayerObj가 없으면 생성해줌
        GameObject prefab = Resources.Load("Prefabs/Moving Character Variant") as GameObject;
        GameObject movingCharacter = MonoBehaviour.Instantiate(prefab) as GameObject;
        movingCharacter.name = "Moving Character";
        // SceneManager에 넣어줌
        AddObject(ObjectType.PlayerObject, movingCharacter);
        return;
    }
    private void Start()
    {
        playerExist = false;
    }
}
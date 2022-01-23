using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.InGameObjects.DontDestroyObject;
using static SceneStartPoint;

public class SceneObjManager : Singleton<SceneObjManager>
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private List<SceneStartPoint> sceneStartPointList;
    public bool getPlayerExist()
    {
        return playerObj != null;
    }
    public void UpdatePlayerPos(Vector3 pos)
    {
        playerObj.transform.position = pos;
        return;
    }
    public void AddStartPoint(SceneStartPoint sceneStartPoint)
    {
        sceneStartPointList.Add(sceneStartPoint);
    }
    public void AddObject(ObjectType type, GameObject gameObject)
    {
        if (type == ObjectType.CanvasObject)
            canvasObj = gameObject;
        else if (type == ObjectType.PlayerObject)    
            playerObj = gameObject;
        this.Start();
    }
    private void Start()
    {
        if (canvasObj != null)
            DontDestroyOnLoad(canvasObj);
        if (playerObj != null)
            DontDestroyOnLoad(playerObj);
    }
}
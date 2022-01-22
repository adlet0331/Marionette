using UnityEngine;
using static Assets.Scripts.InGameObjects.DontDestroyObject;

public class SceneObjManager : Singleton<SceneObjManager>
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private GameObject playerObj;

    private void Start()    
    {
        if (canvasObj != null)
            DontDestroyOnLoad(canvasObj);
        if (playerObj != null)
            DontDestroyOnLoad(playerObj);
    }

    public void AddObject(ObjectType type, GameObject gameObject)
    {
        if (type == ObjectType.CanvasObject)
            canvasObj = gameObject;
        else if (type == ObjectType.PlayerObject)    
            playerObj = gameObject;
        this.Start();
    }

    public void UpdatePlayerPos(Vector3 pos)
    {
        playerObj.transform.position = pos;
    }
}
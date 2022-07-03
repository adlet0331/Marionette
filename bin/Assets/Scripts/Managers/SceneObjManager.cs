using UnityEngine;

public class SceneObjManager : Singleton<SceneObjManager>
{
    public GameObject canvas;
    public GameObject eventSystem;
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
    public void AddPlayerObject(GameObject gameObject)
    {
        playerObj = gameObject;
        DontDestroyOnLoad(playerObj);
    }
    private void createPlayerObject()
    {
        // SceneObjManager에서 PlayerObj가 없으면 생성해줌
        GameObject prefab = Resources.Load("Prefabs/Character") as GameObject;
        GameObject movingCharacter = MonoBehaviour.Instantiate(prefab) as GameObject;
        movingCharacter.name = "Moving Character";
        // SceneManager에 넣어줌
        AddPlayerObject(movingCharacter);
        return;
    }
    private void Start()
    {
        playerExist = false;
        if (canvas)
            DontDestroyOnLoad(canvas);
        if (eventSystem)
            DontDestroyOnLoad(eventSystem);
    }
}
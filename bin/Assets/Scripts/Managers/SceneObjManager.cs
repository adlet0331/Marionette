using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjManager : Singleton<SceneObjManager>
{
    [SerializeField] private List<GameObject> dontDestroyObj;

    private void Start()    
    {
        foreach(GameObject gameObject in dontDestroyObj)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddDontDestroyObj(GameObject gameObject)
    {
        dontDestroyObj.Add(gameObject);
        this.Start();
    }
}
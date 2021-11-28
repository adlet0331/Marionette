using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjManager : Singleton<SceneObjManager>
{
    [SerializeField] private List<GameObject> dontDestroyObj;

    private void Start()
    {
        Debug.Log("SceneObjManager Start Called");
        foreach(GameObject gameObject in dontDestroyObj)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
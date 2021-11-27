using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveObject : ColliderObject
{
    [SerializeField] private string sceneName;
    public override void InteractIn()
    {
        SceneManager.LoadScene(sceneName);
    }

    public override void InteractOut()
    {
        SceneManager.LoadScene(sceneName);
    }
}

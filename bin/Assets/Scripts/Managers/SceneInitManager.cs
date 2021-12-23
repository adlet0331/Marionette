using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitManager : Singleton<SceneInitManager>
{
    [SerializeField] private List<string> sceneCharacterableList;
    [SerializeField] private List<string> sceneNCharacterableList;
    [SerializeField] string currentScene;

    private void Start()
    {
        SwitchScene("StartScene");
    }

    public string GetScene()
    {
        return currentScene;
    }

    public void ChangeScene(string sceneName)
    {
        if (SwitchScene(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }

    }

    public bool SwitchScene(string sceneName)
    {
        bool exist = false;
        foreach (string str in sceneCharacterableList)
        {
            if (str == sceneName)
            {
                exist = true;
                InputManager.Instance.IsMoveable = true;
            }
        }
        foreach (string str in sceneNCharacterableList)
        {
            if (str == sceneName)
            {
                exist = true;
                InputManager.Instance.IsMoveable = false;
            }
        }
        return exist;
    }
}

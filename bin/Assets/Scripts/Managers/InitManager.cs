using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : Singleton<InitManager>
{
    [SerializeField] private List<string> sceneCharacterableList;
    [SerializeField] private List<string> sceneNCharacterableList;
}

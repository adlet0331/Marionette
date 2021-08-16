using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    #region Singleton
    private void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton

    [SerializeField] private GameObject[] mapCollection;

    public GameObject GetMap(int index) {
        return mapCollection[index];
    }
}

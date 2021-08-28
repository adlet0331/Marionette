using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject[] mapCollection;

    public GameObject GetMap(int index) {
        return mapCollection[index];
    }
}

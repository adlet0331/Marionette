using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Map의 GameObject를 List로 관리
 * 
 * Map Gameobject 내 정보를 public 함수로 전달
 * 
 */

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject[] mapCollection;

    public void UpdateMapCollection()
    {
        mapCollection = GameObject.FindGameObjectsWithTag("Map");
    }

    public GameObject GetMap(int index) {
        return mapCollection[index];
    }
}

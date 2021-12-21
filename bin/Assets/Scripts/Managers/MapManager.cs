using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Map�� GameObject�� List�� ����
 * 
 * Map Gameobject �� ������ public �Լ��� ����
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

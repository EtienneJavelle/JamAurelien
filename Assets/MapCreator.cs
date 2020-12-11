using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public Vector2Int mapSize;
    public GameObject floorPrefab;


    public void RefreshMap()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (Transform child in tempList)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Instantiate(floorPrefab, Tools.YZero(x, y), Quaternion.identity,this.transform);
            }
        }
        Debug.Log("RefreshMap");
    }
}

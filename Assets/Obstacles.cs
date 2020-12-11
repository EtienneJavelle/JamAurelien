using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Transform[] obstacles;
    [SerializeField]
    SelectableFloor[] selectableFloors;

    // Start is called before the first frame update
    void Start()
    {
        GameObject u = GameObject.Find("Team");
        selectableFloors = u.GetComponent<SquadControl>().selectableFloors;
        obstacles = this.gameObject.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform obstacle in obstacles)
        {
            SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(obstacle.position));
            under.canMove = false;
            under.blocked = true;
        }
    }
}

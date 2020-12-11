using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public SelectableFloor[] selectableFloors;
    public Transform[] ennemies;

    // Start is called before the first frame update
    void Start()
    {
        GameObject u = GameObject.Find("Team");
        selectableFloors = u.GetComponent<SquadControl>().selectableFloors;
        ennemies = this.gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform obstacle in ennemies)
            {
                SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(obstacle.position));
                under.canMove = false;
                under.blocked = true;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemies != null)
        {
        }
    }
}

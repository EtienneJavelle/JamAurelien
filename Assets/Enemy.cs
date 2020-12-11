using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private void Start()
    {
        SelectableFloor[] selectableFloors = GameObject.Find("Team").GetComponent<SquadControl>().selectableFloors;
        SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(this.transform.position));
        under.canMove = false;
        under.blocked = true;
        under.enemy = this;
    }
}

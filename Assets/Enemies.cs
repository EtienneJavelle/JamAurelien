using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public SelectableFloor[] selectableFloors;
    public Enemy[] enemies;
    internal bool enemyTurn;
    SquadMember[] squadMembers;
    int enemyturn = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject u = GameObject.Find("Team");
        squadMembers = u.GetComponent<SquadControl>().squadMembers;
        selectableFloors = u.GetComponent<SquadControl>().selectableFloors;
        enemies = this.gameObject.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(enemy.transform.position));
            under.canMove = false;
            under.blocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurn)
        {
            foreach (Enemy enemy in enemies)
            {
                Vector3 closest = Vector3.one * 1000;
                foreach (SquadMember squadMember in squadMembers)
                {
                    Vector3 tmp = Tools.YZero(squadMember.transform.position-enemy.transform.position);
                    if (tmp.x < closest.x && tmp.y < closest.z) closest = tmp;
                }

            }
        }
    }


    void MoveEnemy(int dist, Vector3 dir)
    {
        dir.y = .9f;
        enemies[enemyturn].transform.DOMove(dir, .4f, false);
    }
}

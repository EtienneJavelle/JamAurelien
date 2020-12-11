using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
            enemyturn = 0;
            foreach (Enemy enemy in enemies)
            {
                MoveEnemy(1, enemy.transform.position, enemy);
                enemyturn++;
            }
            Invoke("EndMove", .4f);
            enemyTurn = false;
            GameObject u = GameObject.Find("Team");
            u.GetComponent<SquadControl>().teamMemberTurn = 0;
        }
    }


    void MoveEnemy(int dist, Vector3 dir, Enemy enemy)
    {
        dir.y = .9f;
        dir.x += (int)UnityEngine.Random.Range(-1, 2);
        dir.z += (int)UnityEngine.Random.Range(-1, 2);
        dir.x = Mathf.Clamp(dir.x, 0, GameObject.Find("Map").GetComponent<MapCreator>().mapSize.x-1);
        dir.z = Mathf.Clamp(dir.z, 0, GameObject.Find("Map").GetComponent<MapCreator>().mapSize.y-1);

        foreach (SelectableFloor selectable in selectableFloors)
        {
            SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(enemy.transform.position));
            under.enemy = null;
        }
        enemies[enemyturn].transform.DOMove(dir, .4f, false);
    }
    void EndMove()
    {
        foreach (var enemy in enemies)
        {
            foreach (SelectableFloor selectable in selectableFloors)
            {
                SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(enemy.transform.position));
                under.enemy = enemy;
            }
        }
    }
}

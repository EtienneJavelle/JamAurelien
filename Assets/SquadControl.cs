using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SquadControl : MonoBehaviour
{
    public SquadMember[] squadMembers;
    public SelectableFloor[] selectableFloors;

    public int teamMemberTurn = 0;

    public float turnSpeed = .4f;

    public bool attackMode;
    // Start is called before the first frame update
    void Awake()
    {
        selectableFloors = this.gameObject.GetComponentsInChildren<SelectableFloor>();
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (SelectableFloor selectableFloor in selectableFloors)
        {

            selectableFloor.selected = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) hit.transform.gameObject.GetComponent<SelectableFloor>().selected = true;

        if (Input.GetButtonDown("Fire1") && Tools.Timer.Check(nextTurn))
        {
            if (hit.transform.gameObject.TryGetComponent(out SelectableFloor floor))
            {
                if (!attackMode)
                {
                    if (floor.canMove)
                    {
                        nextTurn = Tools.Timer.New(turnSpeed);
                        MoveMember(1, hit.transform.position);
                        teamMemberTurn++;
                        if (teamMemberTurn >= squadMembers.Length) teamMemberTurn = 0;
                        Invoke("NewTurn", turnSpeed + .1f);
                    }
                }
                else
                {

                    if (floor.blocked && floor.enemy != null)
                    {
                        nextTurn = Tools.Timer.New(turnSpeed);
                        Attack(floor.enemy);
                        Debug.Log("Attack");
                        teamMemberTurn++;
                        if (teamMemberTurn >= squadMembers.Length) teamMemberTurn = 0;
                        Invoke("NewTurn", turnSpeed + .1f);
                    }
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            attackMode = !attackMode;
            NewTurn();
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.LooseLife(squadMembers[teamMemberTurn].attackValue);
    }

    public void NewTurn()
    {
        foreach (SelectableFloor selectableFloor in selectableFloors)
        {
            selectableFloor.blocked = false;
            if (attackMode)
            {
                if (selectableFloor.transform.position.x <= squadMembers[teamMemberTurn].transform.position.x + squadMembers[teamMemberTurn].attackRange &&
                    selectableFloor.transform.position.x >= squadMembers[teamMemberTurn].transform.position.x - squadMembers[teamMemberTurn].attackRange &&
                    selectableFloor.transform.position.z <= squadMembers[teamMemberTurn].transform.position.z + squadMembers[teamMemberTurn].attackRange &&
                    selectableFloor.transform.position.z >= squadMembers[teamMemberTurn].transform.position.z - squadMembers[teamMemberTurn].attackRange &&
                    Tools.YZero(selectableFloor.transform.position) != Tools.YZero(squadMembers[teamMemberTurn].transform.position))
                    selectableFloor.canAttack = true;
                else
                {
                    selectableFloor.canAttack = false;
                }
                selectableFloor.canMove = false;
            }
            else
            {
                if (selectableFloor.transform.position.x <= squadMembers[teamMemberTurn].transform.position.x + squadMembers[teamMemberTurn].moveSpeed &&
                    selectableFloor.transform.position.x >= squadMembers[teamMemberTurn].transform.position.x - squadMembers[teamMemberTurn].moveSpeed &&
                    selectableFloor.transform.position.z <= squadMembers[teamMemberTurn].transform.position.z + squadMembers[teamMemberTurn].moveSpeed &&
                    selectableFloor.transform.position.z >= squadMembers[teamMemberTurn].transform.position.z - squadMembers[teamMemberTurn].moveSpeed &&
                    Tools.YZero(selectableFloor.transform.position) != Tools.YZero(squadMembers[teamMemberTurn].transform.position))
                    selectableFloor.canMove = true;
                else
                {
                    selectableFloor.canMove = false;
                }
                selectableFloor.canAttack = false;
            }
        }
        foreach (SquadMember squadMember in squadMembers)
        {
            SelectableFloor under = Array.Find(selectableFloors, p => Tools.YZero(p.transform.position) == Tools.YZero(squadMember.transform.position));
            under.canMove = false;
            under.blocked = true;
        }
        foreach (SquadMember squadMember in squadMembers)
        {
            squadMember.GetComponent<SpriteRenderer>().color = Color.white;
        }
        squadMembers[teamMemberTurn].GetComponent<SpriteRenderer>().color = Color.blue;
    }

    void MoveMember(int dist, Vector3 dir)
    {
        dir.y = .9f;
        squadMembers[teamMemberTurn].transform.DOMove(dir, turnSpeed, false);
    }

    float nextTurn;
}

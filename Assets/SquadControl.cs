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
    // Start is called before the first frame update
    void Start()
    {
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.RightArrow)) MoveMember(1, new Vector3(1,0,0));
        //if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveMember(1, new Vector3(-1, 0, 0));
        //if (Input.GetKeyDown(KeyCode.UpArrow)) MoveMember(1, new Vector3(0, 0, 1));
        //if (Input.GetKeyDown(KeyCode.DownArrow)) MoveMember(1, new Vector3(0, 0, -1));

        


            
        foreach (SelectableFloor selectableFloor in selectableFloors) selectableFloor.selected = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) hit.transform.gameObject.GetComponent<SelectableFloor>().selected = true; ;
        if (Input.GetButtonDown("Fire1"))
        {
            if (hit.transform.gameObject.GetComponent<SelectableFloor>().canMove)
            {
                MoveMember(1, hit.transform.position);
                teamMemberTurn++;
                if (teamMemberTurn >= squadMembers.Length) teamMemberTurn = 0;
            }
        }


    }

    void NewTurn()
    {
        foreach (SelectableFloor selectableFloor in selectableFloors)
        {
            selectableFloor.blocked = false;
            if (selectableFloor.transform.position.x <= squadMembers[teamMemberTurn].transform.position.x + squadMembers[teamMemberTurn].mouveSpeed &&
                selectableFloor.transform.position.x >= squadMembers[teamMemberTurn].transform.position.x - squadMembers[teamMemberTurn].mouveSpeed &&
                selectableFloor.transform.position.z <= squadMembers[teamMemberTurn].transform.position.z + squadMembers[teamMemberTurn].mouveSpeed &&
                selectableFloor.transform.position.z >= squadMembers[teamMemberTurn].transform.position.z - squadMembers[teamMemberTurn].mouveSpeed &&
                YZero(selectableFloor.transform.position) != YZero(squadMembers[teamMemberTurn].transform.position))
                selectableFloor.canMove = true;
            else
            {
                selectableFloor.canMove = false;
            }
        }
        foreach (SquadMember squadMember in squadMembers)
        {
            SelectableFloor under = Array.Find(selectableFloors, p => YZero(p.transform.position) == YZero(squadMember.transform.position));
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
        squadMembers[teamMemberTurn].transform.DOMove(dir, turnSpeed,false);
        Invoke("NewTurn", turnSpeed+.1f);
    }

    Vector3 YZero(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }
}

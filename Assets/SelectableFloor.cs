using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableFloor : MonoBehaviour
{
    public bool selected = false;
    public bool canMove = false;
    public bool blocked = false;
    public Material[] materials;
    void Select()
    {
        this.transform.localScale = Vector3.one * 1.1f;
    }
    void DeSelect()
    {
        this.transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (selected) Select();
        else DeSelect();
        if (canMove) Walkable();
        else NonWalkable();
        if (blocked) Blocked();
    }

    private void Blocked()
    {
        this.GetComponent<Renderer>().material = materials[2];
    }

    void NonWalkable()
    {
        this.GetComponent<Renderer>().material = materials[0];
    }

    void Walkable()
    {
        this.GetComponent<Renderer>().material = materials[1];
    }
}

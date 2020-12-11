using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int life = 5;
    public int moveSpeed = 1;
    public int attackRange = 1;
    public int attackValue = 1;
    public GameObject poufPrefab;

    private void Awake()
    {
        currentLife = life;
        Debug.Log(currentLife);
    }
    public void LooseLife(int ammount)
    {
        currentLife -= ammount;
        if (currentLife <= 0)
        {
            GameObject.Find("Team").GetComponent<SquadControl>().NewTurn();
            //this.gameObject.SetActive(false);
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
    [SerializeField]
    int currentLife;
}

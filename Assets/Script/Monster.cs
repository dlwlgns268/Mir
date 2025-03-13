using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public float mobCurHp;
    public float mobAtk;
    public float mobSpd;
    public float mobAtkDelay;
    public GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
    }

    public abstract void Damagee();
    public abstract void Damage();
}

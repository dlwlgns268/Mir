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
    public PlayerStateManager player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStateManager>();
    }

    public abstract void Damagee();
    public abstract void Damage();
}

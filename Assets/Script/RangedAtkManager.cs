using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class RangedAtkManager : MonoBehaviour
{
    public atk atk;
    private int _dam = 0;
    public Rigidbody2D rigid;
    public CircleCollider2D col;
    public bool facing;
    private float _size = 0;

    public void Initialize()
    {
        _dam = atk.rangedAtk;
        _size = atk.bulletsize;
        transform.localScale = Vector3.one * _size;
        rigid.velocity = (facing ? Vector3.left : Vector3.right) * 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("monster"))
        {
            Vector2 knockBack = other.transform.position - transform.position;
            other.GetComponent<Monster>().Damage(_dam, knockBack);
        }
    }
}

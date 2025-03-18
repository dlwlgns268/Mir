using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class atk : MonoBehaviour
{
    private float _cooltime = 0.25f;
    private float _curCooltime = 0;
    private float _rangedCooltime = 3f;
    private float _curRangedCooltime = 0;
    public Transform pos;
    public Vector2 boxSize;
    public SpriteRenderer spriteRenderer;
    public PlayerStateManager playerState;

    void Update()
    {
        pos.localPosition = spriteRenderer.flipX ? new Vector3(-0.175f, 0.051f, 0) : new Vector3(0.175f, 0.051f, 0);
        if (_curCooltime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _curCooltime = _cooltime;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("monster"))
                    {
                        Vector2 knockBack = collider.transform.position - transform.position;
                        collider.GetComponent<Monster>().Damage(playerState.PlayerAtk, knockBack);
                    }
                }
            }
        }
        else
        {
            _curCooltime -= Time.deltaTime;
        }
        
        if (_curRangedCooltime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                PlayerMove.CanMove = false;
                
            }
        }
        else
        {
            _curRangedCooltime -= Time.deltaTime;
        }
    }
}

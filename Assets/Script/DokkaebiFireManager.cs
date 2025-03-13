using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DokkaebiFireManager : Monster
{
    public PlayerStateManager player;
    public ChargeDash dash;
    private Rigidbody2D _rigid;
    private SpriteRenderer _sprite;
    private const float DFSight = 27.5f;
    private const float DFAtkSight = 5.5f;
    
    public DokkaebiFireManager() {
        mobCurHp = 50;
        mobAtk = 6;
        mobSpd = 10;
        mobAtkDelay = 2;
    }

    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _rigid.freezeRotation = true;
    }
    
    private void LockOnTarget()
    {
        if (player.transform.position.x < transform.position.x)
        {
            _sprite.flipX = false;
        }
        else
        {
            _sprite.flipX = true;
        }
    }
    public override void Damage()
    {
        mobCurHp -= player.PlayerAtk;
    }

    public override void Damagee()
    {
        mobCurHp -= dash._dashAtk;
    }
    
    void MoveToTarget()
    {
        float dir = player.transform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(mobSpd * Time.deltaTime *new Vector2(dir, 0));
    }
    
    void AttackToTarget()
    {
        transform.AddComponent<BoxCollider2D>();
        Destroy(GetComponent<BoxCollider2D>());
    }
}

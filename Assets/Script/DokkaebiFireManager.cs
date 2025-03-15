using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DokkaebiFireManager : Monster
{
    public ChargeDash dash;
    private Rigidbody2D _rigid;
    private SpriteRenderer _sprite;
    private const float DFSight = 27.5f;
    private const float DFAtkSight = 5.5f;
    private const float DFCliffSight = 2f;
    private float _direct = 1;
    private const float StopChaseTime = 0.5f;
    private float _playerLostTime = 0f;
    private bool _isChasing;
    private Coroutine _patrolCoroutine;
    
    
    public DokkaebiFireManager() {
        mobCurHp = 50;
        mobAtk = 6;
        mobSpd = 2;
        mobAtkDelay = 2;
    }

    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _rigid.freezeRotation = true;
    }
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        mobAtkDelay -= Time.deltaTime;

        if (mobAtkDelay < 0)
        {
            mobAtkDelay = 0;
        }
        
        if (mobAtkDelay <= 0 && distance <= DFSight)
        {
            _isChasing = true;
            LockOnTarget();

            if (distance <= DFAtkSight)
            {
                StartCoroutine(AttackToTarget());
            }
            else
            {
                MoveToTarget();
            }
        }
        else
        {
            if (_isChasing)
            {
                _playerLostTime += Time.deltaTime;
            }
            _isChasing = false;
        }

        if (!_isChasing && _patrolCoroutine == null)
        {
            _patrolCoroutine = StartCoroutine(Patrol());
        }
        else if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }

        if (_isChasing && _patrolCoroutine != null)
        {
         StopCoroutine(_patrolCoroutine);
         _patrolCoroutine = null;
        }

        if (_playerLostTime >= StopChaseTime && !_isChasing)
        {
            _playerLostTime = 0;
        }
    }
    
    private void LockOnTarget()
    {
        _sprite.flipX = !(player.transform.position.x < transform.position.x);
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
        if (_detectCliff()) return;
        
        float dir = (player.transform.position.x < transform.position.x) ? -1 : 1;
        transform.Translate(Vector2.right * (dir * mobSpd * Time.deltaTime));
    }
     IEnumerator AttackToTarget()
     { 
         mobSpd = 0;
        BoxCollider2D collider = transform.AddComponent<BoxCollider2D>();
        collider.size = new(0.15f, 0.2f);
        collider.isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(GetComponent<BoxCollider2D>());
        mobSpd = 2;
     }

     IEnumerator Patrol()
     {
         while (true)
         {
             if (_detectCliff()) yield return new WaitForSeconds(0.1f);
             transform.Translate(Vector2.right * (_direct * mobSpd * Time.deltaTime));
             yield return new WaitForSeconds(1.5f);
             _direct *= -1;
         }
     }

     private bool _detectCliff()
     {
         Vector2 checkPos = (Vector2)transform.position + new Vector2(_direct * 0.5f, 0);
         RaycastHit2D groundCheck = Physics2D.Raycast(checkPos, Vector2.down, DFCliffSight, LayerMask.GetMask("Ground"));

         if (!groundCheck.collider)
         {
             _direct *= -1;
             _sprite.flipX = _direct > 0;
             _isChasing = false;
             return true;
         }
         return false;
     }
}

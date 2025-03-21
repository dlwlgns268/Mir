using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaskGhostManager : Monster
{
    public Animator anim;
    public ChargeDash dash;
    private Rigidbody2D _rigid;
    private bool _canMove = true;
    private bool PlayerDetection
    {
        get
        {
            var pos = transform.position;
            var playerPos = player.transform.position;
            var xs = Mathf.Abs(playerPos.x - pos.x) <= 3.5;
            var ys = Mathf.Abs(playerPos.y - pos.y) <= 1.6;
            return xs && ys;
        }
    }

    private bool _isRandomMoving;
    private Coroutine _randomMoveCoroutine;
    public List<Transform> movingPositions = new();  
    
    private new void Awake()
    {
        base.Awake();
        mobCurHp = 70;
        mobAtk = 4;
        mobSpd = 1.2f;
        mobAtkDelay = 2.7f;
        ApplyDelay();
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.freezeRotation = true;
        if (movingPositions.Count > 0) StartCoroutine(StaticMoveFlow());
    }

    private void Update()
    {
        if (mobCurHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private new void FixedUpdate()
    {
        if (PlayerDetection)
        {
            base.FixedUpdate();
            _isRandomMoving = false;
            if (_randomMoveCoroutine != null)
            {
                StopCoroutine(_randomMoveCoroutine);
                _randomMoveCoroutine = null;
            }
            spriteRenderer.flipX = transform.position.x > player.transform.position.x;
            if (Clipping(spriteRenderer.flipX ? Vector3.right : Vector3.left))
                MoveTo(_rigid.position + (spriteRenderer.flipX ? Vector2.left : Vector2.right) * (mobSpd * Time.fixedDeltaTime));
            if (!(Vector3.Distance(player.transform.position, transform.position) <= 0.16) || IsDelayed) return;
            player.Damage(mobAtk);
            ApplyDelay();
            return;
        }
        if (movingPositions.Count > 0) return;
        if (!_isRandomMoving && Random.Range(0, 250) == 0)
        {
            _randomMoveCoroutine = StartCoroutine(RandomMoveFlow(Vector2.left));
            spriteRenderer.flipX = true;
        }
        if (_isRandomMoving || Random.Range(0, 250) != 0) return;
        _randomMoveCoroutine = StartCoroutine(RandomMoveFlow(Vector2.right));
        spriteRenderer.flipX = false;
    }

    public override void Damage(int damage, Vector2 knockBack)
    {
        anim.SetBool("Blowed", true);
        currentDelay = 1000000;
        _rigid.velocity = knockBack;
        _canMove = false;
        var dmg = Mathf.Max(damage + Random.Range(-2, 3), 1);
        mobCurHp -= dmg;
        var o = Instantiate(text);
        o.DamageIndicate($"{dmg}", transform);
        StartCoroutine(KnockBackFlow());
    }

    public IEnumerator KnockBackFlow()
    {
        yield return new WaitForSeconds(0.33f);
        _canMove = true;
        ApplyDelay();
        anim.SetBool("Blowed", false);
    }

    private IEnumerator RandomMoveFlow(Vector2 direction)
    {
        _isRandomMoving = true;
        for (var i = 0; i < Random.Range(5, 100); i++)
        {
            if (Clipping(spriteRenderer.flipX ? Vector3.right : Vector3.left)) MoveTo(_rigid.position + direction * (mobSpd * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
        _isRandomMoving = false;
    }

    private IEnumerator StaticMoveFlow()
    {
        while (true)
        {
            foreach (var movingPosition in movingPositions)
            {
                while (Vector3.Distance(transform.position, movingPosition.position) >= 0.1)
                {
                    if (!PlayerDetection)
                        MoveTo(Vector2.MoveTowards(transform.position, movingPosition.position,
                            mobSpd * Time.fixedDeltaTime));
                    spriteRenderer.flipX = transform.position.x < movingPosition.transform.position.x;
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSeconds(movingPosition.localScale.x);
            }
        }
    }

    public void MoveTo(Vector2 position)
    {
        if (_canMove) _rigid.position = position;
    }
}

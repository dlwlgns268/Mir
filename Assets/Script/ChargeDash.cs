using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDash : MonoBehaviour
{
    public int _dashAtk = 0;
    private int _dashPower = 50;
    private float _curChage = 0f;
    private float _maxCharge = 5f;
    private float _horizontal;
    private float _dashCooltime = 1.2f;
    [SerializeField] private float _currDashCooltime = 0f;
    [SerializeField] private Rigidbody2D _rigid;
    public Animator _anim;
    public static bool IsDashing;
    public Transform pos;
    public Vector2 boxSize;
    private Coroutine _dashCoroutine;
    
    private void Awake()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _rigid.freezeRotation = true;
        //_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_currDashCooltime <= 0)
        {
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _rigid.velocity = new Vector2(0, 0);
                _rigid.gravityScale = 0.02f;
                _dashCoroutine = StartCoroutine(Dash());
            }
        }
        else
        {
            _currDashCooltime -= Time.deltaTime;
        }

        if (IsDashing == true)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Monster>().Damagee();
                    if (IsDashing)
                    {
                        _rigid.velocity *= 0.1f;
                        StopCoroutine(_dashCoroutine);
                        PlayerMove.CanMove = true;
                        _rigid.gravityScale = 2f;
                        _currDashCooltime = _dashCooltime;
                        _dashAtk = 0;
                        IsDashing = false;
                    }
                }
            }
        }
    }

    IEnumerator Dash()
    {
        IsDashing = true;
        PlayerMove.CanMove = false;
        float currentTime = Time.fixedTime;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Z));
        float cal = Mathf.Min(Time.fixedTime - currentTime, 2);
        float atkcal = Mathf.Min(Time.fixedTime - currentTime, 10);
        _rigid.gravityScale = 0f;
        _rigid.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * (cal * 16);
        _dashAtk = (int)(atkcal * 20);
        PlayerMove.CanMove = true;
        yield return new WaitForSeconds(0.2f);
        _rigid.velocity = new Vector2(0, 0);
        _rigid.gravityScale = 0.2f;
        yield return new WaitForSeconds(0.04f);
        _rigid.gravityScale = 0.8f;
        yield return new WaitForSeconds(0.04f);
        _rigid.gravityScale = 2f;
        _currDashCooltime = _dashCooltime;
        _dashAtk = 0;
        IsDashing = false;
    }
    
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(pos.position, boxSize);
    }
}

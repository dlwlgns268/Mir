using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDash : MonoBehaviour
{
    private int _dashAtk = 0;
    private int _dashPower = 50;
    private float _curChage = 0f;
    private float _maxCharge = 5f;
    private float _horizontal;
    private float _dashCooltime = 0.38f;
    [SerializeField] private float _currDashCooltime = 0f;
    [SerializeField] private Rigidbody2D _rigid;
    public Animator _anim;
    private bool _isdashing;
    
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
                StartCoroutine(Dash());
            }
        }
        else
        {
            _currDashCooltime -= Time.deltaTime;
        }
        
    }

    IEnumerator Dash()
    {
        PlayerMove.CanMove = false;
        float currentTime = Time.fixedTime;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Z));
        float cal = Mathf.Min(Time.fixedTime - currentTime, 2);
        _rigid.gravityScale = 0f;
        _rigid.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * (cal * 16);
        PlayerMove.CanMove = true;
        yield return new WaitForSeconds(0.2f);
        _rigid.velocity = new Vector2(0, 0);
        _rigid.gravityScale = 0.2f;
        yield return new WaitForSeconds(0.04f);
        _rigid.gravityScale = 0.8f;
        yield return new WaitForSeconds(0.04f);
        _rigid.gravityScale = 2f;
        _currDashCooltime = _dashCooltime;
    }
}

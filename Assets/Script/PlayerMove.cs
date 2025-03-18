using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _speed = 200;
    [SerializeField] private float JumpPower = 9.2f;
    private float _horizontal;
    private Rigidbody2D _rigid;
    public Animator _anim;
    [SerializeField] private bool _isJumping;
    private static readonly int Isjumping = Animator.StringToHash("isjumping");
    private SpriteRenderer _spriteRenderer;
    public static bool CanMove = true;

    private void Awake()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _rigid.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _isJumping = false;
        //_jumpStack = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false && !ChargeDash.IsDashing)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _isJumping = !_rigid.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!CanMove) _horizontal = 0;
        if (!Input.GetKey(KeyCode.Z) && _rigid.velocity.magnitude < 5)
        {
            Vector2 movePos = new Vector2(_horizontal * (_speed * Time.fixedDeltaTime), _rigid.velocity.y);
            _rigid.velocity = movePos;
            if (_horizontal > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_horizontal < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        _anim.SetBool("run", _horizontal != 0);
        _anim.SetBool("jump", _isJumping);
        
        if (!(_rigid.velocity.y < 0)) return;
        Debug.DrawRay(_rigid.position, Vector3.down, new Color(0, 1, 0));
        var rayHit = Physics2D.Raycast(_rigid.position, Vector3.down, 1, LayerMask.GetMask($"Platform"));
        if (!rayHit.collider) return;
        if (rayHit.distance < 0.5f) _anim.SetBool(Isjumping, false);
    }

    private void Jump()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, JumpPower);
        _isJumping = true;
    }
}

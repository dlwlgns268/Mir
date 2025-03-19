using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;

public class atk : MonoBehaviour
{
    public int rangedAtk = 0;
    private float _cooltime = 0.25f;
    private float _curCooltime = 0;
    private float _rangedCooltime = 6f;
    [SerializeField] private float _curRangedCooltime = 0;
    [SerializeField] private float _chargeTime = 0;
    public bool canShot;
    public float bulletsize;
    public GameObject fire;
    public GameObject player;
    public Transform pos;
    public Vector2 boxSize;
    public SpriteRenderer spriteRenderer;
    public PlayerStateManager playerState;
    public ChargeDash dash;
    private Coroutine _chargeShot;

    private void Awake()
    {
        canShot = true;
    }

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
            if (Input.GetKeyDown(KeyCode.X) && canShot)
            {
                dash.canDash = false;
                _chargeShot = StartCoroutine(ChargeShot());
                _curRangedCooltime = _rangedCooltime;
            }
        }
        else
        {
            _curRangedCooltime -= Time.deltaTime;
        }
    }

    IEnumerator ChargeShot()
    {
        _chargeTime = 0;
        float currentTime = Time.fixedTime;
        PlayerMove.CannotMove.Add("ChargeShot");
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.X));
        float cal = Mathf.Min(Time.fixedTime - currentTime, 4);
        float dur = Mathf.Min(Time.fixedTime - currentTime, 7.5f);
        rangedAtk = (int)((playerState.PlayerAtk * 0.5) * cal);
        bulletsize = (0.01f + (cal * 0.25f));
        var position = player.transform.position;
        var inst = Instantiate(fire, new Vector3(position.x, position.y, 0), Quaternion.identity).GetComponent<RangedAtkManager>();
        PlayerMove.CannotMove.Remove("ChargeShot");
        dash.canDash = true;
        inst.atk = this;
        inst.facing = spriteRenderer.flipX;
        inst.Initialize();
        yield return new WaitForSeconds(dur);
        Destroy(inst);
    }
}

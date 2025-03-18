using System;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public float currentDelay;
    public float mobCurHp;
    public float mobAtk;
    public float mobSpd;
    public float mobAtkDelay;
    public bool isSmart;
    public PlayerStateManager player;
    public SpriteRenderer spriteRenderer;
    public bool IsDelayed => currentDelay > 0;

    public void Awake()
    {
        player = FindObjectOfType<PlayerStateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        currentDelay = 0.95f;
    }

    public void FixedUpdate()
    {
        currentDelay -= Time.fixedDeltaTime;
        if (currentDelay < 0) currentDelay = 0;
    }

    public void ApplyDelay()
    {
        currentDelay = mobAtkDelay;
    }
    
    public abstract void Damage(int damage, Vector2 knockBack);

    public bool Clipping(Vector3 direction)
    {
        var pos = transform.position + direction * 0.2f;
        return !isSmart || Physics2D.Raycast(pos, Vector2.down, 1f, LayerMask.GetMask("Ground")).collider;
    }
}

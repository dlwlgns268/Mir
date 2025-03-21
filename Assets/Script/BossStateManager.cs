using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStateManager : Monster
{
    public int bosscurrHp;
    private static int _bossHp = 2000;
    private int _bossAtk;
    private static GameObject _boss;
    public Slider bossHpBar;
    public ChargeDash dash;
    
    
    void Start()
    {
        _bossHp = 2000;
        bosscurrHp = _bossHp;
        _bossAtk = 8;
    }

    
    void Update()
    {
        bossHpBar.value = (float) bosscurrHp/_bossHp;
    }

    public override void Damage(int damage, Vector2 knockBack)
    {
        var dmg = Mathf.Max(damage + Random.Range(-2, 3), 1);
        bosscurrHp -= dmg;
        var o = Instantiate(text);
        o.DamageIndicate($"{dmg}", transform);
    }
}

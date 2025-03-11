using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStateManager : MonoBehaviour
{
    public int bosscurrHp;
    private static int _bossHp = 7000;
    private int _bossAtk;
    private static GameObject _boss;
    public Slider bossHpBar;
    public PlayerStateManager player;
    public ChargeDash dash;
    
    
    void Start()
    {
        _bossHp = 7000;
        bosscurrHp = _bossHp;
        _bossAtk = 8;
    }

    
    void Update()
    {
        bossHpBar.value = (float) bosscurrHp/_bossHp;
    }

    public void Damage()
    {
        bosscurrHp -= player.PlayerAtk;
    }

    public void Damagee()
    {
        bosscurrHp -= dash._dashAtk;
    }
}

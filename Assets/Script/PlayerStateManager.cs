using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateManager : MonoBehaviour
{
    public float _playerCurHp;
        public float PlayerAtk;
        private static int _playerDef;
        private static int _playerMaxHp;
        private static GameObject _player;
        public float _invincibleTime = 0;
        public Image HpBar;

    void Start()
    {
        _playerMaxHp = 100;
        PlayerAtk = 20;
        _playerDef = 10;
        _playerCurHp = _playerMaxHp;
        HpBar = HpBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = _playerCurHp/_playerMaxHp;
        _invincibleTime -= Time.deltaTime;
        if (_invincibleTime < 0)
        {
            _invincibleTime = 0;
        }
        if (_playerCurHp <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }

        if (_playerCurHp < 0)
        {
            _playerCurHp = 0;
        }
    }
}

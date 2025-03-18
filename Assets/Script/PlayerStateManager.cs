using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateManager : MonoBehaviour
{
        public float _playerCurHp;
        public int PlayerAtk;
        private static int _playerMaxHp;
        private static GameObject _player;
        public float _invincibleTime = 0;
        public Image HpBar;
        public float playerCurMp;
        private static int _playerMaxMp;

    void Start()
    {
        _playerMaxHp = 100;
        _playerMaxMp = 50;
        PlayerAtk = 20;
        _playerCurHp = _playerMaxHp;
        playerCurMp = _playerMaxMp;
        HpBar = HpBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = (float) _playerCurHp/_playerMaxHp;
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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerCurHp -= 5;
        }
    }

    public void Damage(float amount)
    {
        _playerCurHp -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("deadline"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    private RaycastHit2D _ray;
    private float _maxRayDistance = 0.25f;
    public SpriteRenderer player;
    void Start()
    {
        
    }
    
    void Update()
    {
        bool left = player.flipX;
        _ray = Physics2D.Raycast(player.transform.position, new Vector3(left ? -1 : 1, 0, 0), _maxRayDistance, LayerMask.GetMask("Interaction"));
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.DrawRay(transform.position, new Vector3(left ? -1 : 1, 0, 0) * _maxRayDistance, Color.red);
            if (_ray.collider)
            {
                if (_ray.collider.CompareTag("Portal"))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    GetComponent<PlayerStateManager>()._playerCurHp = 100;
                }
            }
        }
    }

    public void interaction()
    {
        
    }
}

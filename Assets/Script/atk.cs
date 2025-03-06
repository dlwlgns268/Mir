using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class atk : MonoBehaviour
{
    private float _cooltime = 0.1f;
    private float _curCooltime = 0;
    public Transform pos;
    public Vector2 boxSize;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_curCooltime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _curCooltime = _cooltime;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    Debug.Log(collider.tag);
                }
            }
        }
        else
        {
            _curCooltime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(pos.position, boxSize);
    }
}

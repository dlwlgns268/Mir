using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private RaycastHit2D _ray;
    private float _maxRayDistance = 0.25f;
    public GameObject player;
    void Start()
    {
        
    }
    
    void Update()
    {
        _ray = Physics2D.Raycast(player.transform.position, transform.right, _maxRayDistance);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.DrawRay(transform.position, transform.right * _maxRayDistance, Color.red);
            if (_ray)
            {
                //_ray.transform
            }
        }
    }

    public void interaction()
    {
        
    }
}

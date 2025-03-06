using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    private int _patternNumber = 0;
    void Start()
    {
        _patternNumber = Random.Range(1, 4);
    }
    
    void Update()
    {
        
    }

    /*private IEnumerator TeleportAttack()
    {
        
    }*/
}

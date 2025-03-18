using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Animator _anime;
    // Start is called before the first frame update
    void Start()
    {
        _anime.SetBool("open", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _anime.SetBool("open", true);
        }
    }
}

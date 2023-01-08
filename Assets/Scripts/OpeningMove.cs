using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMove : MonoBehaviour
{
    public float speed;
    private Vector3 move;

    private SpriteRenderer _spriteRenderer;
    private Animation _animation;
    private void Awake()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boundary")
        {
            
        }
    }
}

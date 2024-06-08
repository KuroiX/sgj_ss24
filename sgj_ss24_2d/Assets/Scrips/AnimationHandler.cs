using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public float speed;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
      
        transform.position += new Vector3(horizontal, vertical, 0).normalized * speed * Time.deltaTime;

        ResetAll();
        
        if(horizontal < 0)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool("L", true);
        }
        else if (horizontal > 0)
        {
            _spriteRenderer.flipX = false;
            _animator.SetBool("R", true);
        }

        if (vertical < 0)
        {
            _animator.SetBool("U", true);
        }
        else if (vertical > 0)
        {
            _animator.SetBool("O", true);
        }
    }


    private void ResetAll()
    {
        _animator.SetBool("L", false);
        _animator.SetBool("R", false);
        _animator.SetBool("O", false);
        _animator.SetBool("U", false);
    }
}

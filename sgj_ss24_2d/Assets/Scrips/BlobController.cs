using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BlobController : MonoBehaviour
{
    [Header("Scale")]
    public float minScale = 0.1f;
    public float maxScale = 2;
    [Header("Speed")]
    public float minSpeed;
    public float maxSpeed;
    [Header("Dash")]
    public float dashForce = 10;
    public float dashCoolDown = 3;
    [Header("Other")]
    public float shrinkFactor;
   

    private float _upInput;
    private float _downInput;
    private float _rightInput;
    private float _leftInput;

    private Vector2 _velocity;

    private Rigidbody2D _rigidbody2D;
    private bool canDash = true;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(maxScale, maxScale, maxScale);
    }

    private void Update()
    {
        ShrinkBlob();

        if (IsDeath())
            Debug.Log("IsDeath Now");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ResetInput()
    {
        _upInput = 0;
        _downInput = 0;
        _rightInput = 0;
        _leftInput = 0;
    }
    
    private void Move()
    {
        Vector2 moveDir = CalculateDir();

        float currSpeed = Mathf.Lerp(minSpeed, maxSpeed,
            Mathf.InverseLerp(maxScale, minScale, transform.localScale.x));

        Vector2 velocity = new Vector2(moveDir.x, moveDir.y) * currSpeed;
        _rigidbody2D.AddForce(velocity, ForceMode2D.Force);
    }

    public void DropCollected(float value)
    {
        transform.localScale += new Vector3(value, value, value);
    }

    private void ShrinkBlob()
    {
        float subtrator = shrinkFactor * Time.deltaTime;
        Vector3 currScale = transform.localScale;
        Vector3 newScale = new Vector3(currScale.x - subtrator, currScale.y - subtrator,
            currScale.z - subtrator);
        transform.localScale = newScale;
    }

    private Vector2 CalculateDir()
    {
        var vec = new Vector2(_rightInput - _leftInput, _upInput - _downInput);
        return vec.normalized;
    }

    private bool IsDeath()
    {
        return transform.localScale.x <= minScale;
    }

    public void Dash()
    {
        if (canDash)
        {
            var vec = CalculateDir();
            _rigidbody2D.AddForce(vec * dashForce,ForceMode2D.Impulse);
            canDash = false;
            Invoke(nameof(ResetDash),dashCoolDown);
        }
    }

    private void ResetDash()
    {
        canDash = true;
    }

    public void SetUp(float f)
    {
        _upInput = Mathf.Clamp01(Mathf.Abs(f));
        _downInput = 0;
    }

    public void SetDown(float f)
    {
        _downInput = Mathf.Clamp01(Mathf.Abs(f));
        _upInput = 0;
    }

    public void SetRight(float f)
    {
        _rightInput = Mathf.Clamp01(Mathf.Abs(f));
        _leftInput = 0;
    }

    public void SetLeft(float f)
    {
        _leftInput = Mathf.Clamp01(Mathf.Abs(f));
        _rightInput = 0;
    }
}
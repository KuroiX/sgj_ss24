using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BlobController : MonoBehaviour
{
    public float minScale = 0.1f;
    public float maxScale = 2;
    
    public float minSpeed;
    public float maxSpeed;
    
    public float shrinkFactor;

    private float _upInput;
    private float _downInput;
    private float _rightInput;
    private float _leftInput;

    private Rigidbody2D _rigidbody2D;

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
        Vector2 moveDir = new Vector2(_rightInput - _leftInput, _upInput - _downInput);
        moveDir.Normalize();

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

    private bool IsDeath()
    {
        return transform.localScale.x <= minScale;
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
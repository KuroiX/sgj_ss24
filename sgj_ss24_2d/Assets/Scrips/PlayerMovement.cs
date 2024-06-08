using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float startScale = 2;
    public float minScale = 0.1f;
    public float maxSpeed;
    public float minSpeed;
    public float shrinkFactor;

    private float _upInput;
    private float _downInput;
    private float _rightInput;
    private float _leftInput;

    private Rigidbody2D _rigidbody2D;

    public InputActionReference PlayerOne;
    public InputActionReference PlayerTwo;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TestInput();

        ShrinkBlob();

        if (IsDeath())
            Debug.Log("IsDeath Now");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void TestInput()
    {
        var v = PlayerOne.action.ReadValue<Vector2>();
        if (v.x > 0)
            SetRight(v.x);
        if (v.y > 0)
            SetUp(v.y);
        if (v.x < 0)
            SetLeft(v.x);
        if (v.y < 0)
            SetDown(v.y);

        Debug.Log("P1:" + v);
        
        v = PlayerTwo.action.ReadValue<Vector2>();
        if (v.x > 0)
            SetRight(v.x);
        if (v.y > 0)
            SetUp(v.y);
        if (v.x < 0)
            SetLeft(v.x);
        if (v.y < 0)
            SetDown(v.y);
        
        
        Debug.Log("P2:" + v);
    }

    private void Move()
    {
        Vector2 moveDir = new Vector2(_rightInput - _leftInput, _upInput - _downInput);
        moveDir.Normalize();

        float currSpeed = Mathf.Lerp(minSpeed, maxSpeed,
            Mathf.InverseLerp(startScale, minScale, transform.localScale.x));

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
    }

    public void SetDown(float f)
    {
        _downInput = Mathf.Clamp01(Mathf.Abs(f));
    }

    public void SetRight(float f)
    {
        _rightInput = Mathf.Clamp01(Mathf.Abs(f));
    }

    public void SetLeft(float f)
    {
        _leftInput = Mathf.Clamp01(Mathf.Abs(f));
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BlobController : MonoBehaviour
{
    [Header("Scale")] public float minScale = 0.1f;
    public float maxScale = 1;
    [Header("Speed")] public float minSpeed;
    public float maxSpeed;
    [Header("Dash")] public float dashForce = 10;
    public float dashCoolDown = 3;
    [Header("Other")] public float initialWaterStorage;
    public SubtrahentSO shrinkSubtrahent;
    //public float shrinkFactor;


    private float _upInput;
    private float _downInput;
    private float _rightInput;
    private float _leftInput;

    private bool doSomething = true;
    private bool canShrink = true;

    private Vector2 _velocity;

    private Rigidbody2D _rigidbody2D;
    private bool canDash = true;

    private float _currentWaterStorage;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        _currentWaterStorage = initialWaterStorage;
        doSomething = false;
    }

    private void Update()
    {
        if (!doSomething) return;

        ShrinkBlob();

        if (IsDeath())
            Debug.Log("IsDeath Now");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!doSomething) return;

        Vector2 moveDir = CalculateDir();

        float currSpeed = Mathf.Lerp(minSpeed, maxSpeed,
            Mathf.InverseLerp(maxScale, minScale, transform.localScale.x));

        Vector2 velocity = new Vector2(moveDir.x, moveDir.y) * currSpeed;
        _rigidbody2D.AddForce(velocity, ForceMode2D.Force);
    }

    public void DropCollected(float value)
    {
        _currentWaterStorage += value;
        transform.localScale = new Vector3(1, 1, 1) * _currentWaterStorage / initialWaterStorage;
    }

    private void ShrinkBlob()
    {
        if (!canShrink) return;
        if (_currentWaterStorage <= 0.05)
        {
            FindObjectOfType<GameLoop>().GameOver();
            StopBlob();
        }

        _currentWaterStorage -= shrinkSubtrahent.shringkSubtrahent * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, 1) * _currentWaterStorage / initialWaterStorage;
    }

    public void GetHit(float value)
    {
        _currentWaterStorage -= value;
        if (_currentWaterStorage < 0)
            _currentWaterStorage = 0;
    }

    public Vector2 CalculateDir()
    {
        var vec = new Vector2(_rightInput - _leftInput, _upInput - _downInput);
        return vec.magnitude > 1 ? vec.normalized : vec;
    }

    private bool IsDeath()
    {
        return transform.localScale.x <= minScale;
    }

    public void Dash(Vector2 dir)
    {
        Debug.Log("dash" + canDash);
        if (canDash)
        {
            Debug.Log("can dash apparently");
            var vec = dir;
            _rigidbody2D.AddForce(vec * dashForce,ForceMode2D.Impulse);
            canDash = false;
            Invoke(nameof(ResetDash), dashCoolDown);
        }
    }

    public void StopBlob()
    {
        doSomething = false;
    }

    public void StartBlob()
    {
        doSomething = true;
    }
    
    public void StopShrinking()
    {
        canShrink = false;
    }

    public void StartShrinking()
    {
        canShrink = true;
    }

    private void ResetDash()
    {
        canDash = true;
    }

    public void SetUp(float f)
    {
        Debug.Log("up");
        _upInput = Mathf.Clamp01(Mathf.Abs(f));
        _downInput = 0;
    }

    public void SetDown(float f)
    {
        Debug.Log("down");
        _downInput = Mathf.Clamp01(Mathf.Abs(f));
        _upInput = 0;
    }

    public void SetRight(float f)
    {
        Debug.Log("right");
        _rightInput = Mathf.Clamp01(Mathf.Abs(f));
        _leftInput = 0;
    }

    public void SetLeft(float f)
    {
        Debug.Log("left");
        _leftInput = Mathf.Clamp01(Mathf.Abs(f));
        _rightInput = 0;
    }

    [ContextMenu("loadeShit")]
    private void LoadeFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }
}
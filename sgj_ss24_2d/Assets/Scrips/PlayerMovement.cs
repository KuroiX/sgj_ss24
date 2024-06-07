using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public float startScale = 2;
    public float minScale = 0.1f;
    public float maxSpeed;
    public float minSpeed;
    public float shrinkFactor;

    private bool leftInput;
    private bool rightInput;
    private bool upInput;
    private bool downInput;

    private KeyCode keyForLeft = KeyCode.A;
    private KeyCode keyForRight = KeyCode.D;
    private KeyCode keyForUp = KeyCode.UpArrow;
    private KeyCode keyForDown = KeyCode.DownArrow;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ChangeInputRandomly),6f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.localScale.x < minScale)
        {
            Debug.Log("GAME OVER!!");
            return;
        }
        
        leftInput = Input.GetKey(keyForLeft);
        rightInput = Input.GetKey(keyForRight);
        upInput = Input.GetKey(keyForUp);
        downInput = Input.GetKey(keyForDown);

        transform.localScale -= new Vector3(shrinkFactor * Time.deltaTime, shrinkFactor * Time.deltaTime, shrinkFactor * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        Vector2 moveVec = Vector2.zero;
        
        if (leftInput)
        {
            moveVec -= new Vector2(1, 0);
        }       
        if (rightInput)
        {
            moveVec += new Vector2(1, 0);
        }       
        if (upInput)
        {
            moveVec += new Vector2(0, 1);
        }       
        if (downInput)
        {
            moveVec -= new Vector2(0, 1);
        }

        float curSpeed = Mathf.Lerp(minSpeed,maxSpeed,Mathf.InverseLerp(startScale, minScale, transform.localScale.x));
        transform.position += new Vector3(moveVec.x, moveVec.y, 0).normalized * curSpeed;
    }

    public void DropCollected(float value)
    {
        transform.localScale += new Vector3(value, value, value);
    }

    private void ChangeInputRandomly()
    {
        keyForLeft = KeyCode.LeftArrow;
        keyForRight = KeyCode.RightArrow;
        keyForUp = KeyCode.W;
        keyForDown = KeyCode.S;
    }
}

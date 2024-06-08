using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed;
    public float lifeTime;

    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        Debug.Log("Player hit!");
        //TODO stuff abziehen!
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(lifeTime);
        
        Destroy(gameObject);
    }
}

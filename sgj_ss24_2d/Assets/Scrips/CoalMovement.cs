using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed;
    public float lifeTime;
    public float rotationSpeed;
    public GameObject sprite;
    public float damage;

    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
        sprite.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        Debug.Log("Player hit!");
        other.GetComponent<BlobController>().GetHit(damage);
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(lifeTime);
        
        Destroy(gameObject);
    }
}

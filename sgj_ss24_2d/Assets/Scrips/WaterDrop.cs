using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float value;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<BlobController>().DropCollected(value);
        Destroy(gameObject);
    }
}

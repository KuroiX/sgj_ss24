using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float initialWaterStorage;
    public SubtrahentSO shrinkSubtrahent;

    private float _currentWaterStorage;

    private void Start()
    {
        _currentWaterStorage = initialWaterStorage;
    }

    private void Update()
    {
        _currentWaterStorage -= shrinkSubtrahent.shringkSubtrahent * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, 1) * _currentWaterStorage / initialWaterStorage;

        if (transform.localScale.x <= 0.05)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<BlobController>().DropCollected(_currentWaterStorage);
        Destroy(gameObject);
    }
}

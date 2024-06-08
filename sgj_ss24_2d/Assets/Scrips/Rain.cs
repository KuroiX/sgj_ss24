using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rain : MonoBehaviour
{
    [SerializeField] private GameObject rainPrefab;
    public int spawnChance;
    public float height;
    public float width;

    private float _halfHeight;
    private float _halfWidth;
    
    private void Update()
    {
        Debug.DrawLine(new Vector3(-width, height, 0), new Vector3(width, height, 0));
        Debug.DrawLine(new Vector3(-width, -height, 0), new Vector3(width, -height, 0));
        Debug.DrawLine(new Vector3(-width, height, 0), new Vector3(-width, -height, 0));
        Debug.DrawLine(new Vector3(width, height, 0), new Vector3(width, -height, 0));
        
        if (Random.Range(0, spawnChance) == 0)
        {
            float randomX = Random.Range(-width, width);
            float randomY = Random.Range(-height, height);
            Instantiate(rainPrefab);
            rainPrefab.transform.position = new Vector3(randomX, randomY, 0);
        }
    }
}
